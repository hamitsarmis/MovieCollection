using MovieCollectionAPI.Entities;
using MovieCollectionAPI.Interfaces;

namespace MovieCollectionAPI.Services
{
    public class UserService : IUserService
    {

        private readonly IRepository<AppUser> _userRepository;

        public UserService(IRepository<AppUser> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<AppUser> CreateUser(AppUser user)
        {
            return await _userRepository.AddAsync(user);
        }

        public async Task DeleteUser(AppUser user)
        {
            await _userRepository.DeleteAsync(user);
        }

        public async Task UpdateUser(AppUser user)
        {
            await _userRepository.UpdateAsync(user);
        }
    }
}
