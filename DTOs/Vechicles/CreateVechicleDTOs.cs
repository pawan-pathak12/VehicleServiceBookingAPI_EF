using System.ComponentModel.DataAnnotations;
using VehicleServiceBookingAPI_EF.Entity;

namespace VehicleServiceBookingAPI_EF.DTOs.Vechicles
{
   
    public class CreateVechicleDTOs
    {
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string RegistrationNumber { get; set; }
        [Required]
        public int Year { get; set; }

    }
}
