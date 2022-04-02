using Microsoft.AspNetCore.Identity;
using MovieCollectionAPI.Interfaces;

namespace MovieCollectionAPI.Entities
{
    public class AppUserRole : IdentityUserRole<int>, IAggregateRoot
    {
        public AppUser User { get; set; }
        public AppRole Role { get; set; }
    }
}
