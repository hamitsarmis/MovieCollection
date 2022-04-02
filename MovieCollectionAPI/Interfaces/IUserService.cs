using MovieCollectionAPI.Entities;

namespace MovieCollectionAPI.Interfaces
{
    public interface IUserService
    {
        Task<AppUser> CreateUser(AppUser user);

        Task UpdateUser(AppUser user);

        Task DeleteUser(AppUser user);

    }
}
