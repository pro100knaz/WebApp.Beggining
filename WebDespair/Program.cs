using WebDespair.Data;
using WebDespair.Dtos;
using WebDespair.Endpoints;

internal class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connString = builder.Configuration.GetConnectionString("GameStore");

        builder.Services.AddSqlite<GameStoreContext>(connString);


        var app = builder.Build();


        app.MapGamesEndpoints();
        app.MapGenresEndPoints();

        await app.MigrateDbAsync();


        app.Run();
    }
}
 