using VehicleServiceBookingAPI_EF.Entity;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.EntityFrameworkCore;
using VehicleServiceBookingAPI_EF.Data;

namespace VehicleServiceBookingAPI_EF.Interface
{
    public interface IServiceRepository
    {
        Task<ServiceType> CreateServiceTypeAsync(ServiceType serviceType);
        Task<IEnumerable<ServiceType>> GetAllServiceTypesAsync();
        Task<ServiceType> GetServiceTypeByIdAsync(int id);
        Task UpdateServiceTypeAsync( ServiceType serviceType);
        Task DeleteServiceTypeAsync(ServiceType service);
    }
}


