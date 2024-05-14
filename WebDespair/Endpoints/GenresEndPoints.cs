using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using WebDespair.Data;
using WebDespair.Entities;
using WebDespair.Mapping;

namespace WebDespair.Endpoints
{
    public static class GenresEndPoints
    {
        public static RouteGroupBuilder MapGenresEndPoints(this WebApplication app)
        {
            /*
             *  var group = app.MapGroup("games")
                       .WithParameterValidation()
                   ;
               //Get / games
               group.MapGet("/", async (GameStoreContext dbContext) =>
                   await dbContext.Games
                       .Include(game => game.Genre)
                       .Select(game => game.ToGameSummaryDto()).AsNoTracking().ToListAsync());
             */
            var group = app.MapGroup("genres").WithParameterValidation();

            group.MapGet($"/", async (GameStoreContext dbContext) =>
                await dbContext.Genres.Select(genre => genre.ToDto()).AsNoTracking().ToListAsync());

            group.MapGet("/{id}", async (int id, GameStoreContext dbContext) =>
            {
                var x = await dbContext.Genres.FindAsync(id);
                if (x is null)
                {
                    Results.NotFound();
                    return null;
                }

                return x.ToDto();

            });

            return group;
        }
    }
}
