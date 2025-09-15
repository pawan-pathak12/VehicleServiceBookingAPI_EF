using AutoMapper;
using VehicleServiceBookingAPI_EF.DTOs.ServiceBookings;
using VehicleServiceBookingAPI_EF.Entity;

namespace VehicleServiceBookingAPI_EF.Mapper
{
    public class ServiceBookingProfile:Profile
    {
        public ServiceBookingProfile()
        {

            CreateMap<ServiceBooking, ResponseServiceBookingDTO>();
            CreateMap<CreateServiceBookingDTO, ServiceBooking>();
            CreateMap<UpdateServiceBookingDTO, ServiceBooking>();
        }
    }
}
