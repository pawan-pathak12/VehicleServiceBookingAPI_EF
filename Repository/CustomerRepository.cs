using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VehicleServiceBookingAPI_EF.Data;
using VehicleServiceBookingAPI_EF.Entity;
using VehicleServiceBookingAPI_EF.Interface;

namespace VehicleServiceBookingAPI_EF.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public CustomerRepository(ApplicationDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }

        async Task<Customer> ICustomerRepository.CreateCustomerAsync(Customer customer)
        {

            _context.Add(customer);
            await _context.SaveChangesAsync();
            return customer;
        }

        async Task<IEnumerable<Customer>> ICustomerRepository.GetAllCustomerAsync()
        {
            return await _context.Customer.ToListAsync();
        }

        async Task<Customer> ICustomerRepository.GetFindCustomerById(int id)
        {
            return await _context.Customer.FirstOrDefaultAsync(a => a.Id == id);
        }

         async Task ICustomerRepository.UpdateCustomerAsync(Customer customer)
        {
            await _context.SaveChangesAsync();
        }
        async Task ICustomerRepository.DeleteCustomer(Customer customer)
        {
            var a = await _context.Customer.FindAsync(customer.Id);
            if (a != null)
            {
                _context.Customer.Remove(a);
                await _context.SaveChangesAsync();
            }
        }
       async Task ICustomerRepository.PatchCustomerAsync(int id, JsonPatchDocument patchDoc)
        {
            var customer = await _context.Customer.FindAsync(id);
            if(customer!=null)
            {
                patchDoc.ApplyTo(customer);
                await _context.SaveChangesAsync();
            }
            
        }
    }
}
