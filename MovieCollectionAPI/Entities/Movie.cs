using MovieCollectionAPI.Interfaces;

namespace MovieCollectionAPI.Entities
{
    public class Movie : IAggregateRoot
    {

        public Movie()
        {
            CollectionMovies = new HashSet<CollectionMovie>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal ImdbScore { get; set; }
        public string ImagePath { get; set; }
        public virtual ICollection<CollectionMovie> CollectionMovies { get; set; }

    }
}
