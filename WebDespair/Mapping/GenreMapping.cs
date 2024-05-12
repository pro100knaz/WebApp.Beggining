using WebDespair.Dtos;
using WebDespair.Entities;

namespace WebDespair.Mapping
{
    public static class GenreMapping
    {
        public static GenreDto ToDto(this Genre genre)
        {
            return new GenreDto(genre.Id, genre.Name);
        }

        public static Genre ToEntity(this GenreDto genre) => new Genre() { Id = genre.id, Name = genre.Name };

    }
}
