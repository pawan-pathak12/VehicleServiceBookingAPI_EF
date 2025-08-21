namespace VehicleServiceBookingAPI_EF.DTOs.Vechicles
{
    public class PatchVehicleDto
    {

        public int? CustomerId { get; set; }
        public string? Model { get; set; }
     
        public int? Year { get; set; }
    }
}
