using FlightScheduleAPI.ViewModel;
using FlightScheduleAPI.Entities;
using FluentValidation;
using FlightScheduleAPI.ViewModel.Validation;
using FluentValidation.Results;


namespace FlightScheduleAPI.Service
{
    public class FlightScheduleService : IFlightScheduleService
    {
        private static List<FlightSchedule> _flightSchedules = new();
        private static List<KhachHang> _khachHangs = new List<KhachHang>();
        private readonly ILogger<FlightScheduleService> _logger;
        private readonly IValidator<CreateFlightScheduleVM> _createValidator;
        private readonly IValidator<UpdateFlightScheduleVM> _updateFlightValidator;
        private readonly IValidator<CreateKhachHangVM> _createKhachHangValidator;
        private readonly IValidator<UpdateKhachHangVM> _updateKhachHangValidator;

        public FlightScheduleService(
            ILogger<FlightScheduleService> logger,
            IValidator<CreateFlightScheduleVM> createFlightValidator,
            IValidator<UpdateFlightScheduleVM> updateFlightValidator,
            IValidator<CreateKhachHangVM> createKhachHangValidator,
            IValidator<UpdateKhachHangVM> updateKhachHangValidator)
        {
            _logger = logger;
            _createValidator = createFlightValidator;
            _updateFlightValidator = updateFlightValidator;
            _createKhachHangValidator = createKhachHangValidator;
            _updateKhachHangValidator = updateKhachHangValidator;
        }

        public List<FlightScheduleVM> LayDanhSachChuyenBay(out string errorMessage)
        {
            if (_flightSchedules.Any())
            {
                var flightSchedulesVM = _flightSchedules.Select(f => new FlightScheduleVM 
                { 
                    Id = f.Id,
                    FlightNumber = f.FlightNumber,
                    DepartureAirport = f.DepartureAirport,
                    ArrivalAirport = f.ArrivalAirport,
                    DepartureTime = f.DepartureTime,
                    ArrivalTime = f.ArrivalTime,
                    Status = f.Status,
                    KhachHangs = LayDanhSachKhachHangTheoChuyenBay(f.Id, out _)
                }).ToList();
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
            return new FlightScheduleVM
            {
                Id = flightSchedule.Id,
                FlightNumber = flightSchedule.FlightNumber,
                DepartureAirport = flightSchedule.DepartureAirport,
                ArrivalAirport = flightSchedule.ArrivalAirport,
                DepartureTime = flightSchedule.DepartureTime,
                ArrivalTime = flightSchedule.ArrivalTime,
                Status = flightSchedule.Status,
                KhachHangs = LayDanhSachKhachHangTheoChuyenBay(flightSchedule.Id, out _)
            };

        }

        public bool TaoChuyenBay(CreateFlightScheduleVM request, out string errorMessage)
        {
            try
            {
                ValidationResult result = _createValidator.Validate(request);
                if(!result.IsValid)
                {
                    errorMessage = string.Join("," , result.Errors.Select(e => e.ErrorMessage));
                    throw new Exception(errorMessage);
                }    

                var flightSchedule = new FlightSchedule
                {
                    Id = _flightSchedules.Any() ? _flightSchedules.Max(f => f.Id) + 1 : 1,
                    FlightNumber = request.FlightNumber,
                    DepartureAirport = request.DepartureAirport,
                    ArrivalAirport = request.ArrivalAirport,
                    DepartureTime = request.DepartureTime,
                    ArrivalTime = request.ArrivalTime,
                    Status = request.Status
                };

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

                flightSchedule.FlightNumber = request.FlightNumber;
                flightSchedule.DepartureAirport = request.DepartureAirport;
                flightSchedule.ArrivalAirport = request.ArrivalAirport;
                flightSchedule.DepartureTime = request.StartTime;
                flightSchedule.ArrivalTime = request.EndTime;
                flightSchedule.Status = request.Status;

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

                var khachHang = new KhachHang
                {
                    Id = _khachHangs.Any() ? _khachHangs.Max(k => k.Id) + 1 : 1,
                    Name = request.Name,
                    Email = request.Email,
                    Phone = request.Phone,
                    TicketNumber = request.TicketNumber,
                    FlightScheduleId = flightScheduleId,
                    FlightSchedule = flightSchedule
                };

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

        public List<KhachHangVM> LayDanhSachKhachHang(int flightScheduleId, out string errorMessage)
        {
            var flightSchedule = _flightSchedules.FirstOrDefault(f => f.Id == flightScheduleId);
            if (flightSchedule != null && flightSchedule.KhachHangs.Any())
            {
                errorMessage = null;
                return flightSchedule.KhachHangs.Select(k => new KhachHangVM
                {
                    Id = k.Id,
                    Name = k.Name,
                    Email = k.Email,
                    Phone = k.Phone,
                    TicketNumber = k.TicketNumber
                }).ToList();
            }

            errorMessage = "Không tìm thấy hành khách nào trên chuyến bay này.";
            return null;
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

                khachHang.Name = request.Name;
                khachHang.Email = request.Email;
                khachHang.Phone = request.Phone;
                khachHang.TicketNumber = request.TicketNumber;

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

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public List<KhachHangVM> LayDanhSachKhachHangTheoChuyenBay(int flightScheduleId, out string errorMessage)
        {
            var flightSchedule = _flightSchedules.FirstOrDefault(f => f.Id == flightScheduleId);
            if(flightSchedule == null)
            {
                errorMessage = "Không tìm thấy chuyến bay với ID này";
                return null;
            }    
            var khachHangs = _khachHangs.Where(k => k.FlightScheduleId == flightScheduleId).ToList();
            if(!khachHangs.Any())
            {
                errorMessage = "Không có khách hàng nào trên chuyến bay này.";
                return null;
            }
            var khachHangVMs = khachHangs.Select(k => new KhachHangVM
            {
                Id = k.Id,
                Name = k.Name,
                Email = k.Email,
                Phone = k.Phone,
                TicketNumber = k.TicketNumber,
            }).ToList();
            errorMessage = null;
            return khachHangVMs;
        }

        public KhachHangVM LayDanhSachKhachHangTheoID(int id, out string errorMessage)
        {
            var khachHang =_khachHangs.FirstOrDefault(k => k.Id == id);
            if (khachHang == null)
            {
                errorMessage = "Không tìm thấy hành khách với ID này.";
                return null;
            }
            var khachHangVM = new KhachHangVM
            {
                Id = khachHang.Id,
                Name = khachHang.Name,
                Email = khachHang.Email,
                Phone = khachHang.Phone,
                TicketNumber = khachHang.TicketNumber,
            };
            errorMessage = null;
            return khachHangVM;
        }
    }
}
