using FlightScheduleAPI.Service;
using FlightScheduleAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FlightScheduleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KhachHangController : ControllerBase
    {
        private readonly IFlightScheduleService _flightScheduleService;

        public KhachHangController(IFlightScheduleService flightScheduleService)
        {
            _flightScheduleService = flightScheduleService;
        }

        [HttpPut("CapNhatKhachHang/{id}")]
        public IActionResult CapNhatKhachHang(int id, [FromBody] UpdateKhachHangVM request)
        {
            var result = _flightScheduleService.CapNhatKhachHang(id, request, out string errorMessage);
            if (result)
            {
                var updatedKhachHang = _flightScheduleService.LayDanhSachKhachHang(id, out _).FirstOrDefault(k => k.Id == id);
                return Ok(updatedKhachHang);
            }

            return BadRequest(errorMessage);
        }

        [HttpDelete("XoaKhachHang/{id}")]
        public IActionResult XoaKhachHang(int id)
        {
            var result = _flightScheduleService.XoaKhachHang(id, out string errorMessage);
            if (result)
                return NoContent();

            return BadRequest(errorMessage);
        }
    }
}
