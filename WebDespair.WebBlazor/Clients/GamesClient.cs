using WebDespair.WebBlazor.Models;

namespace WebDespair.WebBlazor.Clients
{
	public class GamesClient
	{
		private static readonly Random rnd = new();

		private static readonly Genre[] genres = new GenresClient().GetGenre();

		private static readonly List<GameSummary> games = Enumerable.Range(1, 10).Select(i => new GameSummary()
		{
			Id = i,
			Genre = genres[rnd.Next(0,6)].Name,
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
			var genre = GetGenreById(game.GenreId);

			var GameSummary = new GameSummary
			{
				Id = games.Last().Id + 1,
				Name = game.Name,
				Genre = genre.Name,
				Price = game.Price,
				ReleaseDate = game.ReleaseDate,
			};


			games.Add(GameSummary);

		}

		public GameDetails GetGame(int id)
		{

			var game = GetGameSummaryById(id);

			var genre = genres.First(genre =>
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

		public void UpdateGame(GameDetails updatedGame)
		{
			var genre = GetGenreById(updatedGame.GenreId);
			GameSummary existingGame = GetGameSummaryById(updatedGame.Id);

			existingGame.Name = updatedGame.Name;
			existingGame.Genre = genre.Name;
			existingGame.Price = updatedGame.Price;
			existingGame.ReleaseDate = updatedGame.ReleaseDate;
		}


		public void DeleteGame(int id)
		{
			var game = GetGameSummaryById(id);
			games.Remove(game);
		}

		/************************************************************************************************************************/

		private GameSummary GetGameSummaryById(int id)
		{
			var game =	games.Find(game => game.Id == id) ?? throw new ArgumentOutOfRangeException(nameof(id),"Игра с таким ид не существует");
			return game;
		}

		private Genre GetGenreById(string? id)
		{
			ArgumentException.ThrowIfNullOrWhiteSpace(id);
			var genre = genres.First(genre => genre.Id == int.Parse(id));
			return genre;
		}

	}
}
