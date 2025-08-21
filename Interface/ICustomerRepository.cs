using Microsoft.AspNetCore.JsonPatch;
using VehicleServiceBookingAPI_EF.Entity;

namespace VehicleServiceBookingAPI_EF.Interface
{
    public interface ICustomerRepository
    {
        Task<Customer> CreateCustomerAsync(Customer customer);
        Task<IEnumerable<Customer>> GetAllCustomerAsync();
        Task<Customer> GetFindCustomerById(int id);
         Task UpdateCustomerAsync(Customer customer);
        Task DeleteCustomer(Customer customer);
        Task PatchCustomerAsync(int id, JsonPatchDocument patchDoc);

    }
}
