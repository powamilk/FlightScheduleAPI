using FlightScheduleAPI.ViewModel;
using FluentValidation;

public class UpdateKhachHangValidator : AbstractValidator<UpdateKhachHangVM>
{
    public UpdateKhachHangValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("ID khách hàng không hợp lệ.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Tên khách hàng là bắt buộc.")
            .Length(1, 100).WithMessage("Tên khách hàng không được vượt quá 100 ký tự.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email là bắt buộc.")
            .EmailAddress().WithMessage("Email không hợp lệ.");

        RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("Số điện thoại là bắt buộc.")
            .Matches(@"^\d{10,15}$").WithMessage("Số điện thoại phải là số và có độ dài từ 10 đến 15 ký tự.");

        RuleFor(x => x.TicketNumber)
            .NotEmpty().WithMessage("Số vé máy bay là bắt buộc.")
            .Length(1, 20).WithMessage("Số vé máy bay không được vượt quá 20 ký tự.");
    }
}
