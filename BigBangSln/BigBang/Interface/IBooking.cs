using BigBang.Models;

namespace BigBang.Interface
{
    public interface IBooking
    {
        Task<List<Bookings>> GetAppointments();

        Task<List<Bookings>> AddAppointment(Bookings apps);

        Task<List<Bookings>?> UpdateAppointmentById(int id, Bookings apps);
        Task<List<Bookings>?> DeleteAppointmentById(int id);
    }
}
