using MovieCollectionAPI.Interfaces;

namespace MovieCollectionAPI.Entities
{
    public class CollectionMovie : IAggregateRoot
    {
        public int Id { get; set; }
        public int? MovieCollectionId { get; set; }
        public int? MovieId { get; set; }
        public virtual MovieCollection MovieCollection { get; set; }
        public virtual Movie Movie { get; set; }
    }
}
