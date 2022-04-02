using MovieCollectionAPI.Entities;

namespace MovieCollectionAPI.Interfaces
{
    public interface ITokenService
    {
        Task<string> CreateToken(AppUser user);
    }
}
