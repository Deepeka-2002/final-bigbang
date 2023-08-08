using BigBang.Interface;
using BigBang.Models;
using BigBang.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BigBang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackage IPack;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public PackageController(IPackage IPack, IWebHostEnvironment webHostEnvironment)
        {
            this.IPack = IPack;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/Customers

        [HttpGet]
        public IActionResult GetTourPackages()
        {
            var images = IPack.GetTourPackages();
            if (images == null)
            {
                return NotFound();
            }

            var imageList = new List<TourPackage>();
            foreach (var image in images)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Packages");
                var filePath = Path.Combine(uploadsFolder, image.PackageImg);

                var imageBytes = System.IO.File.ReadAllBytes(filePath);
                var tourData = new TourPackage
                {
                    PackageId = image.PackageId,
                   
                    Destination = image.Destination,
                    PriceForAdult = image.PriceForAdult,
                    PriceForChild = image.PriceForChild,
                    Days = image.Days,
                    Description = image.Description,
                    UserId = image.UserId,
                    PackageImg = Convert.ToBase64String(imageBytes)
                };
                imageList.Add(tourData);
            }

            return new JsonResult(imageList);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TourPackage>> GetPackageById(int id)
        {
            try
            {
                var customer = await IPack.GetPackageById( id);
                return Ok(customer);
            }

            catch (ArithmeticException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("last-package-id")]
        public async Task<ActionResult<int>> GetLastInsertedPackageId()
        {
            try
            {
                var lastPackageId = await IPack.GetLastInsertedPackageId();
                return Ok(lastPackageId);
            }
            catch (Exception ex)
            {
                // Log the error
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("image")]
        public async Task<ActionResult> Post([FromForm] TourPackage tourpackage, IFormFile imageFile)
        {

            try
            {
                var createdHotel = await IPack.AddTourPackage(tourpackage, imageFile);
                
                return CreatedAtAction("Post",  createdHotel);

            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TourPackage>> Put(int id, [FromForm] TourPackage tourpackage, IFormFile imageFile)
        {
            try
            {
                tourpackage.PackageId = id;
                var updatedTour = await IPack.UpdateTourPackageById(tourpackage, imageFile);
                return Ok(updatedTour);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }


        // DELETE: api/Customers/5

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<TourPackage>>> DeleteTourPackageById(int id)
        {
            try
            {
                var customer = await IPack.DeleteTourPackageById(id);
                return Ok(customer);
            }
            catch (ArithmeticException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
