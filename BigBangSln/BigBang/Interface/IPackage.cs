using BigBang.Models;
using Microsoft.AspNetCore.Mvc;

namespace BigBang.Interface
{
    public interface IPackage
    {

        Task<List<TourPackage>> GetTourPackages();
        Task<TourPackage> AddTourPackage([FromForm] TourPackage tourpackage, IFormFile imageFile);
        Task<TourPackage>? UpdateTourPackageById(TourPackage tourpackage, IFormFile imageFile);
        Task<List<TourPackage>?> DeleteTourPackageById(int id);
    }
}
