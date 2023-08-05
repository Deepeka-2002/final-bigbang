using BigBang.Interface;
using BigBang.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BigBang.Services
{
    public class HotelsService : IHotels
    {
       

            public TravelDbContext _Context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HotelsService(TravelDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _Context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        

            public IEnumerable<Hotels> GetHotels()
            {
                var apps = _Context.hotels.ToList();
                return apps;
            }

        public async Task<Hotels?> GetHotelById(int id)
        {
            var customer = await _Context.hotels.FindAsync(id);
            if (customer is null)
            {
                throw new ArithmeticException("Invalid id");
            }
            return customer;
        }

        public async Task<Hotels> AddHotel([FromForm] Hotels hotels, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Hotels");
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            hotels.HotelImg = fileName;

            _Context.hotels.Add(hotels);
            await _Context.SaveChangesAsync();
            return hotels;
        }

        public IEnumerable<Hotels> Filterpackage(int packageId)
        {
            List<Hotels> hotels = _Context.hotels.Where(x => x.PackageId == packageId).ToList();
            return hotels;
        }


        public async Task<Hotels>? UpdateHotelById(Hotels hotels, IFormFile imageFile)
        {

            var existingHotel = await _Context.hotels.FindAsync(hotels.PackageId);

            if (existingHotel == null)
            {
                throw new ArgumentException("Hotel not found");
            }

            existingHotel.HotelName = hotels.HotelName;

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Hotels");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                existingHotel.HotelImg = fileName;

            }

            await _Context.SaveChangesAsync();

            return existingHotel;
        }
            

            public async Task<List<Hotels>?> DeleteHotelById(int id)
            {
                var customer = await _Context.hotels.FindAsync(id);
                if (customer is null)
                {
                    throw new ArithmeticException("Invalid  id to delete");

                }
                _Context.Remove(customer);
                await _Context.SaveChangesAsync();
                return await _Context.hotels.ToListAsync();
            }
        }
}


