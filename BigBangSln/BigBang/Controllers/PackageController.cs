using BigBang.Interface;
using BigBang.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigBang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackage IPack;

        public PackageController(IPackage IPack)
        {
            this.IPack = IPack;
        }

        // GET: api/Customers

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TourPackage>>> GetTourPackages()
        {
            var customers = await IPack.GetTourPackages();
            return Ok(customers);
        }



        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("image")]
        public async Task<ActionResult> Post([FromForm] TourPackage tourpackage, IFormFile imageFile)
        {

            try
            {
                var createdHotel = await IPack.AddTourPackage(tourpackage, imageFile);
                //return CreatedAtAction("Get", new { id = createdHotel.PackageId }, createdHotel);
                return CreatedAtAction("Post",  createdHotel);

            }
            catch (ArgumentException ex)
            {
                //ModelState.AddModelError("", ex.Message);
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
