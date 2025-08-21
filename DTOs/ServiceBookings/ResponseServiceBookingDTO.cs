namespace VehicleServiceBookingAPI_EF.DTOs.ServiceBookings
{
    public class ResponseServiceBookingDTO
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public int CustomerId { get; set; } 
        public int VehicleId { get; set; }   
        public int ServiceTypeId { get; set; }

    }
}
