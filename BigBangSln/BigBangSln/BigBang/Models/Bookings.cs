using System.ComponentModel.DataAnnotations;

namespace BigBang.Models
{
    public class Bookings
    {
        [Key]

        public int BookingId { get; set; }

        public string Name { get; set; }   
        public string Email { get; set; }
        public DateTime CheckIn{ get; set; } 
        public DateTime CheckOut { get; set; }
        public int Adult { get; set; }

        public int Child { get; set; }

        public int PackageId { get; set; }
        public TourPackage? TourPackage { get; set; }

    }
}
