using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MovieCollectionAPI.Tests
{
    internal static class CreationalHelpers
    {

        public static IMapper GetMapper()
        {
            var myProfile = new AutoMapperProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(myProfile));
            return new Mapper(configuration);
        }

        public static ControllerContext GetControllerContext(int authenticatedUserId)
        {
            return new Microsoft.AspNetCore.Mvc.ControllerContext()
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

    }
}
