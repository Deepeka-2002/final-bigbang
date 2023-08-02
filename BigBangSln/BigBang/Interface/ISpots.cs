﻿using BigBang.Models;
using Microsoft.AspNetCore.Mvc;

namespace BigBang.Interface
{
    public interface ISpots
    {
        IEnumerable<NearbySpots> GetSpots();
        Task<NearbySpots> AddSpots([FromForm] NearbySpots Spots, IFormFile imageFile);
        Task<NearbySpots>? UpdateSpotById(NearbySpots Spots, IFormFile imageFile);
        Task<List<NearbySpots>?> DeleteSpotById(int id);
    }
}
