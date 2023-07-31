using System.ComponentModel.DataAnnotations;

namespace BigBang.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; } 
        public string UserName { get; set; } = string.Empty;
        public string EmailId { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }= string.Empty;

        public ICollection<TourPackage>? tourpackage { get; set; }

        public ICollection<Feedback>? feedback { get; set; }
    }
}
