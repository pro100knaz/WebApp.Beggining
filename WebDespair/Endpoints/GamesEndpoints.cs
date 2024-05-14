using Microsoft.EntityFrameworkCore;
using WebDespair.Data;
using WebDespair.Dtos;
using WebDespair.Entities;
using WebDespair.Mapping;

namespace WebDespair.Endpoints
{
    public static class GamesEndpoints
    {

        const string getGameEndpointName = "GetGame";

        public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("games")
                    .WithParameterValidation()
                ;
            //Get / games
            group.MapGet("/", async (GameStoreContext dbContext) => await dbContext.Games
	            .Include(game => game.Genre)
	            .Select(game => game.ToGameSummaryDto())
	            .AsNoTracking()
	            .ToListAsync());


            //Get /games/1
            group.MapGet("/{id:int}", async (int id, GameStoreContext dbContext) =>
                 {

                     Game? game = await dbContext.Games.FindAsync(id);
                     game.Genre = await dbContext.Genres.FindAsync(game.GenreId);

                     return game is null ?
                         Results.NotFound() :
                         Results.Ok(game.ToGameDetailsDto());

                 })
                 .WithName(getGameEndpointName);


            //Post /games  добавить
            group.MapPost("/", async (CreateGameDto newGame, GameStoreContext dbContext) =>
                {

                    Game game = newGame.ToEntity();
                    game.Genre = await dbContext.Genres.FindAsync(game.GenreId);

                    dbContext.Games.Add(game);

                    await dbContext.SaveChangesAsync();

                    return Results.CreatedAtRoute(getGameEndpointName, new { id = game.Id }, game.ToGameDetailsDto());
                })
             .WithParameterValidation();


            //Put /games
            group.MapPut("/{id:int}", async (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
             {
                 var existingGame = await dbContext.Games.FindAsync(id);

                 if (existingGame is null)
                 {
                     return Results.NotFound();
                 }

                 dbContext.Entry(existingGame)
                    .CurrentValues
                    .SetValues(updatedGame.ToEntity(id));

                 await dbContext.SaveChangesAsync();
                 return Results.NoContent();

             });

            //Delete /games/1
            group.MapDelete("/{id:int}",  async (int id, GameStoreContext dbContext) =>
            {
                await dbContext.Games.Where(game => game.Id == id)
                    .ExecuteDeleteAsync();
                await dbContext.SaveChangesAsync();

                return Results.NoContent();
            });


            app.MapGet("/", () => "Hello World!");


            return group;
        }
    }
}
