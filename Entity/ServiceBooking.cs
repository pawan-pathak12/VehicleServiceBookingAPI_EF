namespace VehicleServiceBookingAPI_EF.Entity
{
    public class ServiceBooking
    {
        public int Id { get; set; }

        public DateTime BookingDate { get; set; }

        // FK
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        public int VehicleId { get; set; }
        public Vehicle Vehicle { get; set; }

        public int ServiceTypeId { get; set; }
        public ServiceType ServiceType { get; set; }
    }
}
