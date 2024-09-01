using System.ComponentModel.DataAnnotations;

namespace FlightScheduleAPI.ViewModel
{
    public class UpdateFlightScheduleVM
    {
        public string FlightNumber { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Status { get; set; }

        
    }
}
