using BigBang.Interface;
using BigBang.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace BigBang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotels IHot;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HotelsController(IHotels IHot, IWebHostEnvironment webHostEnvironment)
        {
            this.IHot = IHot;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/Customers

        [HttpGet]
        public IActionResult GetHotels()
        {
            var images = IHot.GetHotels();
            if (images == null)
            {
                return NotFound();
            }

            var imageList = new List<Hotels>();
            foreach (var image in images)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Hotels");
                var filePath = Path.Combine(uploadsFolder, image.HotelImg);

                var imageBytes = System.IO.File.ReadAllBytes(filePath);
                var Data = new Hotels
                {
                    PackageId = image.PackageId,

                    HotelName = image.HotelName,
                    Location = image.Location,
                    HotelId = image.HotelId,
                   
                    HotelImg = Convert.ToBase64String(imageBytes)
                };

                imageList.Add(Data);
            }

            return new JsonResult(imageList);
        }

        [HttpGet("{id}")]
        public IActionResult GetHotelById(int id)
        {

            var images = IHot.GetHotelById(id);
            if (images == null)
            {
                return NotFound();
            }

            var imageList = new List<Hotels>();
            foreach (var image in images)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Hotels");
                var filePath = Path.Combine(uploadsFolder, image.HotelImg);

                var imageBytes = System.IO.File.ReadAllBytes(filePath);
                var Data = new Hotels
                {
                    PackageId = image.PackageId,

                    HotelName = image.HotelName,
                    Location = image.Location,
                    HotelId = image.HotelId,

                    HotelImg = Convert.ToBase64String(imageBytes)
                };

                imageList.Add(Data);
            }

            return new JsonResult(imageList);

        }

        [HttpGet("Filter/{packageId}")]
        public IActionResult Filterpackage(int packageId)
        {

            var images = IHot.Filterpackage(packageId);
            if (images == null)
            {
                return NotFound();
            }

            var imageList = new List<Hotels>();
            foreach (var image in images)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Hotels");
                var filePath = Path.Combine(uploadsFolder, image.HotelImg);

                var imageBytes = System.IO.File.ReadAllBytes(filePath);
                var Data = new Hotels
                {
                    PackageId = image.PackageId,

                    HotelName = image.HotelName,
                    Location = image.Location,
                    HotelId = image.HotelId,

                    HotelImg = Convert.ToBase64String(imageBytes)
                };

                imageList.Add(Data);
            }

            return new JsonResult(imageList);

        }


        [HttpPost]
        public async Task<ActionResult> Post([FromForm] Hotels hotels, IFormFile imageFile)
        {

            try
            {
                var createdHotel = await IHot.AddHotel(hotels, imageFile);
                return CreatedAtAction("Post", createdHotel);

            }
            catch (ArgumentException ex)
            {
              
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Hotels>> Put(int id, [FromForm] Hotels hotels, IFormFile imageFile)
        {
            try
            {
                hotels.HotelId = id;
                var updatedTour = await IHot.UpdateHotelById(hotels, imageFile);
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
        public async Task<ActionResult<List<Hotels>>> DeleteHotelById(int id)
        {
            try
            {
                var customer = await IHot.DeleteHotelById(id);
                return Ok(customer);
            }
            catch (ArithmeticException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
