using Microsoft.EntityFrameworkCore;

namespace BigBang.Models
{
    public class TravelDbContext : DbContext
    {
        public TravelDbContext(DbContextOptions<TravelDbContext> options) : base(options) { }

        public DbSet<User> user { get; set; }

        public DbSet<TourPackage> tourpackage { get; set; }
        public DbSet<Restaurents> restaurents { get; set; }
        public DbSet<NearbySpots> nearbyspots { get; set; }
        public DbSet<Hotels> hotels { get; set; }   
        public DbSet<Feedback> feedback { get; set; }
        public DbSet<Bookings> bookings { get; set; }

        public virtual DbSet<Imagetable> imagetable { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

    }
}