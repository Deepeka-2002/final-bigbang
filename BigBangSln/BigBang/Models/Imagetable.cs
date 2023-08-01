using System.ComponentModel.DataAnnotations;

namespace BigBang.Models
{
    public class Imagetable
    {
        [Key]

        public int Imgid { get; set; }
        public string? ImgName { get; set; }
    }
}
