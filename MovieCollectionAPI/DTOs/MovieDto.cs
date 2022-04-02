namespace MovieCollectionAPI.DTOs
{
    public class MovieDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal ImdbScore { get; set; }
        public string ImagePath { get; set; }
    }
}
