using FluentValidation;
namespace FlightScheduleAPI.ViewModel.Validation
{
    public partial class CreateFlightValidation : AbstractValidator<CreateFlightScheduleVM>
    {
        public CreateFlightValidation() 
        {
            RuleFor(x => x.FlightNumber)
                .NotEmpty().WithMessage("Số Máy bay không được để trống")
                .Length(1, 10).WithMessage("Số máy bay không được vượt quá 10 kí tự");

            RuleFor(x => x.DepartureAirport)
                .NotEmpty().WithMessage("Điểm bắt đầu khống được để trống")
                .Length(1, 100).WithMessage("Điểm bắt đầu không được quá 100 kí tự");

            RuleFor(x => x.ArrivalAirport)
                .NotEmpty().WithMessage("Điểm Đến không được để trống")
                .Length(1, 100).WithMessage("Điểm đến không được quá 100 kí tự");

            RuleFor(x => x.DepartureTime)
                .NotEmpty().WithMessage("THời gian bắt đầu không được để trống");

            RuleFor(x => x.ArrivalTime)
                .NotEmpty().WithMessage("Thời gian đến không được để trống")
                .GreaterThan(x => x.DepartureTime).WithMessage("Thời gian đến không được bé hơn thời gian đi");

            RuleFor(x => x.Status)
                .NotEmpty().WithMessage("Trạng thái không được để trống")
                .Must(x => new[] { "đang bay", "đã hạ cánh", "hoãn" }.Contains(x))
                .WithMessage(" trạng thái phải là 'đang bay' , 'đã hạ cánh' , 'hoãn'");
                
        }
    }
}
