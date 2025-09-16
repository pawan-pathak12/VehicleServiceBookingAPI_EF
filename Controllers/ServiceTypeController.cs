using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleServiceBookingAPI_EF.DTOs.ServiceTypes;
using VehicleServiceBookingAPI_EF.Entity;
using VehicleServiceBookingAPI_EF.Interface;

namespace VehicleServiceBookingAPI_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceTypeController : ControllerBase
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<ServiceTypeController> _logger;

        public ServiceTypeController(
            IServiceRepository serviceRepository,
            IMapper mapper,
            ILogger<ServiceTypeController> logger)
        {
            _serviceRepository = serviceRepository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> CreateServiceType([FromBody] CreateServiceTypeDTO createServiceType)
        {
         //   _logger.LogInformation("CreateServiceType called with data: {@CreateServiceType}", createServiceType);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for CreateServiceType");
                return BadRequest(ModelState);
            }

            try
            {
                var createService = _mapper.Map<ServiceType>(createServiceType);
                await _serviceRepository.CreateServiceTypeAsync(createService);

                _logger.LogInformation("ServiceType created successfully with Name: {Name}", createServiceType.Name);
                return Ok("Service Created");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating ServiceType");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllServiceType()
        {
            _logger.LogInformation("GetAllServiceType called");

            try
            {
                var getServices = await _serviceRepository.GetAllServiceTypesAsync();
                var result = _mapper.Map<IEnumerable<ResponseServiceTypeDTO>>(getServices);

                _logger.LogInformation("Retrieved {Count} service types", result.Count());
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving all ServiceTypes");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceByID(int id)
        {
            _logger.LogInformation("GetServiceByID called with Id: {Id}", id);

            try
            {
                var result = await _serviceRepository.GetServiceTypeByIdAsync(id);
                if (result == null)
                {
                    _logger.LogWarning("ServiceType with Id {Id} not found", id);
                    return NotFound("Data doesn't exist");
                }

                var value = _mapper.Map<ResponseServiceTypeDTO>(result);
                _logger.LogInformation("ServiceType with Id {Id} retrieved successfully", id);
                return Ok(value);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving ServiceType with Id: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateService(int id, [FromBody] UpdateServiceTypeDTO updateServiceType)
        {
            _logger.LogInformation("UpdateService called with Id: {Id}", id);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Invalid model state for UpdateService with Id: {Id}", id);
                return BadRequest(ModelState);
            }

            try
            {
                var existingData = await _serviceRepository.GetServiceTypeByIdAsync(id);
                if (existingData == null)
                {
                    _logger.LogWarning("ServiceType with Id {Id} not found for update", id);
                    return NotFound("Data doesn't exist");
                }

                _mapper.Map(updateServiceType, existingData);
                await _serviceRepository.UpdateServiceTypeAsync(existingData);

                _logger.LogInformation("ServiceType with Id {Id} updated successfully", id);
                return Ok("Service Updated");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while updating ServiceType with Id: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteService(int id)
        {
            _logger.LogInformation("DeleteService called with Id: {Id}", id);

            try
            {
                var existingData = await _serviceRepository.GetServiceTypeByIdAsync(id);
                if (existingData == null)
                {
                    _logger.LogWarning("ServiceType with Id {Id} not found for deletion", id);
                    return NotFound("Data doesn't exist");
                }

                await _serviceRepository.DeleteServiceTypeAsync(existingData);

                _logger.LogInformation("ServiceType with Id {Id} deleted successfully", id);
                return Ok("Data Deleted");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while deleting ServiceType with Id: {Id}", id);
                return StatusCode(500, "Internal server error");
            }
        }
    }
}