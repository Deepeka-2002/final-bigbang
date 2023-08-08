using BigBang.Interface;
using BigBang.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;

namespace BigBang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurentsController : ControllerBase
    {
        private readonly IRestaurents IRestaurent;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public RestaurentsController(IRestaurents IRestaurent, IWebHostEnvironment webHostEnvironment)
        {
            this.IRestaurent = IRestaurent;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/Customers

        [HttpGet]
        public IActionResult GetRestaurents()
        {
            var images = IRestaurent.GetRestaurents();
            if (images == null)
            {
                return NotFound();
            }

            var imageList = new List<Restaurents>();
            foreach (var image in images)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Restaurents");
                var filePath = Path.Combine(uploadsFolder, image.RestaurentImg);

                var imageBytes = System.IO.File.ReadAllBytes(filePath);
                var Data = new Restaurents
                {
                    PackageId = image.PackageId,

                    RestaurentName = image.RestaurentName,
                    Location = image.Location,
                    RestaurentId = image.RestaurentId,

                    RestaurentImg = Convert.ToBase64String(imageBytes)
                };

                imageList.Add(Data);
            }

            return new JsonResult(imageList);
        }

        [HttpGet("Filter/{packageId}")]
        public IActionResult Filterpackage(int packageId)
        {

            var images = IRestaurent.Filterpackage(packageId);
            if (images == null)
            {
                return NotFound();
            }

            var imageList = new List<Restaurents>();
            foreach (var image in images)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Restaurents");
                var filePath = Path.Combine(uploadsFolder, image.RestaurentImg);

                var imageBytes = System.IO.File.ReadAllBytes(filePath);
                var Data = new Restaurents
                {
                    PackageId = image.PackageId,

                    RestaurentName = image.RestaurentName,
                    Location = image.Location,
                    RestaurentId = image.RestaurentId,

                    RestaurentImg = Convert.ToBase64String(imageBytes)
                };

                imageList.Add(Data);
            }

            return new JsonResult(imageList);

        }
        [HttpGet("{id}")]
        public IActionResult GetRestaurentById(int id)
        {

            var images = IRestaurent.GetRestaurentById(id);
            if (images == null)
            {
                return NotFound();
            }

            var imageList = new List<Restaurents>();
            foreach (var image in images)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Restaurents");
                var filePath = Path.Combine(uploadsFolder, image.RestaurentImg);

                var imageBytes = System.IO.File.ReadAllBytes(filePath);
                var Data = new Restaurents
                {
                    PackageId = image.PackageId,

                    RestaurentName = image.RestaurentName,
                    Location = image.Location,
                    RestaurentId = image.RestaurentId,

                    RestaurentImg = Convert.ToBase64String(imageBytes)
                };

                imageList.Add(Data);
            }

            return new JsonResult(imageList);

        }

        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Restaurents>> Put(int id, [FromForm] Restaurents restaurents, IFormFile imageFile)
        {
            try
            {
                restaurents.RestaurentId = id;
                var updatedTour = await IRestaurent.UpdateRestaurentById(restaurents, imageFile);
                return Ok(updatedTour);
            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
                return BadRequest(ModelState);
            }
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] Restaurents restaurents, IFormFile imageFile)
        {

            try
            {
                var createdHotel = await IRestaurent.AddRestaurent(restaurents, imageFile);
                return CreatedAtAction("Post", createdHotel);

            }
            catch (ArgumentException ex)
            {

                return BadRequest(ModelState);
            }
        }

        // DELETE: api/Customers/5

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Restaurents>>> DeleteRestaurentById(int id)
        {
            try
            {
                var customer = await IRestaurent.DeleteRestaurentById(id);
                return Ok(customer);
            }
            catch (ArithmeticException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
