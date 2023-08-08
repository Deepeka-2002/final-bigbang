using BigBang.Interface;
using BigBang.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BigBang.Services
{
    public class RestaurentsService : IRestaurents
    {
        public TravelDbContext _Context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public RestaurentsService(TravelDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _Context = context;
            _webHostEnvironment = webHostEnvironment;
        }


        public IEnumerable<Restaurents> GetRestaurents()
        {
            var apps = _Context.restaurents.ToList();
            return apps;
        }
        public IEnumerable<Restaurents> GetRestaurentById(int id)
        {
            List<Restaurents> restaurents = _Context.restaurents.Where(x => x.RestaurentId == id).ToList();
            return restaurents;
        }

        public IEnumerable<Restaurents> Filterpackage(int packageId)
        {
            List<Restaurents> restaurents = _Context.restaurents.Where(x => x.PackageId == packageId).ToList();
            return restaurents;
        }



        public async Task<Restaurents> AddRestaurent([FromForm] Restaurents restaurents, IFormFile imageFile)
        {
            if (imageFile == null || imageFile.Length == 0)
            {
                throw new ArgumentException("Invalid file");
            }

            var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Restaurents");
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
            var filePath = Path.Combine(uploadsFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(stream);
            }

            restaurents.RestaurentImg = fileName;

            _Context.restaurents.Add(restaurents);
            await _Context.SaveChangesAsync();
            return restaurents;
        }
        public async Task<Restaurents>? UpdateRestaurentById(Restaurents restaurents, IFormFile imageFile)
        {

            var existingHotel = await _Context.restaurents.FindAsync(restaurents.RestaurentId);

            if (existingHotel == null)
            {
                throw new ArgumentException("Restaurent not found");
            }

            existingHotel.RestaurentName = restaurents.RestaurentName;

            if (imageFile != null && imageFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Restaurents");
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(imageFile.FileName);
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(stream);
                }

                existingHotel.RestaurentImg = fileName;

            }

            await _Context.SaveChangesAsync();

            return existingHotel;
        }


        public async Task<List<Restaurents>?> DeleteRestaurentById(int id)
        {
            var customer = await _Context.restaurents.FindAsync(id);
            if (customer is null)
            {
                throw new ArithmeticException("Invalid  id to delete");

            }
            _Context.Remove(customer);
            await _Context.SaveChangesAsync();
            return await _Context.restaurents.ToListAsync();
        }
    }
}
