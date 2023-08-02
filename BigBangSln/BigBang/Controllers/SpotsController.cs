using BigBang.Interface;
using BigBang.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigBang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotsController : ControllerBase
    {

        private readonly ISpots ISpot;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public SpotsController(ISpots ISpot, IWebHostEnvironment webHostEnvironment)
        {
            this.ISpot = ISpot;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/Customers

        [HttpGet]
        public IActionResult GetSpots()
        {
            var images = ISpot.GetSpots();
            if (images == null)
            {
                return NotFound();
            }

            var imageList = new List<NearbySpots>();
            foreach (var image in images)
            {
                var uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Spots");
                var filePath = Path.Combine(uploadsFolder, image.Spotsimg);

                var imageBytes = System.IO.File.ReadAllBytes(filePath);
                var Data = new NearbySpots
                {
                    PackageId = image.PackageId,
                   
                    Name = image.Name,
                   
                    Location = image.Location,
                    Description = image.Description,
                    Spotsimg = Convert.ToBase64String(imageBytes)
                };

                imageList.Add(Data);

            }

            return new JsonResult(imageList);
        }




        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] NearbySpots spots, IFormFile imageFile)
        {

            try
            {
                var createdHotel = await ISpot.AddSpots(spots, imageFile);
                return CreatedAtAction("Post", createdHotel);

            }
            catch (ArgumentException ex)
            {
                
                return BadRequest(ModelState);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<NearbySpots>> Put(int id, [FromForm] NearbySpots spots, IFormFile imageFile)
        {
            try
            {
                spots.SpotId = id;
                var updatedTour = await ISpot.UpdateSpotById( spots, imageFile);
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
        public async Task<ActionResult<List<NearbySpots>>> DeleteSpotById(int id)
        {
            try
            {
                var customer = await ISpot.DeleteSpotById(id);
                return Ok(customer);
            }
            catch (ArithmeticException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
