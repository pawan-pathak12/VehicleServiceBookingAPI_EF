using AutoMapper;
using VehicleServiceBookingAPI_EF.DTOs.Vechicles;
using VehicleServiceBookingAPI_EF.Entity;

namespace VehicleServiceBookingAPI_EF.Mapper
{
    public class VechileProfile:Profile
    {
        public VechileProfile()
        {
            CreateMap<Vehicle, ResponseVechicleDTOs>();
            CreateMap<CreateVechicleDTOs, Vehicle>();
            CreateMap<UpdateVechicleDTOs, Vehicle>();
            CreateMap<PatchVehicleDto, Vehicle>();
        }
    }
}
