using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Razor.Language.Intermediate;
using Microsoft.EntityFrameworkCore;
using VehicleServiceBookingAPI_EF.Data;
using VehicleServiceBookingAPI_EF.DTOs.Vechicles;
using VehicleServiceBookingAPI_EF.Entity;
using VehicleServiceBookingAPI_EF.Interface;

namespace VehicleServiceBookingAPI_EF.Repository
{
    public class VehicleRepository : IVechileRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public VehicleRepository(ApplicationDbContext applicationDbContext, IMapper mapper)
        {
            this._context = applicationDbContext;
            this._mapper = mapper;
        }
        async Task<string> IVechileRepository.CreateVehicleAsync(Vehicle vehicle)
        {
            bool isExists = await _context.Vehicle
        .AnyAsync(v => v.RegistrationNumber == vehicle.RegistrationNumber);
            if (isExists)
            {
                return "RegistrationNumber must be unique.";
            }
            int currentYear = DateTime.Now.Year;
            if (vehicle.Year > currentYear)
            {
                return $"Year must be {currentYear} or later.";
            }
            await _context.Vehicle.AddAsync(vehicle);
            await _context.SaveChangesAsync();
            return "Success";
        }

        async Task<bool> IVechileRepository.DeleteVehicleAsync(Vehicle vehicle)
        {
            var result = await _context.Vehicle.FindAsync(vehicle.ID);
            if (result != null)
            {
                _context.Vehicle.Remove(result);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        async Task<IEnumerable<Vehicle>> IVechileRepository.GetAllVehiclesAsync()
        {
            return await _context.Vehicle.ToListAsync();

        }

        async Task<Vehicle> IVechileRepository.GetVehicleByIdAsync(int id)
        {
            return await _context.Vehicle.FirstOrDefaultAsync(x => x.ID == id);
        }

        public async Task UpdateVehicleAsync(Vehicle vehicle)
        {
            // No need to call Update() since EF is tracking the entity
            await _context.SaveChangesAsync();
        }

        async Task<string> IVechileRepository.PatchVehicleAsync(int id, PatchVehicleDto dto)
        {
            var existingData = await _context.Vehicle.FindAsync(id);
            if (existingData == null)
            {
                return "Vehicle doesn't exist";
            }

            if (dto.Year.HasValue && dto.Year.Value > DateTime.Now.Year)
            {
                return "Year must be current year or earlier.";
            }

            if (dto.CustomerId.HasValue && !await _context.Customer.AnyAsync(c => c.Id == dto.CustomerId.Value))
            {
                return "Customer not found.";
            }

            if (!string.IsNullOrWhiteSpace(dto.Model))
            {
                existingData.Model = dto.Model;
            }

            if (dto.Year.HasValue)
            {
                existingData.Year = dto.Year.Value;
            }

            if (dto.CustomerId.HasValue)
            {
                existingData.CustomerId = dto.CustomerId.Value;
            }

            await _context.SaveChangesAsync();
            return "Updated";
        }
    }
}
