using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieCollectionAPI.DTOs;
using MovieCollectionAPI.Entities;
using MovieCollectionAPI.Interfaces;

namespace MovieCollectionAPI.Controllers
{
    [Authorize(Policy = "RequireAdminRole")]
    public class MovieController : BaseApiController
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;

        public MovieController(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService;
            _mapper = mapper;
        }

        [HttpPost("create-movie")]
        public async Task<MovieDto> CreateMovie(MovieDto movieDto)
        {
            var result = await _movieService.CreateMovie(_mapper.Map<Movie>(movieDto));
            return _mapper.Map<MovieDto>(result);
        }

        [HttpPost("update-movie")]
        public async Task<ActionResult> UpdateMovie(MovieDto movieDto)
        {
            await _movieService.UpdateMovie(_mapper.Map<Movie>(movieDto));
            return Ok();
        }

        [HttpPost("delete-movie")]
        public async Task<ActionResult> DeleteMovie(MovieDto movieDto)
        {
            await _movieService.DeleteMovie(_mapper.Map<Movie>(movieDto));
            return Ok();
        }

        [HttpPost("get-movies")]
        [AllowAnonymous]
        public async Task<List<MovieDto>> GetMovies()
        {
            var result = await _movieService.GetMovies();
            return result.Select(x => _mapper.Map<MovieDto>(x)).ToList();
        }

    }
}
