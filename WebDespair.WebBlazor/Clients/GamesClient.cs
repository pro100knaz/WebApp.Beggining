using WebDespair.WebBlazor.Models;

namespace WebDespair.WebBlazor.Clients
{
	public class GamesClient
	{
		private static readonly Random rnd = new();

		private readonly Genre[] genres = new GenresClient().GetGenre();

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
			//Thread.Sleep(20000);
			return [.. games];
		}

		public void AddGame(GameDetails game)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(game.GenreId);
			var genre = genres.Single(genre => genre.Id == int.Parse(game.GenreId));

			var GameSummary = new GameSummary
			{
				Id = games.Last().Id ++,
				Name = game.Name,
				Genre = genre.Name,
				Price = game.Price,
				ReleaseDate = game.ReleaseDate,
			};


			games.Add(GameSummary);

		}

		public GameDetails GetGame(int id)
		{

			var game =	games.Find(game => game.Id == id) ?? throw new ArgumentOutOfRangeException(nameof(id),"Игра с таким ид не существует");

			var genre = genres.Single(genre =>
				string.Equals(genre.Name, game.Genre, StringComparison.CurrentCultureIgnoreCase));

			return new GameDetails()
			{
				Id = game.Id,
				Name = game.Name,
				GenreId = genre.Id.ToString(),
				Price = game.Price,
				ReleaseDate = game.ReleaseDate
			};
		}
	}
}
