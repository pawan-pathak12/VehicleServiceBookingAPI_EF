using AutoMapper;
using System.CodeDom;
using VehicleServiceBookingAPI_EF.DTOs.ServiceTypes;
using VehicleServiceBookingAPI_EF.Entity;

namespace VehicleServiceBookingAPI_EF.Mapper
{
    public class ServiceTypeProfile:Profile
    {
        public ServiceTypeProfile()
        {

            CreateMap<ServiceType, ResponseServiceTypeDTO>();
            CreateMap<CreateServiceTypeDTO, ServiceType>();
            CreateMap<UpdateServiceTypeDTO, ServiceType>();
        }
    }
}
