using System.ComponentModel.DataAnnotations;
using VehicleServiceBookingAPI_EF.Entity;

namespace VehicleServiceBookingAPI_EF.DTOs.ServiceBookings
{
    public class CreateServiceBookingDTO
    {

        [Required]
        public DateTime BookingDate { get; set; }
        // FK
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int VehicleId { get; set; }
        [Required]
        public int ServiceTypeId { get; set; }
     
    }
}
