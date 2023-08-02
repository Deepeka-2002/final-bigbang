using BigBang.Interface;
using BigBang.Models;
using Microsoft.EntityFrameworkCore;

namespace BigBang.Services
{
    public class FeedbackService : IFeedback
    {
        public TravelDbContext _Context;
        public FeedbackService(TravelDbContext Context)
        {
            _Context = Context;
        }


        public async Task<List<Feedback>> GetFeedbacks()
        {
            var apps = await _Context.feedback.ToListAsync();
            return apps;
        }




        public async Task<List<Feedback>> AddFeedback(Feedback apps)
        {

            _Context.feedback.Add(apps);
            await _Context.SaveChangesAsync();
            return await _Context.feedback.ToListAsync();
        }

        public async Task<List<Feedback>?> UpdateFeedbackById(int id, Feedback apps)
        {
            var customer = await _Context.feedback.FindAsync(id);
            if (customer is null)
            {
                throw new ArithmeticException("Invalid  id to update details");
            }
            customer.Name = apps.Name;

            customer.Email = apps.Email;
            customer.Rating = apps.Rating;
            customer.Description = apps.Description;
            

            await _Context.SaveChangesAsync();
            return await _Context.feedback.ToListAsync();
        }

        public async Task<List<Feedback>?> DeleteFeedbackById(int id)
        {
            var customer = await _Context.feedback.FindAsync(id);
            if (customer is null)
            {
                throw new ArithmeticException("Invalid  id to delete");

            }
            _Context.Remove(customer);
            await _Context.SaveChangesAsync();
            return await _Context.feedback.ToListAsync();
        }
    }
}
