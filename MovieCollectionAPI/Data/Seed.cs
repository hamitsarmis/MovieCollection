using Microsoft.AspNetCore.Identity;
using MovieCollectionAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace MovieCollectionAPI.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager,
            RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            List<AppUser> users = new List<AppUser>();
            users.Add(new AppUser
            {
                Email = $"dextermorgan@someaddress.com",
                PhoneNumber = "+90 (595)-773-0001",
                Created = DateTime.Now
            });

            users.Add(new AppUser
            {
                Email = $"harrymorgan@someaddress.com",
                PhoneNumber = "+90 (595)-773-0002",
                Created = DateTime.Now
            });

            users.Add(new AppUser
            {
                Email = $"willsmith@someaddress.com",
                PhoneNumber = "+90 (595)-773-0102",
                Created = DateTime.Now
            });

            users.Add(new AppUser
            {
                Email = $"sharonstone@someaddress.com",
                PhoneNumber = "+90 (595)-773-0802",
                Created = DateTime.Now
            });

            users.Add(new AppUser
            {
                Email = $"brucewills@someaddress.com",
                PhoneNumber = "+90 (595)-773-0002",
                Created = DateTime.Now
            });

            var roles = new List<AppRole>
            {
                new AppRole{Name = "Member"},
                new AppRole{Name = "Admin"}
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                user.UserName = user.Email.Substring(0, user.Email.IndexOf("@"));
                await userManager.CreateAsync(user, "Passw0rd");
                await userManager.AddToRoleAsync(user, "Member");
            }

            var admin = new AppUser
            {
                UserName = "admin"
            };

            await userManager.CreateAsync(admin, "Passw0rd");
            await userManager.AddToRolesAsync(admin, new[] { "Admin", "Member" });
        }

        internal static async Task SeedMovies(DataContext context)
        {

            if (await context.Movies.AnyAsync()) return;

            await context.Movies.AddAsync(new Movie
            {
                Description = "With Spider-Man's identity now revealed, Peter asks Doctor Strange for help. When a spell goes wrong, dangerous foes from other worlds start to appear, forcing Peter to discover what it truly means to be Spider-Man.",
                ImagePath = "https://m.media-amazon.com/images/M/MV5BNzgwNTVjYWQtNTY3YS00NzIzLTg1ZDAtYTA5MDNkNWZhZjA5XkEyXkFqcGdeQXVyODk4OTc3MTY@._V1_FMjpg_UX1280_.jpg",
                ImdbScore = 8.5M,
                Name = "Spider-Man: No Way Home"
            });
            await context.Movies.AddAsync(new Movie
            {
                Description = "When the menace known as the Joker wreaks havoc and chaos on the people of Gotham, Batman must accept one of the greatest psychological and physical tests of his ability to fight injustice.",
                ImagePath = "https://m.media-amazon.com/images/M/MV5BMjA5ODU3NTI0Ml5BMl5BanBnXkFtZTcwODczMTk2Mw@@._V1_FMjpg_UX1280_.jpg",
                ImdbScore = 9.1M,
                Name = "The Dark Knight"
            });
            await context.Movies.AddAsync(new Movie
            {
                Description = "The aging patriarch of an organized crime dynasty in postwar New York City transfers control of his clandestine empire to his reluctant youngest son.",
                ImagePath = "https://m.media-amazon.com/images/M/MV5BM2YzMDE1NDUtMTc2Zi00OTZhLWFiOTAtN2I4YmMyMzE2MzNjXkEyXkFqcGdeQXVyNjUwNzk3NDc@._V1_FMjpg_UX762_.jpg",
                ImdbScore = 9.2M,
                Name = "The Godfather"
            });
            await context.Movies.AddAsync(new Movie
            {
                Description = "Two imprisoned men bond over a number of years, finding solace and eventual redemption through acts of common decency.",
                ImagePath = "https://m.media-amazon.com/images/M/MV5BNjQ2NDA3MDcxMF5BMl5BanBnXkFtZTgwMjE5NTU0NzE@._V1_QL75_UX1000_CR0,94,1000,563_.jpg",
                ImdbScore = 9.3M,
                Name = "The Shawshank Redemption"
            });
            await context.Movies.AddAsync(new Movie
            {
                Description = "Eight years after the Joker's reign of anarchy, Batman, with the help of the enigmatic Catwoman, is forced from his exile to save Gotham City from the brutal guerrilla terrorist Bane.",
                ImagePath = "https://m.media-amazon.com/images/M/MV5BNzgwNTVjYWQtNTY3YS00NzIzLTg1ZDAtYTA5MDNkNWZhZjA5XkEyXkFqcGdeQXVyODk4OTc3MTY@._V1_FMjpg_UX1280_.jpg",
                ImdbScore = 8.4M,
                Name = "The Dark Knight Rises"
            });
            await context.SaveChangesAsync();
        }

        internal static async Task SeedCollections(DataContext context)
        {
            if (await context.MovieCollections.AnyAsync()) return;

            var movieCollections = new List<MovieCollection>();
            movieCollections.Add(new MovieCollection { UserId = 1, Name = "my favourite movies" });
            movieCollections.Add(new MovieCollection { UserId = 1, Name = "already watched movies" });
            movieCollections.Add(new MovieCollection { UserId = 1, Name = "drama movies" });
            movieCollections.Add(new MovieCollection { UserId = 1, Name = "superb movies" });

            movieCollections.Add(new MovieCollection { UserId = 2, Name = "watched" });
            movieCollections.Add(new MovieCollection { UserId = 2, Name = "to watch" });
            movieCollections.Add(new MovieCollection { UserId = 2, Name = "dramas" });

            movieCollections.Add(new MovieCollection { UserId = 3, Name = "will never watch" });
            movieCollections.Add(new MovieCollection { UserId = 3, Name = "definitely watch" });
            movieCollections.Add(new MovieCollection { UserId = 3, Name = "maybe watch" });

            foreach (var item in movieCollections)
            {
                await context.AddAsync(item);
                await context.SaveChangesAsync();
            }

            foreach (var item in movieCollections)
            {
                await context.CollectionMovies.AddAsync(new CollectionMovie { MovieCollection = item, MovieId = 1 });
                await context.CollectionMovies.AddAsync(new CollectionMovie { MovieCollection = item, MovieId = 2 });
                await context.CollectionMovies.AddAsync(new CollectionMovie { MovieCollection = item, MovieId = 3 });
                await context.CollectionMovies.AddAsync(new CollectionMovie { MovieCollection = item, MovieId = 4 });
            }

            await context.SaveChangesAsync();

        }
    }
}
