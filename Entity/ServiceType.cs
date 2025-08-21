namespace VehicleServiceBookingAPI_EF.Entity
{
    public class ServiceType
    {
        public int Id { get; set; }
        public DateTime BookingDate { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
