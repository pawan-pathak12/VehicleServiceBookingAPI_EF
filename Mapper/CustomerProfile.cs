using AutoMapper;
using VehicleServiceBookingAPI_EF.DTOs.Customers;
using VehicleServiceBookingAPI_EF.Entity;

namespace VehicleServiceBookingAPI_EF.Mapper
{
    public class CustomerProfile:Profile
    {
        public CustomerProfile()
        {
            CreateMap<Customer, ResponseCustomerDTO>();
            CreateMap<CreateCustomerDTO, Customer>();
            CreateMap<UpdateCustomerDTO, Customer>();
            CreateMap<PatchCustomerDTO, Customer>();
        }
    }
}
