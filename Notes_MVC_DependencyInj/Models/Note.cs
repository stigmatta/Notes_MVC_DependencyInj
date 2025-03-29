using System.ComponentModel.DataAnnotations;

namespace Notes_MVC_DependencyInj.Models
{
    public class Note
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; } = null!;
        [Required]
        public string Text { get; set; } = null!;
        [Required]
        public DateTime CreatedAt { get; set; }
        public string? Tags { get; set; }
    }
}
