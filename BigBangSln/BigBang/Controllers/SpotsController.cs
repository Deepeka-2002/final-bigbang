using BigBang.Interface;
using BigBang.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigBang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotsController : ControllerBase
    {
        private readonly ISpots ISpot;

        public SpotsController(ISpots ISpot)
        {
            this.ISpot = ISpot;
        }

        // GET: api/Customers

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NearbySpots>>> GetSpots()
        {
            var customers = await ISpot.GetSpots();
            return Ok(customers);
        }




        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NearbySpots>> Post([FromForm] NearbySpots spots, IFormFile imageFile)
        {

            try
            {
                var createdHotel = await ISpot.AddSpots(spots, imageFile);
                return CreatedAtAction("Get", new { id = createdHotel.SpotId }, createdHotel);

            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
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
