using System.ComponentModel.DataAnnotations;
using VehicleServiceBookingAPI_EF.Entity;

namespace VehicleServiceBookingAPI_EF.DTOs.ServiceTypes
{
    public class UpdateServiceTypeDTO
    {
        [Required]
        public DateTime BookingDate { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public decimal Price { get; set; }
    }
}
