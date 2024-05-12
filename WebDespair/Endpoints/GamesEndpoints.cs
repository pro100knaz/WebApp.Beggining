using WebDespair.Data;
using WebDespair.Dtos;
using WebDespair.Entities;
using WebDespair.Mapping;

namespace WebDespair.Endpoints
{
    public static class GamesEndpoints
    {
        static Random rnd = new Random();
        const string getGameEndpointName = "GetGame";
        private static readonly List<GameSummaryDto> games = Enumerable.Range(1, 10).Select(i => new GameSummaryDto(i,
            $"Game {i}",
            $"Genre {i}",
            (decimal)(i + rnd.NextDouble() * rnd.Next(10, 100)),
            DateOnly.FromDateTime(DateTime.Now - TimeSpan.FromDays(360 * rnd.Next(5, 10)))
        )).ToList();

        public static RouteGroupBuilder MapGamesEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("games")
                    .WithParameterValidation()
                ;
            //Get / games
           group.MapGet("/", () => games);


            //Get /games/1
           group.MapGet("/{id}", (int id, GameStoreContext dbContext) =>
                {

                    Game? game = dbContext.Games.Find(id);
                    game.Genre = dbContext.Genres.Find(game.GenreId);


                    return game is null ? 
                        Results.NotFound() : 
                        Results.Ok(game.ToGameSummaryDto());

                })
                .WithName(getGameEndpointName);


            //Post /games  добавить
           group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
               {

               Game game = newGame.ToEntity();
               game.Genre = dbContext.Genres.Find(game.GenreId);

            dbContext.Games.Add(game);

                dbContext.SaveChanges();

                return Results.CreatedAtRoute(getGameEndpointName, new { id = game.Id }, game.ToGameDetailsDto());
            })
            .WithParameterValidation();


            //Put /games
           group.MapPut("/{id}", (int id, UpdateGameDto updatedGame, GameStoreContext dbContext) =>
            {
                var existingGame =  dbContext.Games.Find(id);

                if (existingGame is null)
                {
                    return Results.NotFound();
                }

               dbContext.Entry(existingGame)
                   .CurrentValues
                   .SetValues(updatedGame.ToEntity(id));

               dbContext.SaveChanges();
                return Results.NoContent();

            });



            //Delete /games/1
           group.MapDelete("/{id}", (int id) =>
            {
                int result = games.RemoveAll(game => game.Id == id);

                if (result > 0)
                    return Results.NoContent();

                return Results.NotFound();
            });


            app.MapGet("/", () => "Hello World!");


            return group;
        }
    }
}
