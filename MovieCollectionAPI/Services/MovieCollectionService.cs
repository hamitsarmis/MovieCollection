﻿using MovieCollectionAPI.Data;
using MovieCollectionAPI.Entities;
using MovieCollectionAPI.Interfaces;

namespace MovieCollectionAPI.Services
{
    public class MovieCollectionService : IMovieCollectionService
    {

        private readonly DataContext _dataContext;

        public MovieCollectionService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<MovieCollection> CreateMovieCollection(MovieCollection movie)
        {
            await _dataContext.MovieCollections.AddAsync(movie);
            await _dataContext.SaveChangesAsync();
            return movie;
        }

        public async Task DeleteMovieCollection(MovieCollection movie)
        {
            movie = await _dataContext.MovieCollections.FindAsync(movie.Id);
            _dataContext.MovieCollections.Remove(movie);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<MovieCollection> GetMovieCollection(MovieCollection movieCollection)
        {
            return await _dataContext.MovieCollections.FindAsync(movieCollection.Id);
        }

        public async Task RemoveFromMovieCollection(MovieCollection movieCollection, Movie movie)
        {
            var toRemove = _dataContext.CollectionMovies.First(c => c.MovieId == movie.Id && c.MovieCollectionId == movieCollection.Id);
            _dataContext.CollectionMovies.Remove(toRemove);
            await _dataContext.SaveChangesAsync();
        }

        public async Task AddToMovieCollection(MovieCollection movieCollection, Movie movie)
        {
            _dataContext.CollectionMovies.Add(new CollectionMovie { MovieId = movie.Id, MovieCollectionId = movieCollection.Id });
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateMovieCollection(MovieCollection movie)
        {
            var mv = await _dataContext.MovieCollections.FindAsync(movie.Id);
            _dataContext.Entry(mv).CurrentValues.SetValues(movie);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<MovieCollection>> GetMovieCollectionsOfUser(int userid)
        {
            return _dataContext.MovieCollections.Where(m => m.UserId == userid).ToList();
        }

        public async Task<List<Movie>> GetMoviesOfCollection(int collectionId)
        {
            var itemsToSelect = _dataContext.CollectionMovies.Where(m => m.MovieCollectionId == collectionId).
                Select(m => m.MovieId).ToList();
            return _dataContext.Movies.Where(m => itemsToSelect.Contains(m.Id)).ToList();
        }

        public List<MovieCollection> GetCollections()
        {
            return _dataContext.MovieCollections.ToList();
        }
    }
}
