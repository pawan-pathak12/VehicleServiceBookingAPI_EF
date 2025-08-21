using AutoMapper;
using Microsoft.AspNetCore.Http;
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

        public ServiceBookingController(IServiceBookingRepository repository, IMapper mapper)
        {
            this._repository = repository;
            this._mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateBooking([FromBody] CreateServiceBookingDTO createService)
        {

            var service = _mapper.Map<ServiceBooking>(createService);
            var result = await _repository.CreateBookingAsync(service);
            if (result != "Success")
            {
                return BadRequest(result);
            }

            return Ok("Booking created successfully.");
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBooking()
        {
            var bookings = await _repository.GetAllServiceBookingAsync();
            var result = _mapper.Map<IEnumerable<ResponseServiceBookingDTO>>(bookings);
            return Ok(result);
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetBookingById(int id)
        {
            var booking = await _repository.GetServiceBookingByIdAsync(id);
            if(booking==null)
            {
                return NotFound("data doesnt exists");
            }
            var result = _mapper.Map<ResponseServiceBookingDTO>(booking);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateBooking(int id , [FromBody] UpdateServiceBookingDTO updateServiceBooking)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingBooking = await _repository.GetServiceBookingByIdAsync(id);
            if(existingBooking==null)
            {
                return NotFound("Data doesnt exists");
            }
            _mapper.Map(updateServiceBooking, existingBooking);
            await _repository.UpdateServiceBookingAsync(existingBooking);
            return Ok("Service Booking Updated Successfully");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteServiceBoking(int id)
        {
            var result = await _repository.GetServiceBookingByIdAsync(id);
            if(result==null)
            {
                return NotFound("Service Booking doesnt exists");
            }
            await _repository.DeleteServiceBooking(result);
            return Ok("Service Booking Deleted.");
        }

}
}
