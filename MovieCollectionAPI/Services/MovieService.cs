using AutoMapper;
using MovieCollectionAPI;
using MovieCollectionAPI.DTOs;
using MovieCollectionAPI.Entities;
using System.Linq;
using MovieCollectionAPI.Interfaces;

namespace MovieCollectionAPI.Services
{
    public class MovieService : IMovieService
    {

        private readonly IRepository<Movie> _movieRepository;

        public MovieService(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<Movie> CreateMovie(Movie movie)
        {
            return await _movieRepository.AddAsync(movie);
        }

        public async Task DeleteMovie(Movie movie)
        {
            await _movieRepository.DeleteAsync(movie);
        }

        public async Task UpdateMovie(Movie movie)
        {
            await _movieRepository.UpdateAsync(movie);
        }

        public async Task<List<Movie>> GetMovies()
        {
            return await _movieRepository.ListAsync();
        }
    }
}
