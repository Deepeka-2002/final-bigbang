using BigBang.Interface;
using BigBang.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigBang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBooking IBook;

        public BookingController(IBooking IBook)
        {
            this.IBook = IBook;
        }

        // GET: api/Customers

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bookings>>> GetAppointments()
        {
            var customers = await IBook.GetAppointments();
            return Ok(customers);
        }




        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Bookings>>> UpdateAppointment(int id, Bookings apps)
        {

            try
            {
                var customer = await IBook.UpdateAppointmentById(id, apps);
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
        public async Task<ActionResult<List<Bookings>>> AddAppointment(Bookings apps)
        {
            var customer = await IBook.AddAppointment(apps);
            return Ok(customer);
        }

        // DELETE: api/Customers/5

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Bookings>>> DeleteAppointmentById(int id)
        {
            try
            {
                var customer = await IBook.DeleteAppointmentById(id);
                return Ok(customer);
            }
            catch (ArithmeticException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
