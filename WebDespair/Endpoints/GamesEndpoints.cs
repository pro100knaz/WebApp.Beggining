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

                    return game is null ? 
                        Results.NotFound() : 
                        Results.Ok(game.ToGameSummaryDto());

                })
                .WithName(getGameEndpointName);


            //Post /games  добавить
           group.MapPost("/", (CreateGameDto newGame, GameStoreContext dbContext) =>
               {

               Game game = newGame.ToEntity();

                dbContext.Games.Add(game);
                dbContext.SaveChanges();

                return Results.CreatedAtRoute(getGameEndpointName, new { id = game.Id }, game.ToGameDetailsDto());
            })
            .WithParameterValidation();


            //Put /games
           group.MapPut("/{id}", (int id, UpdateGameDto updatedGame) =>
            {
                var game = games.Find(game => game.Id == id);

                var index = games.FindIndex(game => game.Id == id);

                if (index == -1)
                {
                    return Results.NotFound();
                }

                games[index] = new GameSummaryDto(
                    id,
                    updatedGame.Name is null ? game.Name : updatedGame.Name,
                    updatedGame.Genre is null ? game.Genre : updatedGame.Genre,
                    updatedGame.Price,
                    updatedGame.ReleaseDate
                );

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
