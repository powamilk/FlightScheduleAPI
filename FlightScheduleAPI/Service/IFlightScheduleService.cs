using FlightScheduleAPI.ViewModel;

namespace FlightScheduleAPI.Service
{
    public interface IFlightScheduleService
    {
        List<FlightScheduleVM> LayDanhSachChuyenBay(out string errorMessage);
        FlightScheduleVM LayChuyenBayTheoId(int id, out string errorMessage);
        bool TaoChuyenBay(CreateFlightScheduleVM request, out string errorMessage);
        bool CapNhatChuyenBay(int id, UpdateFlightScheduleVM request, out string errorMessage);
        bool XoaChuyenBay(int id, out string errorMessage);
        bool ThemKhachHang(int flightScheduleId, CreateKhachHangVM request, out string errorMessage);
        List<KhachHangVM> LayDanhSachKhachHang(int flightScheduleId, out string errorMessage);
        bool CapNhatKhachHang(int id, UpdateKhachHangVM request, out string errorMessage);
        bool XoaKhachHang(int id, out string errorMessage);
        List<KhachHangVM> LayDanhSachKhachHangTheoChuyenBay(int flightScheduleId, out string errorMessage);
        KhachHangVM LayDanhSachKhachHangTheoID(int id, out string errorMessage);

    }
}
