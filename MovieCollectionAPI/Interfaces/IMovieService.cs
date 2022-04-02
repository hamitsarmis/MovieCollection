using MovieCollectionAPI.Entities;

namespace MovieCollectionAPI.Interfaces
{
    public interface IMovieService
    {
        Task<Movie> CreateMovie(Movie movie);

        Task UpdateMovie(Movie movie);

        Task DeleteMovie(Movie movie);

        Task<List<Movie>> GetMovies();
    }
}
