using Microsoft.EntityFrameworkCore;
using VehicleServiceBookingAPI_EF.Data;
using VehicleServiceBookingAPI_EF.Entity;
using VehicleServiceBookingAPI_EF.Interface;

namespace VehicleServiceBookingAPI_EF.Repository
{
    public class ServiceTypeRepository : IServiceRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ServiceTypeRepository(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        async Task<ServiceType> IServiceRepository.CreateServiceTypeAsync(ServiceType serviceType)
        {
           _dbContext.Add(serviceType);
            await _dbContext.SaveChangesAsync();
            return serviceType;
        }

      
        async Task<IEnumerable<ServiceType>> IServiceRepository.GetAllServiceTypesAsync()
        {
           return await _dbContext.ServiceType.ToListAsync();
        }

        async Task<ServiceType> IServiceRepository.GetServiceTypeByIdAsync(int id)
        {
            return await _dbContext.ServiceType.FirstOrDefaultAsync(a => a.Id == id);
        }

        async Task IServiceRepository.UpdateServiceTypeAsync(ServiceType serviceType)
        {
           
            await _dbContext.SaveChangesAsync();
        
            
        }
        async Task IServiceRepository.DeleteServiceTypeAsync(ServiceType service )
        {
            var data = _dbContext.ServiceType.FindAsync(service.Id);
            if(data!=null)
            {
                _dbContext.ServiceType.Remove(service);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
