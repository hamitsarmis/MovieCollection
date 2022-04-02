using Microsoft.AspNetCore.Identity;
using MovieCollectionAPI.Interfaces;

namespace MovieCollectionAPI.Entities
{
    public class AppUser : IdentityUser<int>, IAggregateRoot
    {

        public AppUser()
        {
            MovieCollections = new HashSet<MovieCollection>();
        }

        public DateTime Created { get; set; } = DateTime.Now;

        public ICollection<AppUserRole> UserRoles { get; set; }

        public virtual ICollection<MovieCollection> MovieCollections { get; set; }

    }
}
