using System.ComponentModel.DataAnnotations;

namespace VehicleServiceBookingAPI_EF.DTOs.Vechicles
{
    public class UpdateVechicleDTOs
    {
        [Required]
        public int CustomerId { get; set; }
        public string Model { get; set; }
        [Required]
        public string RegistrationNumber { get; set; }
        [Required]
        public int Year { get; set; }

    }
}
