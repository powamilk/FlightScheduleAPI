using System.ComponentModel.DataAnnotations;

namespace FlightScheduleAPI.ViewModel
{
    public class CreateFlightScheduleVM
    {
        public string FlightNumber { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Status { get; set; }
    }
}
