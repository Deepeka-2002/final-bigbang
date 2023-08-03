using BigBang.Interface;
using BigBang.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BigBang.Services
{
    public class SpotsService : ISpots
    {
        public TravelDbContext _Context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public SpotsService(TravelDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _Context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        public IEnumerable<NearbySpots> GetSpots()
        {
            var apps =  _Context.nearbyspots.ToList();
            return apps;
        }

        public IEnumerable<NearbySpots> Filterpackage(int packageId)
        {
            List<NearbySpots> spots = _Context.nearbyspots.Where(x => x.PackageId == packageId).ToList();
            return spots;
        }


        public async Task<NearbySpots> AddSpots([FromForm] NearbySpots Spots, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Spots");
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

           Spots.Spotsimg = fileName;

            _Context.nearbyspots.Add(Spots);
            await _Context.SaveChangesAsync();
            return Spots;
        }


        public async Task<NearbySpots>? UpdateSpotById(NearbySpots Spots, IFormFile imageFile)
        {

            var existingHotel = await _Context.nearbyspots.FindAsync(Spots.SpotId);

            if (existingHotel == null)
            {
                throw new ArgumentException("Hotel not found");
            }

            existingHotel.Name = Spots.Name;

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Spots");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                existingHotel.Spotsimg = fileName;

            }

            await _Context.SaveChangesAsync();

            return existingHotel;
        }


        public async Task<List<NearbySpots>?> DeleteSpotById(int id)
        {
            var customer = await _Context.nearbyspots.FindAsync(id);
            if (customer is null)
            {
                throw new ArithmeticException("Invalid  id to delete");

            }
            _Context.Remove(customer);
            await _Context.SaveChangesAsync();
            return await _Context.nearbyspots.ToListAsync();
        }
    }
}
