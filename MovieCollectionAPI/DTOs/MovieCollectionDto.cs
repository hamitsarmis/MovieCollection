using MovieCollectionAPI.Entities;

namespace MovieCollectionAPI.DTOs
{
    public class MovieCollectionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public List<MovieDto> CollectionMovies { get; set; }
    }
}
