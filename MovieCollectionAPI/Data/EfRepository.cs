using Ardalis.Specification.EntityFrameworkCore;
using MovieCollectionAPI.Interfaces;

namespace MovieCollectionAPI.Data
{
    public class EfRepository<T> : RepositoryBase<T>, IReadRepository<T>, IRepository<T> where T : class, IAggregateRoot
    {
        public EfRepository(DataContext dbContext) : 
            base(dbContext)
        {
        }
    }
}
