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


        public async Task<List<Restaurents>> GetRestaurents()
        {
            var apps = await _Context.restaurents.ToListAsync();
            return apps;
        }
        public async Task<Restaurents?> GetRestaurentById(int id)
        {
            var customer = await _Context.restaurents.FindAsync(id);
            if (customer is null)
            {
                throw new ArithmeticException("Invalid id");
            }
            return customer;
        }

        public IEnumerable<Restaurents> Filterpackage(int packageId)
        {
            List<Restaurents> restaurents = _Context.restaurents.Where(x => x.PackageId == packageId).ToList();
            return restaurents;
        }



        public async Task<List<Restaurents>> AddRestaurent(Restaurents apps)
        {

            _Context.restaurents.Add(apps);
            await _Context.SaveChangesAsync();
            return await _Context.restaurents.ToListAsync();
        }

        public async Task<List<Restaurents>?> UpdateRestaurentById(int id, Restaurents apps)
        {
            var customer = await _Context.restaurents.FindAsync(id);
            if (customer is null)
            {
                throw new ArithmeticException("Invalid  id to update details");
            }
            customer.RestaurentName = apps.RestaurentName;

            customer.Location = apps.Location;
            

            await _Context.SaveChangesAsync();
            return await _Context.restaurents.ToListAsync();
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
