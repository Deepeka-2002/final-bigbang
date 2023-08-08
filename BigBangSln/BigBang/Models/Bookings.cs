using System.ComponentModel.DataAnnotations;

namespace BigBang.Models
{
    public class Bookings
    {
        [Key]

        public int BookingId { get; set; }

        public string Name { get; set; }   
        public string Email { get; set; }
        public long Phone { get; set; }
        public DateTime CheckIn{ get; set; } 
        public DateTime CheckOut { get; set; }
        public int Adult { get; set; }

        public int Child { get; set; }

        public int Amount { get; set; }

        public int HotelId { get; set; }
        public Hotels? Hotels { get; set; }
        public int RestaurentId { get; set; }
        public Restaurents? Restaurents { get; set; }
        public int PackageId { get; set; }
        public TourPackage? TourPackage { get; set; }

    }
}
