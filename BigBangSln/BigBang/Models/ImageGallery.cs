using System.ComponentModel.DataAnnotations;

namespace BigBang.Models
{
    public class ImageGallery
    {
        [Key]
        public string Image { get; set; }
    }
}
