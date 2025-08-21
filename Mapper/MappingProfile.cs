using AutoMapper;
using VehicleServiceBookingAPI_EF.DTOs.Customers;
using VehicleServiceBookingAPI_EF.DTOs.ServiceBookings;
using VehicleServiceBookingAPI_EF.DTOs.ServiceTypes;
using VehicleServiceBookingAPI_EF.DTOs.Vechicles;
using VehicleServiceBookingAPI_EF.Entity;

namespace VehicleServiceBookingAPI_EF.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {

            CreateMap<Vehicle, ResponseVechicleDTOs>();
            CreateMap<CreateVechicleDTOs, Vehicle>();
            CreateMap<UpdateVechicleDTOs, Vehicle>();
            CreateMap<PatchVehicleDto, Vehicle>();

            CreateMap<Customer, ResponseCustomerDTO>();
            CreateMap<CreateCustomerDTO, Customer>();
            CreateMap<UpdateCustomerDTO, Customer>();
            CreateMap<PatchCustomerDTO, Customer>();

            CreateMap<ServiceType, ResponseServiceTypeDTO>();
            CreateMap<CreateServiceTypeDTO, ServiceType>();
            CreateMap<UpdateServiceTypeDTO, ServiceType>();

            CreateMap<ServiceBooking, ResponseServiceBookingDTO>();
            CreateMap<CreateServiceBookingDTO, ServiceBooking>();
            CreateMap<UpdateServiceBookingDTO, ServiceBooking>();

        }
    }
}
