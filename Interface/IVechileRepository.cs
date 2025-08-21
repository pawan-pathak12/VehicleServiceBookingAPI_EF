using VehicleServiceBookingAPI_EF.DTOs.Vechicles;
using VehicleServiceBookingAPI_EF.Entity;

namespace VehicleServiceBookingAPI_EF.Interface
{
    public interface IVechileRepository
    {
        Task<string> CreateVehicleAsync(Vehicle vehicle);
        Task<IEnumerable<Vehicle>> GetAllVehiclesAsync();
        Task<Vehicle> GetVehicleByIdAsync(int id);
        Task UpdateVehicleAsync(Vehicle vehicle);
        Task <bool>DeleteVehicleAsync(Vehicle vehicle);
        Task<string> PatchVehicleAsync(int id, PatchVehicleDto dto);
    }
}
