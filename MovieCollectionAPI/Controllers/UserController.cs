using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieCollectionAPI.Data;
using MovieCollectionAPI.DTOs;
using MovieCollectionAPI.Entities;
using MovieCollectionAPI.Interfaces;
using MovieCollectionAPI.Messages;

namespace MovieCollectionAPI.Controllers
{
    public class UserController : BaseApiController
    {
        private readonly ITokenService _tokenService;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(ITokenService tokenService, UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager, IUserService userService, IMapper mapper)
        {
            _tokenService = tokenService;
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _mapper = mapper;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest loginRequest)
        {
            var user = await _userManager.Users
                .SingleOrDefaultAsync(x => x.UserName == loginRequest.UserName);

            if (user == null) return Unauthorized("Bad username or password");

            var result = await _signInManager
                .CheckPasswordSignInAsync(user, loginRequest.Password, false);

            if (!result.Succeeded) return Unauthorized("Bad username or password");

            return new LoginResponse
            {
                Token = await _tokenService.CreateToken(user),
                UserName = user.UserName,
                UserId = user.Id
            };
        }

        [HttpPost("create-user")]
        public async Task<ActionResult<UserDto>> CreateUser(UserDto userData)
        {
            var result = await _userService.CreateUser(_mapper.Map<AppUser>(userData));
            return _mapper.Map<UserDto>(result);
        }

        [HttpPost("update-user")]
        public async Task UpdateUser(UserDto userData)
        {
            await _userService.UpdateUser(_mapper.Map<AppUser>(userData));
        }

        [HttpPost("delete-user")]
        public async Task DeleteUser(UserDto userData)
        {
            await _userService.DeleteUser(_mapper.Map<AppUser>(userData));
        }

    }
}
