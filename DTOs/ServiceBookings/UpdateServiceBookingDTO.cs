using System.ComponentModel.DataAnnotations;

namespace VehicleServiceBookingAPI_EF.DTOs.ServiceBookings
{
    public class UpdateServiceBookingDTO
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
