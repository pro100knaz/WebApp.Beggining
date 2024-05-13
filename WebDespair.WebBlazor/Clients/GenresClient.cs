using WebDespair.WebBlazor.Models;

namespace WebDespair.WebBlazor.Clients
{
	public class GenresClient
	{
		private readonly Genre[] _genres =
		[
			new Genre { Id = 1, Name = "Rpleplay" },
			new Genre { Id = 2, Name = "Fighting" },
			new Genre { Id = 3, Name = "Sport" },
			new Genre { Id = 4, Name = "Racing" },
			new Genre { Id = 5, Name = "Vr" },
			new Genre { Id = 6, Name = "Fighting" },
			new Genre { Id = 7, Name = "Hide and Seek" }

		];

		public Genre[] GetGenre() => _genres;
	}
}
