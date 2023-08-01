using System.ComponentModel.DataAnnotations;

namespace BigBang.Models
{
    public class NearbySpots
    {
        [Key]
        public int SpotId { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Description { get; set; }= string.Empty; 

        public string Location { get; set; } = string.Empty;
        public string Spotsimg { get; set; } = string.Empty;

        public int PackageId { get; set; }  
        public TourPackage? tourpackage { get; set; }
    }
}
