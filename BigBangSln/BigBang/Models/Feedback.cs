using System.ComponentModel.DataAnnotations;

namespace BigBang.Models
{
    public class Feedback
    {
        [Key]
        public int FeedId { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
        public int Rating { get; set; } 

        public int UserId { get; set; } 
        public User? user { get; set; }

    }

}
