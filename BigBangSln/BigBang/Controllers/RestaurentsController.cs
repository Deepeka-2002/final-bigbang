using BigBang.Interface;
using BigBang.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace BigBang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurentsController : ControllerBase
    {
        private readonly IRestaurents IRestaurent;

        public RestaurentsController(IRestaurents IRestaurent)
        {
            this.IRestaurent = IRestaurent;
        }

        // GET: api/Customers

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Restaurents>>> GetRestaurents()
        {
            var customers = await IRestaurent.GetRestaurents();
            return Ok(customers);
        }

        [HttpGet("Filter/{packageId}")]
        public IEnumerable<Restaurents> Filterpackage(int packageId)
        {

            return IRestaurent.Filterpackage(packageId);

        }


        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Restaurents>>> UpdateRestaurent(int id, Restaurents apps)
        {

            try
            {
                var customer = await IRestaurent.UpdateRestaurentById(id, apps);
                return Ok(customer);
            }
            catch (ArithmeticException ex)
            {
                return NotFound(ex.Message);
            }
        }

        // POST: api/Customers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<List<Restaurents>>> AddRestaurent(Restaurents apps)
        {
            var customer = await IRestaurent.AddRestaurent(apps);
            return Ok(customer);
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
