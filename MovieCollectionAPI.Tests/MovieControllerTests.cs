using AutoMapper;
using Moq;
using MovieCollectionAPI.Controllers;
using MovieCollectionAPI.DTOs;
using MovieCollectionAPI.Entities;
using MovieCollectionAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Xunit;

namespace MovieCollectionAPI.Tests
{
    public class MovieControllerTests
    {
        private MovieController getController(int authenticatedUserId)
        {
            var mockService = new Mock<IMovieService>();
            mockService.Setup(x => x.GetMovies()).
                ReturnsAsync(new List<Movie>());
            mockService.Setup(x => x.CreateMovie(It.IsAny<Movie>())).
                ReturnsAsync(new Movie() { Id = 1 });

            return new MovieController(mockService.Object, CreationalHelpers.GetMapper());
        }

        [Fact]
        public void Get_Movies_Should_Return_Not_Null()
        {
            var controller = getController(0);
            var actionResult = controller.GetMovies();
            Assert.NotNull(actionResult.Result);
        }

        [Fact]
        public void Returns_Object_With_Id_When_Movie_Created()
        {
            var controller = getController(1);
            var actionResult = controller.CreateMovie(new MovieDto());
            Assert.NotNull(actionResult.Result);
        }

        [Fact]
        public void Returns_Ok_When_Movie_Updated()
        {
            var controller = getController(1);
            var actionResult = controller.UpdateMovie(new MovieDto());
            Assert.NotNull(actionResult.Result as Microsoft.AspNetCore.Mvc.OkResult);
        }

        [Fact]
        public void Returns_Ok_When_Movie_Deleted()
        {
            var controller = getController(1);
            var actionResult = controller.DeleteMovie(new MovieDto());
            Assert.NotNull(actionResult.Result as Microsoft.AspNetCore.Mvc.OkResult);
        }

    }
}
