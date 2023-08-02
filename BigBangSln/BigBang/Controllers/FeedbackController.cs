using BigBang.Interface;
using BigBang.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BigBang.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedback IFeed;

        public FeedbackController(IFeedback IFeed)
        {
            this.IFeed = IFeed;
        }

        // GET: api/Customers

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Feedback>>> GetFeedbacks()
        {
            var customers = await IFeed.GetFeedbacks();
            return Ok(customers);
        }




        // PUT: api/Customers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<List<Feedback>>> UpdateFeedback(int id, Feedback apps)
        {

            try
            {
                var customer = await IFeed.UpdateFeedbackById(id, apps);
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
        public async Task<ActionResult<List<Feedback>>> AddFeedback(Feedback apps)
        {
            var customer = await IFeed.AddFeedback(apps);
            return Ok(customer);
        }

        // DELETE: api/Customers/5

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Feedback>>> DeleteFeedbackById(int id)
        {
            try
            {
                var customer = await IFeed.DeleteFeedbackById(id);
                return Ok(customer);
            }
            catch (ArithmeticException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
