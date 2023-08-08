using System.ComponentModel.DataAnnotations;

namespace BigBang.Models
{
    public class Restaurents
    {
        [Key]
        public int RestaurentId { get; set; }

        public string RestaurentName { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string RestaurentImg { get; set; } = string.Empty;
        public int PackageId { get; set; } 
        public TourPackage? tourpackage { get; set; }   

    }
}
