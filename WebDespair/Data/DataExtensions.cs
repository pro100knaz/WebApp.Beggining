using Microsoft.EntityFrameworkCore;
using WebDespair.Entities;

namespace WebDespair.Data
{
    public static class DataExtensions
    {
        public static void MigrateDb(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<GameStoreContext>();
            dbContext.Database.Migrate();

        }
    }
}
