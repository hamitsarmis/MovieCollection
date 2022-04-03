using Ardalis.Specification;
using Moq;
using MovieCollectionAPI.Entities;
using MovieCollectionAPI.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace MovieCollectionAPI.Tests
{
    public class GetMovieDetails
    {

        private readonly Mock<IReadRepository<Movie>> _mockMovieRepository;

        public GetMovieDetails()
        {
            _mockMovieRepository = new Mock<IReadRepository<Movie>>();
            var movie = new Movie();
            _mockMovieRepository.Setup(x => x.ListAsync(It.IsAny<ISpecification<Movie>>(), default)).
                ReturnsAsync(new List<Movie> { movie });
        }


        [Fact]
        public async Task NotBeNullIfMovieExists()
        {
            var result = await _mockMovieRepository.Object.GetByIdAsync(1);
            Assert.NotNull(result);
        }
    }
}