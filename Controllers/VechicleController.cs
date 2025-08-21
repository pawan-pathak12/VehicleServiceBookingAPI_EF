using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using VehicleServiceBookingAPI_EF.DTOs.Vechicles;
using VehicleServiceBookingAPI_EF.Entity;
using VehicleServiceBookingAPI_EF.Interface;
using VehicleServiceBookingAPI_EF.Repository;

namespace VehicleServiceBookingAPI_EF.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VechicleController : ControllerBase
    {
        private readonly IVechileRepository _vechileRepository;
        private readonly IMapper _mapper;

        public VechicleController(IVechileRepository vechileRepository, IMapper mapper)
        {
            this._vechileRepository = vechileRepository;
            this._mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] CreateVechicleDTOs vechicleDTOs)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var vechileEntity = _mapper.Map<Vehicle>(vechicleDTOs);
            var result = await _vechileRepository.CreateVehicleAsync(vechileEntity);
            if (result != "Success")
            {
                return BadRequest(result);
            }
            return Ok("Vechile data Created .");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllVehicle()
        {
            var result = await _vechileRepository.GetAllVehiclesAsync();
            var dtos = _mapper.Map<IEnumerable<ResponseVechicleDTOs>>(result);
            return Ok(dtos);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetVechileById(int id)
        {
            var result = await _vechileRepository.GetVehicleByIdAsync(id);
            var dtos = _mapper.Map<ResponseVechicleDTOs>(result);
            return Ok(dtos);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateVechile(int id, [FromBody] UpdateVechicleDTOs update)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingData = await _vechileRepository.GetVehicleByIdAsync(id);
            if (existingData == null)
            {
                return NotFound("Vehicle not found");
            }
            _mapper.Map(update, existingData);
            await _vechileRepository.UpdateVehicleAsync(existingData);
            return Ok("Vechile Updated Successfully");
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
            var existingData = await _vechileRepository.GetVehicleByIdAsync(id);
            if (existingData == null)
            {
                return NotFound("Vehicle Not Found");
            }
            await _vechileRepository.DeleteVehicleAsync(existingData);
            return Ok("Vehicle deleted Successfully ");
        }

    }
}
