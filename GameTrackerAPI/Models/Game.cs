namespace GameTrackerAPI.Models
{
    public class Game
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Platform { get; set; } = null!;
        public string Genre { get; set; } = null!;
        public DateTime ReleaseDate { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
