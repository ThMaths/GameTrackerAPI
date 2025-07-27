using System.ComponentModel.DataAnnotations;

namespace GameTrackerAPI.DTOs
{
    public class GameDto
    {
        [Required]
        public string Title { get; set; } = null!;

        [Required]
        public string Platform { get; set; } = null!;

        [Required]
        public string Genre { get; set; } = null!;

        [Required]
        public DateTime ReleaseDate { get; set; }

        public bool IsCompleted { get; set; } = false;
    }
}
