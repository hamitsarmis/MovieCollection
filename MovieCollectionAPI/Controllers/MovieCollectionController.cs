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
        public async Task<ActionResult> UpdateMovieCollection(MovieCollectionDto movieDto)
        {
            var data = await _movieCollectionService.GetMovieCollection(_mapper.Map<MovieCollection>(movieDto));
            if (data?.UserId != User.GetUserId())
                return Unauthorized();
            await _movieCollectionService.UpdateMovieCollection(_mapper.Map<MovieCollection>(movieDto));
            return Ok();
        }

        [HttpPost("delete-moviecollection")]
        public async Task<ActionResult> DeleteMovieCollection(MovieCollectionDto movieDto)
        {
            var data = await _movieCollectionService.GetMovieCollection(_mapper.Map<MovieCollection>(movieDto));
            if (data?.UserId != User.GetUserId())
                return Unauthorized();
            await _movieCollectionService.DeleteMovieCollection(_mapper.Map<MovieCollection>(movieDto));
            return Ok();
        }

        [HttpPost("get-moviecollection")]
        [AllowAnonymous]
        public async Task<MovieCollectionDto> GetMovieCollection(MovieCollectionDto movieDto)
        {
            var result = await _movieCollectionService.GetMovieCollection(_mapper.Map<MovieCollection>(movieDto));
            return _mapper.Map<MovieCollectionDto>(result);
        }

        [HttpPost("get-collectionsofuser")]
        [AllowAnonymous]
        public async Task<List<MovieCollectionDto>> GetMovieCollectionsOfUser([FromQuery] int userid)
        {
            var result = await _movieCollectionService.GetMovieCollectionsOfUser(userid);
            return _mapper.Map<List<MovieCollectionDto>>(result);
        }

        [HttpPost("get-moviesofcollection")]
        [AllowAnonymous]
        public async Task<List<MovieDto>> GetMoviesOfCollection([FromQuery] int collectionId)
        {
            var result = await _movieCollectionService.GetMoviesOfCollection(collectionId);
            return _mapper.Map<List<MovieDto>>(result);
        }


        [HttpPost("removefrom-moviecollection")]
        public async Task RemoveFromMovieCollection([FromQuery] int collectionId, [FromBody] MovieDto movieDto)
        {
            await _movieCollectionService.RemoveFromMovieCollection(new MovieCollection { Id = collectionId }, _mapper.Map<Movie>(movieDto));
        }

        [HttpPost("addto-moviecollection")]
        public async Task AddToMovieCollection([FromQuery] int collectionId, [FromBody] MovieDto movieDto)
        {
            await _movieCollectionService.AddToMovieCollection(new MovieCollection { Id = collectionId }, _mapper.Map<Movie>(movieDto));
        }

    }
}
