using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieCollectionAPI.Entities;

namespace MovieCollectionAPI.Data
{
    public class DataContext : IdentityDbContext<AppUser, AppRole, int,
        IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieCollection> MovieCollections { get; set; }
        public DbSet<CollectionMovie> CollectionMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            
            builder.Entity<AppUser>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();

            builder.Entity<AppRole>()
                .HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();

            builder.Entity<AppUser>()
                .HasMany(ur => ur.MovieCollections)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<MovieCollection>()
                .HasMany(ur => ur.CollectionMovies)
                .WithOne(u => u.MovieCollection)
                .HasForeignKey(ur => ur.MovieCollectionId)
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
