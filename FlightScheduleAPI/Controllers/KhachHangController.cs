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
            if(!result)
            {
                if(errorMessage == "Không tìm thấy hành khách với ID này.")
                {
                    return NotFound();
                }
                return BadRequest(errorMessage);
            }
            var updateKhachHang = _flightScheduleService.LayDanhSachKhachHangTheoID(id, out _);
            return Ok(updateKhachHang);
        }

        [HttpDelete("XoaKhachHang/{id}")]
        public IActionResult XoaKhachHang(int id)
        {
            var result = _flightScheduleService.XoaKhachHang(id, out string errorMessage);
            if (result)
                return NoContent();

            return BadRequest(errorMessage);
        }

        [HttpGet("LayDanhSachKhahHangTHeoChuyenBay")]
        public IActionResult LayKhachHangTheoChuyenBay(int flightScheduleId)
        {
            var resule = _flightScheduleService.LayDanhSachKhachHangTheoChuyenBay(flightScheduleId, out string errorMessage);
            if(resule == null)
            {
                return NotFound(errorMessage);
            }
            return Ok(resule);
        }

    }
}
