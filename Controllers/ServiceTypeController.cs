using AutoMapper;
using AutoMapper.Configuration.Annotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
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

        public ServiceTypeController(IServiceRepository serviceRepository, IMapper mapper)
        {
            this._serviceRepository = serviceRepository;
            this._mapper = mapper;
        }
        [HttpPost]
        public async Task<IActionResult> CreateServiceType([FromBody] CreateServiceTypeDTO createServiceType)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);

            }
            var createService = _mapper.Map<ServiceType>(createServiceType);
            var createdService = await _serviceRepository.CreateServiceTypeAsync(createService);
            return Ok("Service Created");

        }
        [HttpGet]
        public async Task<IActionResult> GetAllServiceType()
        {
            var getServices = await _serviceRepository.GetAllServiceTypesAsync();
            var a = _mapper.Map<IEnumerable<ResponseServiceTypeDTO>>(getServices);
            return Ok(a);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetServiceByID(int id)
        {
            var result = await _serviceRepository.GetServiceTypeByIdAsync(id);
            if (result == null)
            {
                return NotFound("Data Doesnt Exists");
            }
            var value = _mapper.Map<ResponseServiceTypeDTO>(result);
            return Ok(value);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateService(int id, [FromBody] UpdateServiceTypeDTO updateServiceType)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingData = await _serviceRepository.GetServiceTypeByIdAsync(id);
            if (existingData == null)
            {
                return NotFound("Data doesnt Exists");
            }
            _mapper.Map(updateServiceType, existingData);
            await _serviceRepository.UpdateServiceTypeAsync(existingData);
            return Ok("Service Updated");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteService(int id)
        {
            var exsitingData = await _serviceRepository.GetServiceTypeByIdAsync(id);
            if(exsitingData==null)
            {
                return NotFound("Data doesnt exists");
            }
            await _serviceRepository.DeleteServiceTypeAsync(exsitingData);
            return Ok("Data Deleted");
        }
    }
}
