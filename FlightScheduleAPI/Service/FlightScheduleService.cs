using AutoMapper;
using FlightScheduleAPI.ViewModel;
using FlightScheduleAPI.Entities;
using FluentValidation;
using FluentValidation.Results;

namespace FlightScheduleAPI.Service
{
    public class FlightScheduleService : IFlightScheduleService
    {
        private static List<FlightSchedule> _flightSchedules = new();
        private static List<KhachHang> _khachHangs = new List<KhachHang>();
        private readonly ILogger<FlightScheduleService> _logger;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateFlightScheduleVM> _createValidator;
        private readonly IValidator<UpdateFlightScheduleVM> _updateFlightValidator;
        private readonly IValidator<CreateKhachHangVM> _createKhachHangValidator;
        private readonly IValidator<UpdateKhachHangVM> _updateKhachHangValidator;

        public FlightScheduleService(
            ILogger<FlightScheduleService> logger,
            IMapper mapper,
            IValidator<CreateFlightScheduleVM> createFlightValidator,
            IValidator<UpdateFlightScheduleVM> updateFlightValidator,
            IValidator<CreateKhachHangVM> createKhachHangValidator,
            IValidator<UpdateKhachHangVM> updateKhachHangValidator)
        {
            _logger = logger;
            _mapper = mapper;
            _createValidator = createFlightValidator;
            _updateFlightValidator = updateFlightValidator;
            _createKhachHangValidator = createKhachHangValidator;
            _updateKhachHangValidator = updateKhachHangValidator;
        }

        // Phần implement các phương thức lấy danh sách chuyến bay
        public List<FlightScheduleVM> LayDanhSachChuyenBay(out string errorMessage)
        {
            if (_flightSchedules.Any())
            {
                var flightSchedulesVM = _mapper.Map<List<FlightScheduleVM>>(_flightSchedules);
                errorMessage = null;
                return flightSchedulesVM;
            }
            errorMessage = "Không có chuyến bay nào trong danh sách.";
            return null;
        }

        public FlightScheduleVM LayChuyenBayTheoId(int id, out string errorMessage)
        {
            var flightSchedule = _flightSchedules.FirstOrDefault(f => f.Id == id);
            if (flightSchedule == null)
            {
                errorMessage = "Không tìm thấy chuyến bay với ID này.";
                return null;
            }

            errorMessage = null;
            return _mapper.Map<FlightScheduleVM>(flightSchedule);
        }

        public bool TaoChuyenBay(CreateFlightScheduleVM request, out string errorMessage)
        {
            try
            {
                ValidationResult result = _createValidator.Validate(request);
                if (!result.IsValid)
                {
                    errorMessage = string.Join(",", result.Errors.Select(e => e.ErrorMessage));
                    throw new Exception(errorMessage);
                }

                var flightSchedule = _mapper.Map<FlightSchedule>(request);
                flightSchedule.Id = _flightSchedules.Any() ? _flightSchedules.Max(f => f.Id) + 1 : 1;

                _flightSchedules.Add(flightSchedule);
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Đã xảy ra lỗi khi tạo chuyến bay: {ex.Message}";
                return false;
            }
        }

        public bool CapNhatChuyenBay(int id, UpdateFlightScheduleVM request, out string errorMessage)
        {
            try
            {
                var flightSchedule = _flightSchedules.FirstOrDefault(f => f.Id == id);
                if (flightSchedule == null)
                {
                    errorMessage = "Không tìm thấy chuyến bay với ID này.";
                    return false;
                }

                ValidationResult result = _updateFlightValidator.Validate(request);
                if (!result.IsValid)
                {
                    errorMessage = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                    return false;
                }

                _mapper.Map(request, flightSchedule);

                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Đã xảy ra lỗi khi cập nhật chuyến bay: {ex.Message}";
                return false;
            }
        }

        public bool XoaChuyenBay(int id, out string errorMessage)
        {
            try
            {
                var flightSchedule = _flightSchedules.FirstOrDefault(f => f.Id == id);
                if (flightSchedule == null)
                {
                    errorMessage = "Không tìm thấy chuyến bay với ID này.";
                    return false;
                }

                _flightSchedules.Remove(flightSchedule);
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Đã xảy ra lỗi khi xóa chuyến bay: {ex.Message}";
                return false;
            }
        }

