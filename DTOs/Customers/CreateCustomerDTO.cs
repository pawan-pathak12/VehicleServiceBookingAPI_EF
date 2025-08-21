using System.ComponentModel.DataAnnotations;

namespace VehicleServiceBookingAPI_EF.DTOs.Customers
{
    public class CreateCustomerDTO
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public int Number { get; set; }
    }
}
