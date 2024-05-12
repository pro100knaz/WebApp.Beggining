using Microsoft.EntityFrameworkCore;
using WebDespair.Dtos;
using WebDespair.Entities;

namespace WebDespair.Mapping
{
    public static class GameMapping
    {
        public static Game ToEntity(this CreateGameDto game) =>
            new()
            {
                Name = game.Name,
                //               Genre = dbContext.Genres.Find(game.GenreId),
                GenreId = game.GenreId,
                Price = game.Price,
                ReleaseDate = game.ReleaseDate
            }; 

        public static GameSummaryDto ToGameSummaryDto(this Game game) =>
            new(
                game.Id,
                game.Name,
                game.Genre!.Name,
                game.Price,
                game.ReleaseDate
            );

        public static GameDetailsDto ToGameDetailsDto(this Game game) =>
            new(
                game.Id,
                game.Name,
                game.Genre!.Id,
                game.Price,
                game.ReleaseDate
            );
    }
}