        // Phần implement các phương thức quản lý khách hàng
        public List<KhachHangVM> LayDanhSachKhachHang(int flightScheduleId, out string errorMessage)
        {
            var flightSchedule = _flightSchedules.FirstOrDefault(f => f.Id == flightScheduleId);
            if (flightSchedule != null && flightSchedule.KhachHangs.Any())
            {
                errorMessage = null;
                return _mapper.Map<List<KhachHangVM>>(flightSchedule.KhachHangs);
            }

            errorMessage = "Không tìm thấy hành khách nào trên chuyến bay này.";
            return null;
        }

        public KhachHangVM LayDanhSachKhachHangTheoID(int id, out string errorMessage)
        {
            var khachHang = _khachHangs.FirstOrDefault(k => k.Id == id);
            if (khachHang == null)
            {
                errorMessage = "Không tìm thấy hành khách với ID này.";
                return null;
            }

            errorMessage = null;
            return _mapper.Map<KhachHangVM>(khachHang);
        }

        public bool ThemKhachHang(int flightScheduleId, CreateKhachHangVM request, out string errorMessage)
        {
            try
            {
                var flightSchedule = _flightSchedules.FirstOrDefault(f => f.Id == flightScheduleId);
                if (flightSchedule == null)
                {
                    errorMessage = "Không tìm thấy chuyến bay với ID này.";
                    return false;
                }

                ValidationResult result = _createKhachHangValidator.Validate(request);
                if (!result.IsValid)
                {
                    errorMessage = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                    return false;
                }

                var khachHang = _mapper.Map<KhachHang>(request);
                khachHang.Id = _khachHangs.Any() ? _khachHangs.Max(k => k.Id) + 1 : 1;
                khachHang.FlightScheduleId = flightScheduleId;
                khachHang.FlightSchedule = flightSchedule;

                _khachHangs.Add(khachHang);
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Đã xảy ra lỗi khi thêm hành khách: {ex.Message}";
                return false;
            }
        }

        public bool CapNhatKhachHang(int id, UpdateKhachHangVM request, out string errorMessage)
        {
            try
            {
                var khachHang = _khachHangs.FirstOrDefault(k => k.Id == id);
                if (khachHang == null)
                {
                    errorMessage = "Không tìm thấy hành khách với ID này.";
                    return false;
                }

                ValidationResult result = _updateKhachHangValidator.Validate(request);
                if (!result.IsValid)
                {
                    errorMessage = string.Join(", ", result.Errors.Select(e => e.ErrorMessage));
                    return false;
                }

                _mapper.Map(request, khachHang);

                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Đã xảy ra lỗi khi cập nhật thông tin hành khách: {ex.Message}";
                return false;
            }
        }

        public bool XoaKhachHang(int id, out string errorMessage)
        {
            try
            {
                var khachHang = _khachHangs.FirstOrDefault(k => k.Id == id);
                if (khachHang == null)
                {
                    errorMessage = "Không tìm thấy hành khách với ID này.";
                    return false;
                }

                _khachHangs.Remove(khachHang);
                errorMessage = null;
                return true;
            }
            catch (Exception ex)
            {
                errorMessage = $"Đã xảy ra lỗi khi xóa hành khách: {ex.Message}";
                return false;
            }
        }

        public List<KhachHangVM> LayDanhSachKhachHangTheoChuyenBay(int flightScheduleId, out string errorMessage)
        {
            var flightSchedule = _flightSchedules.FirstOrDefault(f => f.Id == flightScheduleId);
            if (flightSchedule == null)
            {
                errorMessage = "Không tìm thấy chuyến bay với ID này.";
                return null;
            }

            if (!flightSchedule.KhachHangs.Any())
            {
                errorMessage = "Không có hành khách nào trên chuyến bay này.";
                return null;
            }

            var khachHangVMs = _mapper.Map<List<KhachHangVM>>(flightSchedule.KhachHangs);
            errorMessage = null;
            return khachHangVMs;
        }
    }
}
