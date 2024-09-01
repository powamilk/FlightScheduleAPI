using FluentValidation;

namespace FlightScheduleAPI.ViewModel.Validation
{
    public class UpdateFlightValidation : AbstractValidator<UpdateFlightScheduleVM>
    {
        public UpdateFlightValidation() 
        {
            RuleFor(x => x.FlightNumber)
            .NotEmpty().WithMessage("Số hiệu chuyến bay là bắt buộc.")
            .Length(1, 10).WithMessage("Số hiệu chuyến bay không được vượt quá 10 ký tự.");

            RuleFor(x => x.DepartureAirport)
                .NotEmpty().WithMessage("Sân bay khởi hành là bắt buộc.")
                .Length(1, 100).WithMessage("Sân bay khởi hành không được vượt quá 100 ký tự.");

            RuleFor(x => x.ArrivalAirport)
                .NotEmpty().WithMessage("Sân bay đến là bắt buộc.")
                .Length(1, 100).WithMessage("Sân bay đến không được vượt quá 100 ký tự.");

            RuleFor(x => x.StartTime)
                .NotEmpty().WithMessage("Thời gian khởi hành là bắt buộc.");

            RuleFor(x => x.EndTime)
                .NotEmpty().WithMessage("Thời gian đến là bắt buộc.")
                .GreaterThan(x => x.StartTime).WithMessage("Thời gian đến phải lớn hơn thời gian khởi hành.");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Trạng thái là bắt buộc.")
                .Must(x => new[] { "đang bay", "đã hạ cánh", "hoãn" }.Contains(x))
                .WithMessage("Trạng thái phải là 'đang bay', 'đã hạ cánh', hoặc 'hoãn'.");
        }
    }
}
