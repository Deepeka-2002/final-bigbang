using System.ComponentModel.DataAnnotations;

namespace BigBang.Models
{
    public class Hotels
    {
        [Key]

        public int HotelId { get; set; }

        public string HotelName { get; set; } = string.Empty;

        public string Location { get; set; } = string.Empty;
        public string HotelImg { get; set; } = string.Empty;

        public int PackageId { get; set; }
        public TourPackage? tourpackage { get; set; }
    }
}