using BigBang.Interface;
using BigBang.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigBang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotels IHot;

        public HotelsController(IHotels IHot)
        {
            this.IHot = IHot;
        }

        // GET: api/Customers

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hotels>>> GetHotels()
        {
            var customers = await IHot.GetHotels();
            return Ok(customers);
        }




        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hotels>> Post([FromForm] Hotels hotels, IFormFile imageFile)
        {

            try
            {
                var createdHotel = await IHot.AddHotel(hotels, imageFile);
                return CreatedAtAction("Get", new { id = createdHotel.HotelId }, createdHotel);

            }
            catch (ArgumentException ex)
            {
                ModelState.AddModelError("", ex.Message);
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
