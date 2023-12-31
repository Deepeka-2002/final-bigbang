﻿using BigBang.Models;
using Microsoft.AspNetCore.Mvc;

namespace BigBang.Interface
{
    public interface IPackage
    {

        IEnumerable<TourPackage> GetTourPackages();
        Task<TourPackage>? GetPackageById(int id);
        Task<int> GetLastInsertedPackageId();
        Task<TourPackage> AddTourPackage([FromForm] TourPackage tourpackage, IFormFile imageFile);
        Task<TourPackage>? UpdateTourPackageById(TourPackage tourpackage, IFormFile imageFile);
        Task<List<TourPackage>?> DeleteTourPackageById(int id);
    }
}
