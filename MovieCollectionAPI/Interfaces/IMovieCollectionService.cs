using MovieCollectionAPI.Entities;

namespace MovieCollectionAPI.Interfaces
{
    public interface IMovieCollectionService
    {
        Task<MovieCollection> CreateMovieCollection(MovieCollection movie);

        Task UpdateMovieCollection(MovieCollection movie);

        Task DeleteMovieCollection(MovieCollection movie);

        Task<MovieCollection> GetMovieCollection(MovieCollection movieCollection);

        Task RemoveFromMovieCollection(MovieCollection movieCollection, Movie movie);

        Task AddToMovieCollection(MovieCollection movieCollection, Movie movie);

    }
}
