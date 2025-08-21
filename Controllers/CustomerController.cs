using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using VehicleServiceBookingAPI_EF.DTOs.Customers;
using VehicleServiceBookingAPI_EF.Entity;
using VehicleServiceBookingAPI_EF.Interface;
using VehicleServiceBookingAPI_EF.Repository;

namespace VehicleServiceBookingAPI_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;

        public CustomerController(ICustomerRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDTO createCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var customer = _mapper.Map<Customer>(createCustomer);
            var result = await _repository.CreateCustomerAsync(customer);
            return Ok("Customer Created");
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCustomer()
        {
            var result = await _repository.GetAllCustomerAsync();
            var dtos = _mapper.Map<IEnumerable<ResponseCustomerDTO>>(result);
            return Ok(dtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllCustomerById([FromRoute] int id)
        {
            var result = await _repository.GetFindCustomerById(id);

            if (result == null)
            {
                return NotFound("Customer data doesn't exist.");
            }

            var dtos = _mapper.Map<ResponseCustomerDTO>(result);
            return Ok(dtos);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerDTO updateCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingData = await _repository.GetFindCustomerById(id);
            if (existingData == null)
            {
                return NotFound($"Customer with id {id} not found.");
            }
            _mapper.Map(updateCustomer, existingData);
            await _repository.UpdateCustomerAsync(existingData);

            return Ok("Customer Updated Successfully");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await _repository.GetFindCustomerById(id);
            if (result == null)
            {
                return NotFound("data  dont exists");
            }
            await _repository.DeleteCustomer(result);
            return Ok("Customer deleted Successfully");
        }

        [HttpPatch("{id:int}")]

        public async Task<IActionResult> PatchCustomer(int id, [FromBody] PatchCustomerDTO patchDocument)
        {
            var existingCustomer = await _repository.GetFindCustomerById(id);

            if (existingCustomer == null)
            {
                return NotFound("Customer not found");
            }
            _mapper.Map(patchDocument, existingCustomer); // only non-null values will be mapped

            await _repository.UpdateCustomerAsync(existingCustomer);

            return Ok("Customer updated successfully");
        }

    }
}
