using Microsoft.AspNetCore.Identity;

namespace VehicleServiceBookingAPI_EF.Entity
{
    public class Vehicle
    {
        public int ID { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public string Model { get; set; }
       
        public string RegistrationNumber { get; set; }
        public int Year { get; set; }

    }
}
