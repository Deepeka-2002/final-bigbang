using BigBang.Models;
using Microsoft.AspNetCore.Mvc;

namespace BigBang.Interface
{
    public interface IHotels
    {
        IEnumerable<Hotels> GetHotels();
        IEnumerable<Hotels> GetHotelById(int id);
        Task<Hotels> AddHotel([FromForm] Hotels hotels, IFormFile imageFile);
        IEnumerable<Hotels> Filterpackage(int packageId);
        Task<Hotels>? UpdateHotelById(Hotels hotels, IFormFile imageFile);
        Task<List<Hotels>?> DeleteHotelById(int id);
    }
}
