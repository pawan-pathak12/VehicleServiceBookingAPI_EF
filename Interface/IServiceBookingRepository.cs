using VehicleServiceBookingAPI_EF.DTOs.ServiceBookings;
using VehicleServiceBookingAPI_EF.Entity;

namespace VehicleServiceBookingAPI_EF.Interface
{
    public interface IServiceBookingRepository
    {
        Task<string> CreateBookingAsync(ServiceBooking service);
        Task<IEnumerable<ServiceBooking>> GetAllServiceBookingAsync();
        Task<ServiceBooking> GetServiceBookingByIdAsync(int id);
        Task UpdateServiceBookingAsync(ServiceBooking serviceBooking);
        Task<bool> DeleteServiceBooking(ServiceBooking service);



    }
}
