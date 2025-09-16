using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VehicleServiceBookingAPI_EF.DTOs.Vechicles;
using VehicleServiceBookingAPI_EF.Entity;
using VehicleServiceBookingAPI_EF.Interface;

namespace VehicleServiceBookingAPI_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VechicleController : ControllerBase
    {
        private readonly IVechileRepository _vechileRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<VechicleController> _logger;

        public VechicleController(IVechileRepository vechileRepository, IMapper mapper, ILogger<VechicleController> logger)
        {
            this._vechileRepository = vechileRepository;
            this._mapper = mapper;
            this._logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVechicleDTOs vechicleDTOs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var vechileEntity = _mapper.Map<Vehicle>(vechicleDTOs);
                var result = await _vechileRepository.CreateVehicleAsync(vechileEntity);
                if (result != "Success")
                {
                    return BadRequest(result);
                }
                _logger.LogInformation("Vechile Data Created from VechileController");
                return Ok("Vechile data Created.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error while Creating Vechile Data in Controller");
                throw;
            }

        }

        [HttpGet]
        public async Task<IActionResult> GetAllVehicle()
        {
            try
            {
                var result = await _vechileRepository.GetAllVehiclesAsync();
                var dtos = _mapper.Map<IEnumerable<ResponseVechicleDTOs>>(result);
                _logger.LogInformation("Fetching All Data of Vehicle from data through Controller");
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error While Fetching All Record of Vehicle");
                throw;
            }
            
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVechileById(int id)
        {
            try
            {
                var result = await _vechileRepository.GetVehicleByIdAsync(id);
                var dtos = _mapper.Map<ResponseVechicleDTOs>(result);
                _logger.LogInformation("Fetching Record of Vechile Based on Id from Controller");
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error While Fetching Record of Vehicle by Id");
                throw;
            }
           
        }
        [HttpPut]
        public async Task<IActionResult> UpdateVechile(int id, [FromBody] UpdateVechicleDTOs update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var existingData = await _vechileRepository.GetVehicleByIdAsync(id);
                if (existingData == null)
                {
                    return NotFound("Vehicle not found");
                }
                _mapper.Map(update, existingData);
                await _vechileRepository.UpdateVehicleAsync(existingData);
                _logger.LogInformation($"Updating Vechile data of Id {id}");
                return Ok("Vechile Updated Successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while updating Data of Vechile of Id {id}");
                throw;
            }
          
        }
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchVechile(int id, [FromBody] PatchVehicleDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var patch = await _vechileRepository.PatchVehicleAsync(id, dto);

            if (patch == "Vehicle doesn't exist")
                return NotFound(patch);

            if (patch != "Updated")
                return BadRequest(patch);

            return Ok("Vehicle Updated Successfully");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            try
            {
                var existingData = await _vechileRepository.GetVehicleByIdAsync(id);
                if (existingData == null)
                {
                    return NotFound("Vehicle Not Found");
                }
                await _vechileRepository.DeleteVehicleAsync(existingData);
                _logger.LogInformation($"Deleting Record of Vehicle id {id}");
                return Ok("Vehicle deleted Successfully ");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error while deleting record of vechile of id{id}");
                throw;
            }
          
        }

    }
}
