using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCollectionAPI.DTOs;
using MovieCollectionAPI.Entities;
using MovieCollectionAPI.Interfaces;

namespace MovieCollectionAPI.Controllers
{
    [Authorize]
    public class MovieCollectionController : BaseApiController
    {

        private readonly IMapper _mapper;
        private readonly IMovieCollectionService _movieCollectionService;

        public MovieCollectionController(IMapper mapper, IMovieCollectionService movieCollectionService)
        {
            _mapper = mapper;
            _movieCollectionService = movieCollectionService;
        }

        [HttpPost("create-moviecollection")]
        public async Task<MovieCollectionDto> CreateMovieCollection(MovieCollectionDto movieDto)
        {
            var userid = User.GetUserId();
            var movieCollection = _mapper.Map<MovieCollection>(movieDto);
            movieCollection.UserId = userid;

            var result = await _movieCollectionService.CreateMovieCollection(movieCollection);
            return _mapper.Map<MovieCollectionDto>(result);
        }

        [HttpPost("update-moviecollection")]
        public async Task UpdateMovieCollection(MovieCollectionDto movieDto)
        {
            await _movieCollectionService.UpdateMovieCollection(_mapper.Map<MovieCollection>(movieDto));
        }

        [HttpPost("delete-moviecollection")]
        public async Task DeleteMovieCollection(MovieCollectionDto movieDto)
        {
            await _movieCollectionService.DeleteMovieCollection(_mapper.Map<MovieCollection>(movieDto));
        }

        [HttpPost("get-moviecollection")]
        [AllowAnonymous]
        public async Task<MovieCollectionDto> GetMovieCollection(MovieCollectionDto movieDto)
        {
            var result = await _movieCollectionService.GetMovieCollection(_mapper.Map<MovieCollection>(movieDto));
            return _mapper.Map<MovieCollectionDto>(result);
        }

        [HttpPost("removefrom-moviecollection")]
        public async Task RemoveFromMovieCollection([FromQuery] int movieCollectionID, [FromBody] MovieDto movieDto)
        {
            await _movieCollectionService.RemoveFromMovieCollection(new MovieCollection { Id = movieCollectionID }, _mapper.Map<Movie>(movieDto));
        }

        [HttpPost("addto-moviecollection")]
        public async Task AddToMovieCollection([FromQuery] int movieCollectionID, [FromBody] MovieDto movieDto)
        {
            await _movieCollectionService.AddToMovieCollection(new MovieCollection { Id = movieCollectionID }, _mapper.Map<Movie>(movieDto));
        }

    }
}
