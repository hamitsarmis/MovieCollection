using Microsoft.AspNetCore.Identity;
using MovieCollectionAPI.Interfaces;

namespace MovieCollectionAPI.Entities
{
    public class AppRole : IdentityRole<int>, IAggregateRoot
    {
        public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
