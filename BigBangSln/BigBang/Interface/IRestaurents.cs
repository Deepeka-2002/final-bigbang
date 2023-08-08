using BigBang.Models;
using Microsoft.AspNetCore.Mvc;

namespace BigBang.Interface
{
    public interface IRestaurents
    {
        IEnumerable<Restaurents> GetRestaurents();
        IEnumerable<Restaurents> GetRestaurentById(int id);
        Task<Restaurents> AddRestaurent([FromForm] Restaurents restaurents, IFormFile imageFile);
        IEnumerable<Restaurents> Filterpackage(int packageId);
        Task<Restaurents>? UpdateRestaurentById(Restaurents restaurents, IFormFile imageFile);
        Task<List<Restaurents>?> DeleteRestaurentById(int id);
    }
}
