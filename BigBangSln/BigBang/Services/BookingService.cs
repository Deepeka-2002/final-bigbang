using BigBang.Interface;
using BigBang.Models;
using Microsoft.EntityFrameworkCore;

namespace BigBang.Services
{
    public class BookingService : IBooking
    {
        

            public TravelDbContext _Context;
            public BookingService(TravelDbContext Context)
            {
                _Context = Context;
            }


            public async Task<List<Bookings>> GetAppointments()
            {
                var apps = await _Context.bookings.ToListAsync();
                return apps;
            }




            public async Task<List<Bookings>> AddAppointment(Bookings apps)
            {

                _Context.bookings.Add(apps);
                await _Context.SaveChangesAsync();
                return await _Context.bookings.ToListAsync();
            }

            public async Task<List<Bookings>?> UpdateAppointmentById(int id, Bookings apps)
            {
                var customer = await _Context.bookings.FindAsync(id);
                if (customer is null)
                {
                    throw new Exception("Invalid  id to update details");
                }
                customer.Name = apps.Name;
                
                customer.Email = apps.Email;
                customer.CheckIn = apps.CheckIn;
                customer.CheckOut = apps.CheckOut;
                customer.Adult = apps.Adult;
                customer.Child = apps.Child;
                
                await _Context.SaveChangesAsync();
                return await _Context.bookings.ToListAsync();
            }

            public async Task<List<Bookings>?> DeleteAppointmentById(int id)
            {
                var customer = await _Context.bookings.FindAsync(id);
                if (customer is null)
                {
                    throw new ArithmeticException("Invalid  id to delete");

                }
                _Context.Remove(customer);
                await _Context.SaveChangesAsync();
                return await _Context.bookings.ToListAsync();
            }
        }
    }



