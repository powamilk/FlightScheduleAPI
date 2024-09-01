using FlightScheduleAPI.Entities;
using FlightScheduleAPI.Service;
using FlightScheduleAPI.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FlightScheduleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightScheduleController : ControllerBase
    {
        private readonly IFlightScheduleService _flightScheduleService;
        private static List<KhachHang> _khachHangs = new List<KhachHang>();

        public FlightScheduleController(IFlightScheduleService flightScheduleService)
        {
            _flightScheduleService = flightScheduleService;
        }

        [HttpGet("LayDanhSachChuyenBay")]
        public IActionResult LayDanhSachChuyenBay()
        {
            var result = _flightScheduleService.LayDanhSachChuyenBay(out string errorMessage);
            if (result != null)
                return Ok(result);

            return NotFound(errorMessage);
        }

        [HttpGet("LayChuyenBayTheoId/{id}")]
        public IActionResult LayChuyenBayTheoId(int id)
        {
            var result = _flightScheduleService.LayChuyenBayTheoId(id, out string errorMessage);
            if (result != null)
                return Ok(result);

            return NotFound(errorMessage);
        }

        [HttpPost("TaoChuyenBay")]
        public IActionResult TaoChuyenBay([FromBody] CreateFlightScheduleVM request)
        {
            var result = _flightScheduleService.TaoChuyenBay(request, out string errorMessage);
            if (!result)
            {
                return BadRequest(errorMessage);
            }
            var createFlight = _flightScheduleService.LayChuyenBayTheoId(_flightScheduleService.LayDanhSachChuyenBay(out _).Max(f => f.Id), out _);
            return CreatedAtAction(nameof(LayChuyenBayTheoId), new { id = createFlight.Id }, createFlight);
        }

        [HttpPut("CapNhatChuyenBay/{id}")]
        public IActionResult CapNhatChuyenBay(int id, [FromBody] UpdateFlightScheduleVM request)
        {
            var result = _flightScheduleService.CapNhatChuyenBay(id, request, out string errorMessage);
            if (result)
            {
                var updatedFlight = _flightScheduleService.LayChuyenBayTheoId(id, out _);
                return Ok(updatedFlight);
            }

            return BadRequest(errorMessage);
        }

        [HttpDelete("XoaChuyenBay/{id}")]
        public IActionResult XoaChuyenBay(int id)
        {
            var result = _flightScheduleService.XoaChuyenBay(id, out string errorMessage);
            if (result)
                return NoContent();

            return BadRequest(errorMessage);
        }

        [HttpPost("ThemKhachHangVaoChuyenBay/{flightScheduleId}")]
        public IActionResult ThemKhachHangVaoChuyenBay(int flightScheduleId, [FromBody] CreateKhachHangVM request)
        {
            var result = _flightScheduleService.ThemKhachHang(flightScheduleId, request, out string errorMessage);
            if (result)
            {
                var khachHangs = _flightScheduleService.LayDanhSachKhachHang(flightScheduleId, out _);
                return Ok(khachHangs);
            }

            return BadRequest(errorMessage);
        }

        [HttpGet("LayDanhSachKhachHangTheoChuyenBayId/{flightScheduleId}")]
        public IActionResult LayDanhSachKhachHangTheoChuyenBayId(int flightScheduleId)
        {
           
            var result = _flightScheduleService.LayDanhSachKhachHangTheoChuyenBay(flightScheduleId, out string errorMessage);
            if (result == null)
            {
                return NotFound(errorMessage);
            }    
            return Ok(result);
        }
    }
}
