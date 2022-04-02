using AutoMapper;
using MovieCollectionAPI.DTOs;
using MovieCollectionAPI.Entities;

namespace MovieCollectionAPI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<MovieDto, Movie>();
            CreateMap<Movie, MovieDto>();
            CreateMap<AppUser, UserDto>();
            CreateMap<UserDto, AppUser>();
            CreateMap<MovieCollectionDto, MovieCollection>();
            CreateMap<MovieCollection, MovieCollectionDto>();            
        }
    }
}
