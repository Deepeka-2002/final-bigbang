using System.ComponentModel.DataAnnotations;

namespace BigBang.Models
{
    public class TourPackage
    {
        [Key]
        public int PackageId { get; set; } 

        public string Destination { get; set; } = string.Empty;
        public int PriceForAdult { get; set; }  
        public int PriceForChild { get; set; }
        public int Days { get; set; }
        public string Description { get; set; } = string.Empty;

        public string PackageImg { get; set; } = string.Empty;

        public int UserId { get; set; }
        public User? user { get; set; }

        public ICollection<Hotels>? hotels{ get; set; }
        public ICollection<Restaurents>? restaurents { get; set; }
        public ICollection<NearbySpots>? nearbyspots { get; set; }

        public ICollection<Bookings>? bookings { get; set; }

    }
}

