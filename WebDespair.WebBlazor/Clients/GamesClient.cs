using WebDespair.WebBlazor.Models;

namespace WebDespair.WebBlazor.Clients
{
	public class GamesClient
	{
		private static readonly Random rnd = new();

		private readonly List<GameSummary> games = Enumerable.Range(1, 10).Select(i => new GameSummary()
		{
			Id = i,
			Genre = $"Genre {i}",
			Name = $"Total War {i}",
			Price = System.Math.Round((decimal)(i + (decimal)rnd.NextDouble() * rnd.Next(10, 80)), 2),
			ReleaseDate = new DateOnly(2000, 10, i),
		}).ToList();

		public GameSummary[] GetGames()
		{
			Thread.Sleep(2000);
			return [.. games];
		}
	}
}
