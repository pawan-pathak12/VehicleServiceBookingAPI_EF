using VehicleServiceBookingAPI_EF.Entity;

namespace VehicleServiceBookingAPI_EF.DTOs.ServiceTypes
{
    public class ResponseServiceTypeDTO
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
