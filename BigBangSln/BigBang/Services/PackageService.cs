using BigBang.Interface;
using BigBang.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BigBang.Services
{
    public class PackageService : IPackage
    {
        public TravelDbContext _Context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PackageService(TravelDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _Context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        public IEnumerable<TourPackage> GetTourPackages()
        {
            var apps =  _Context.tourpackage.ToList();
            return apps;
        }

        public async Task<TourPackage?> GetPackageById(int id)
        {
            var customer = await _Context.tourpackage.FindAsync(id);
            if (customer is null)
            {
                throw new ArithmeticException("Invalid User Id");
            }
            return customer;
        }

        public async Task<int> GetLastInsertedPackageId()
        {
            try
            {
                var lastPackage = await _Context.tourpackage
                    .OrderByDescending(p => p.PackageId)
                    .FirstOrDefaultAsync();

                return lastPackage?.PackageId ?? 0;
            }
            catch (Exception ex)
            {
                // Log the error
                throw new Exception("Failed to get the last inserted package ID.", ex);
            }
        }
        public async Task<TourPackage> AddTourPackage([FromForm] TourPackage tourpackage, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Packages");
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            tourpackage.PackageImg = fileName;

            _Context.tourpackage.Add(tourpackage);
            await _Context.SaveChangesAsync();
            return tourpackage ;
        }


        public async Task<TourPackage>? UpdateTourPackageById(TourPackage tourpackage, IFormFile imageFile)
        {

            var existingHotel = await _Context.tourpackage.FindAsync(tourpackage.PackageId);

            if (existingHotel == null)
            {
                throw new ArgumentException("Hotel not found");
            }

            existingHotel.Days = tourpackage.Days;

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Packages");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                existingHotel.PackageImg = fileName;

            }

            await _Context.SaveChangesAsync();

            return existingHotel;
        }


        public async Task<List<TourPackage>?> DeleteTourPackageById(int id)
        {
            var customer = await _Context.tourpackage.FindAsync(id);
            if (customer is null)
            {
                throw new ArithmeticException("Invalid  id to delete");

            }
            _Context.Remove(customer);
            await _Context.SaveChangesAsync();
            return await _Context.tourpackage.ToListAsync();
        }
    }
}
