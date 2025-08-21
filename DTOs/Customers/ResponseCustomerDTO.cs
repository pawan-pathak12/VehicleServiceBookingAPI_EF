namespace VehicleServiceBookingAPI_EF.DTOs.Customers
{
    public class ResponseCustomerDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public int Number { get; set; }
    }
}
