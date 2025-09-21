using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VehicleServiceBookingAPI_EF.Data;
using VehicleServiceBookingAPI_EF.Entity;
using VehicleServiceBookingAPI_EF.Interface;

namespace VehicleServiceBookingAPI_EF.Repository
{
    public class ServiceBookingRepository : IServiceBookingRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;

        public ServiceBookingRepository(ApplicationDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }
        async Task<string> IServiceBookingRepository.CreateBookingAsync(ServiceBooking servicebooking)
        {
            #region  Check data exists or not 
            var isExits = await _dbContext.Customer.FirstOrDefaultAsync(x => x.Id == servicebooking.CustomerId);
            if (isExits == null)
            {
                throw new InvalidOperationException($"Customer with Id {servicebooking.CustomerId} not found");
            }
            var isVechileExits = await _dbContext.Vehicle.FirstOrDefaultAsync(x => x.ID == servicebooking.VehicleId);
            if (isVechileExits == null)
            {
                throw new InvalidOperationException($"Vechile with Id {servicebooking.VehicleId} not found");
            }
            var serviceIdExists = await _dbContext.ServiceType.FirstOrDefaultAsync(x => x.Id == servicebooking.ServiceTypeId);
            if (serviceIdExists == null)
            {
                throw new InvalidOperationException($"Service Type with Id {servicebooking.ServiceTypeId} not Found");
            }
            #endregion

            // 1. Booking at least 1 day in advance
            if (servicebooking.BookingDate.Date <= DateTime.UtcNow.Date)
            {
                return "Booking must be made at least 1 day in advance.";
            }
            // 2. Max Bookings per customer per day
            var bookingCount = await _dbContext.ServiceBooking.CountAsync(b => b.CustomerId == servicebooking.CustomerId && b.BookingDate.Date == servicebooking.BookingDate.Date);
            if (bookingCount >= 2)
            {
                return "Customer has already booked 2 services for this date.";
            }
            //3. prevent double booking of vehicle
            var isVehicleBooked = await _dbContext.ServiceBooking.AnyAsync(b => b.VehicleId == servicebooking.VehicleId && b.BookingDate.Date == servicebooking.BookingDate.Date);

            if (isVehicleBooked)
            {
                return "This vehicle is already booked on this date.";
            }

            // 4. All good - save to DB
            var booking = _mapper.Map<ServiceBooking>(servicebooking);
            await _dbContext.ServiceBooking.AddAsync(booking);
            await _dbContext.SaveChangesAsync();
            return "Success";
        }


        async Task<IEnumerable<ServiceBooking>> IServiceBookingRepository.GetAllServiceBookingAsync()
        {
            return await _dbContext.ServiceBooking.ToListAsync();
        }

        async Task<ServiceBooking> IServiceBookingRepository.GetServiceBookingByIdAsync(int id)
        {
            return await _dbContext.ServiceBooking.FirstOrDefaultAsync(x => x.Id == id);
        }

        async Task IServiceBookingRepository.UpdateServiceBookingAsync(ServiceBooking serviceBooking)
        {
            await _dbContext.SaveChangesAsync();
        }
        async Task<bool> IServiceBookingRepository.DeleteServiceBooking(ServiceBooking service)
        {
            var existingData = await _dbContext.ServiceBooking.FindAsync(service.Id);
            if (existingData != null)
            {
                _dbContext.ServiceBooking.Remove(existingData);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            return false;
        }

    }
}

