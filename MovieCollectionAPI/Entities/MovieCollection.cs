using MovieCollectionAPI.Interfaces;

namespace MovieCollectionAPI.Entities
{
    public class MovieCollection : IAggregateRoot
    {

        public MovieCollection()
        {
            CollectionMovies = new HashSet<CollectionMovie>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? UserId { get; set; }
        public virtual AppUser User { get; set; }
        public virtual ICollection<CollectionMovie> CollectionMovies { get; set; }
    }
}
