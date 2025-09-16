using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleServiceBookingAPI_EF.DTOs.Customers;
using VehicleServiceBookingAPI_EF.Entity;
using VehicleServiceBookingAPI_EF.Interface;

namespace VehicleServiceBookingAPI_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ICustomerRepository repository, IMapper mapper, ILogger<CustomerController> logger)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> CreateCustomer([FromBody] CreateCustomerDTO createCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var customer = _mapper.Map<Customer>(createCustomer);
                var result = await _repository.CreateCustomerAsync(customer);
                _logger.LogInformation("Customer Data Created from CustomerController");
                return Ok("Customer Created");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while creating Customer Data in Customer Controller");
                throw;
            }

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
            try
            {
                var result = await _repository.GetFindCustomerById(id);
                if (result == null)
                {
                    return NotFound("Customer data doesn't exist.");
                }

                var dtos = _mapper.Map<ResponseCustomerDTO>(result);
                _logger.LogInformation("Fetching all Customer Data from Database from CustomerContoroller");
                return Ok(dtos);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error While fetching all customer Data");
                throw;
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCustomer(int id, [FromBody] UpdateCustomerDTO updateCustomer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var existingData = await _repository.GetFindCustomerById(id);
                if (existingData == null)
                {
                    return NotFound($"Customer with id {id} not found.");
                }
                _mapper.Map(updateCustomer, existingData);
                await _repository.UpdateCustomerAsync(existingData);
                _logger.LogInformation($"Updating Customer record of Id {id}");
                return Ok("Customer Updated Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while Updating record of Customer of id {id}");
                throw;
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            try
            {
                var result = await _repository.GetFindCustomerById(id);
                if (result == null)
                {
                    return NotFound("data  dont exists");
                }
                await _repository.DeleteCustomer(result);
                _logger.LogInformation($"Deleting Customer record of Id {id}");
                return Ok("Customer deleted Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting record of Customer with id {id}");
                throw;
            }
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
