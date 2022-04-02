using Ardalis.Specification;

namespace MovieCollectionAPI.Interfaces
{
    public interface IReadRepository<T> : IReadRepositoryBase<T> where T : class, IAggregateRoot
    {
    }
}
