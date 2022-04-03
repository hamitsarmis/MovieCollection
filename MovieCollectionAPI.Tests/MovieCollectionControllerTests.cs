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
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MovieCollectionAPI.Tests.MovieCollectionControllerTests
{
    public class MovieCollectionControllerTests
    {

        private MovieCollectionController getController(int authenticatedUserId, int ownerUserId)
        {
            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            IMapper mapper = new Mapper(configuration);

            var mockService = new Mock<IMovieCollectionService>();
            mockService.Setup(x => x.CreateMovieCollection(It.IsAny<MovieCollection>())).
                ReturnsAsync(new MovieCollection { Id = 1, UserId = ownerUserId });
            mockService.Setup(x => x.GetMoviesOfCollection(It.IsAny<int>())).
                ReturnsAsync(new List<Movie>());


            var controller = new MovieCollectionController(mapper, mockService.Object);
            if (authenticatedUserId != 0)
            {
                controller.ControllerContext = new Microsoft.AspNetCore.Mvc.ControllerContext()
                {
                    HttpContext = new Microsoft.AspNetCore.Http.DefaultHttpContext()
                    {
                        User = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.Name, "user1"),
                        new Claim(ClaimTypes.NameIdentifier, authenticatedUserId.ToString()),
                        new Claim("custom-claim", "example claim value"),
                    }, "mock"))
                    }
                };
            }

            return controller;
        }

        [Fact]
        public void Returns_Object_With_Id_When_Collection_Created()
        {
            var controller = getController(authenticatedUserId: 1, ownerUserId: 1);
            var actionResult = controller.CreateMovieCollection(new MovieCollectionDto());
            Assert.Equal(1, actionResult.Result.Id);
        }

        [Fact]
        public void Returns_UnAuthorized_For_Another_User_On_Update()
        {
            var controller = getController(authenticatedUserId: 2, ownerUserId: 1);
            var actionResult = controller.UpdateMovieCollection(new MovieCollectionDto());
            Assert.NotNull(actionResult.Result as Microsoft.AspNetCore.Mvc.UnauthorizedResult);
        }

        [Fact]
        public void Returns_UnAuthorized_For_Another_User_On_Delete()
        {
            var controller = getController(authenticatedUserId: 2, ownerUserId: 1);
            var actionResult = controller.DeleteMovieCollection(new MovieCollectionDto());
            Assert.NotNull(actionResult.Result as Microsoft.AspNetCore.Mvc.UnauthorizedResult);
        }

        [Fact]
        public void Returns_UnAuthorized_For_Another_User_On_Remove_Movie_From_Collection()
        {
            var controller = getController(authenticatedUserId: 2, ownerUserId: 1);
            var actionResult = controller.RemoveFromMovieCollection(1, new MovieDto());
            Assert.NotNull(actionResult.Result as Microsoft.AspNetCore.Mvc.UnauthorizedResult);
        }

        [Fact]
        public void Returns_UnAuthorized_For_Another_User_On_Add_Movie_To_Collection()
        {
            var controller = getController(authenticatedUserId: 2, ownerUserId: 1);
            var actionResult = controller.AddToMovieCollection(1, new MovieDto());
            Assert.NotNull(actionResult.Result as Microsoft.AspNetCore.Mvc.UnauthorizedResult);
        }

        [Fact]
        public void Get_Associated_Movies_Of_A_Collection_Not_Null()
        {
            var controller = getController(authenticatedUserId: 0, ownerUserId: 1);
            var actionResult = controller.GetMoviesOfCollection(1);
            Assert.NotNull(actionResult.Result);
        }

    }
}
