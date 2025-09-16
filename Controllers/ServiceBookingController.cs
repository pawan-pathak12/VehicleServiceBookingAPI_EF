using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleServiceBookingAPI_EF.DTOs.ServiceBookings;
using VehicleServiceBookingAPI_EF.Entity;
using VehicleServiceBookingAPI_EF.Interface;

namespace VehicleServiceBookingAPI_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceBookingController : ControllerBase
    {
        private readonly IServiceBookingRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceBookingController> _logger;

        public ServiceBookingController(IServiceBookingRepository repository, IMapper mapper, ILogger<ServiceBookingController> logger)
        {
            this._repository = repository;
            this._mapper = mapper;
            this._logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateServiceBookingDTO createService)
        {
            _logger.LogInformation("CreateBooking called with data: {@CreateService}", createService);

            try
            {
                var service = _mapper.Map<ServiceBooking>(createService);
                var result = await _repository.CreateBookingAsync(service);

                if (result != "Success")
                {
                    _logger.LogWarning("Booking creation failed: {Result}", result);
                    return BadRequest(result);
                }

                _logger.LogInformation("Booking created successfully for CustomerId: {CustomerId}", createService.CustomerId);
                return Ok("Booking created successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating booking for CustomerId: {CustomerId}", createService.CustomerId);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooking()
        {
            _logger.LogInformation("GetAllBooking called");

            try
            {
                var bookings = await _repository.GetAllServiceBookingAsync();
                var result = _mapper.Map<IEnumerable<ResponseServiceBookingDTO>>(bookings);

                _logger.LogInformation("Retrieved {Count} bookings", result.Count());
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all bookings");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            _logger.LogInformation("GetBookingById called with Id: {Id}", id);

            try
            {
                var booking = await _repository.GetServiceBookingByIdAsync(id);
                if (booking == null)
                {
                    _logger.LogWarning("Booking with Id {Id} not found", id);
                    return NotFound("Data doesn't exist");
                }

                var result = _mapper.Map<ResponseServiceBookingDTO>(booking);
                _logger.LogInformation("Booking with Id {Id} retrieved successfully", id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving booking with Id: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateBooking(int id, [FromBody] UpdateServiceBookingDTO updateServiceBooking)
        {
            _logger.LogInformation("UpdateBooking called with Id: {Id}", id);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for UpdateBooking with Id: {Id}", id);
                return BadRequest(ModelState);
            }

            try
            {
                var existingBooking = await _repository.GetServiceBookingByIdAsync(id);
                if (existingBooking == null)
                {
                    _logger.LogWarning("Booking with Id {Id} not found for update", id);
                    return NotFound("Data doesn't exist");
                }

                _mapper.Map(updateServiceBooking, existingBooking);
                await _repository.UpdateServiceBookingAsync(existingBooking);

                _logger.LogInformation("Booking with Id {Id} updated successfully", id);
                return Ok("Service Booking Updated Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating booking with Id: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteServiceBooking(int id)
        {
            _logger.LogInformation("DeleteServiceBooking called with Id: {Id}", id);

            try
            {
                var booking = await _repository.GetServiceBookingByIdAsync(id);
                if (booking == null)
                {
                    _logger.LogWarning("Booking with Id {Id} not found for deletion", id);
                    return NotFound("Service Booking doesn't exist");
                }

                await _repository.DeleteServiceBooking(booking);

                _logger.LogInformation("Booking with Id {Id} deleted successfully", id);
                return Ok("Service Booking Deleted.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting booking with Id: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}



