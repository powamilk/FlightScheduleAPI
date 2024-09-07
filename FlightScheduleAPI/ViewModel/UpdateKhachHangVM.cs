using System.ComponentModel.DataAnnotations;

namespace FlightScheduleAPI.ViewModel
{
    public class UpdateKhachHangVM
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Tên khách hàng không được bỏ trống")]
        [StringLength(100, ErrorMessage = "Tên khách hàng không được quá 100 ký tự")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email không được bỏ trống")]
        [EmailAddress(ErrorMessage = "Địa chỉ Email không đúng định dạng")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Số Điện Thoại không được bỏ trống")]
        [RegularExpression("^0[0-9]{8}$", ErrorMessage = "Số điện thoại không đúng định dạng")]
        [StringLength(15, ErrorMessage = "Số điện thoại không được quá 15 ký tự")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "Số vé không được bỏ trống")]
        [StringLength(20, ErrorMessage = "Số Vé không được quá 15 ký tự")]
        public string TicketNumber { get; set; }
    }
}
