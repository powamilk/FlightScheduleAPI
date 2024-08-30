﻿namespace FlightScheduleAPI.ViewModel
{
    public class FlightScheduleVM
    {
        public int Id { get; set; }
        public string FlightNumber { get; set; }
        public string DepartureAirport { get; set; }
        public string ArrivalAirport { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public string Status { get; set; }
        public List<KhachHangVM> KhachHangs { get; set; } = new List<KhachHangVM>();
    }
}
