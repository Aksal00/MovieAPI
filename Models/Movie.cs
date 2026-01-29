namespace MovieAPI.Models
{
    public class Movie
    {
        public int Id { get; set; }
        public string ImdbId { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string? Year { get; set; }
        public string? Type { get; set; }
        public string? Poster { get; set; }
    }
}
