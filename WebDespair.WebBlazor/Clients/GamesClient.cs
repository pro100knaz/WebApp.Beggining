using WebDespair.WebBlazor.Components;
using WebDespair.WebBlazor.Models;

namespace WebDespair.WebBlazor.Clients
{
	public class GamesClient(HttpClient httpClient)
	{
		//static HttpClient client = new HttpClient();
		//private static readonly Random rnd = new();

		//private static readonly Genre[] genres = new GenresClient(client).GetGenre();

		//private static readonly List<GameSummary> games = Enumerable.Range(1, 10).Select(i => new GameSummary()
		//{
		//	Id = i,
		//	Genre = genres[rnd.Next(0,6)].Name,
		//	Name = $"Total War {i}",
		//	Price = System.Math.Round((decimal)(i + (decimal)rnd.NextDouble() * rnd.Next(10, 80)), 2),
		//	ReleaseDate = new DateOnly(2000, 10, i),
		//}).ToList();

		public async Task<GameSummary[]>  GetGamesAsync()
		{
			return await httpClient.GetFromJsonAsync<GameSummary[]>("games") ?? [];
		}

		public async Task AddGameAsync(GameDetails game) => httpClient.PostAsJsonAsync("games", game);
		//{
		//	var genre = GetGenreById(game.GenreId);

		//	var GameSummary = new GameSummary
		//	{
		//		Id = games.Last().Id + 1,
		//		Name = game.Name,
		//		Genre = genre.Name,
		//		Price = game.Price,
		//		ReleaseDate = game.ReleaseDate,
		//	};


		//	games.Add(GameSummary);

		//}

		public async Task<GameDetails> GetGameAsync(int id) => await httpClient.GetFromJsonAsync<GameDetails>($"games/{id}") ??
		                                                  throw new Exception("Could not fing game");
		//{

		//	var game = GetGameSummaryById(id);

		//	var genre = genres.First(genre =>
		//		string.Equals(genre.Name, game.Genre, StringComparison.CurrentCultureIgnoreCase));

		//	return new GameDetails()
		//	{
		//		Id = game.Id,
		//		Name = game.Name,
		//		GenreId = genre.Id.ToString(),
		//		Price = game.Price,
		//		ReleaseDate = game.ReleaseDate
		//	};
		//}

		public async Task UpdateGameAsync(GameDetails updatedGame) =>
			await httpClient.PutAsJsonAsync($"games/{updatedGame.Id}", updatedGame);
		//{
		//	var genre = GetGenreById(updatedGame.GenreId);
		//	GameSummary existingGame = GetGameSummaryById(updatedGame.Id);

		//	existingGame.Name = updatedGame.Name;
		//	existingGame.Genre = genre.Name;
		//	existingGame.Price = updatedGame.Price;
		//	existingGame.ReleaseDate = updatedGame.ReleaseDate;
		//}


		public async Task DeleteGameAsync(int id) => await httpClient.DeleteAsync($"games/{id}");
		//{
		//	var game = GetGameSummaryById(id);
		//	games.Remove(game);
		//}

		/************************************************************************************************************************/

		//private GameSummary GetGameSummaryById(int id)
		//{
		//	var game =	games.Find(game => game.Id == id) ?? throw new ArgumentOutOfRangeException(nameof(id),"Игра с таким ид не существует");
		//	return game;
		//}

		//private Genre GetGenreById(string? id)
		//{
		//	ArgumentException.ThrowIfNullOrWhiteSpace(id);
		//	var genre = genres.First(genre => genre.Id == int.Parse(id));
		//	return genre;
		//}

	}
}
