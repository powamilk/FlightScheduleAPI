using System.ComponentModel.DataAnnotations;

namespace FlightScheduleAPI.ViewModel
{
    public class ValidationThoiGian
    {
        public static ValidationResult ValidateArrivalTime(DateTime arrivalTime, ValidationContext context)
        {
            var lichTrinh = context.ObjectInstance as dynamic;

            if (lichTrinh != null)
            {
                if (HasProperty(lichTrinh, "DepartureTime") && arrivalTime <= lichTrinh.DepartureTime)
                {
                    return new ValidationResult("Thời gian đến phải lớn hơn thời gian đi");
                }
                else if (HasProperty(lichTrinh, "StartTime") && arrivalTime <= lichTrinh.StartTime)
                {
                    return new ValidationResult("Thời gian đến phải lớn hơn thời gian bắt đầu");
                }
            }

            return ValidationResult.Success;
        }

        private static bool HasProperty(dynamic obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName) != null;
        }

    }
}
