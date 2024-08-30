namespace FlightScheduleAPI.Entities
{
    public class KhachHang
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string TicketNumber { get; set; }
        public int FlightId { get; set; }
        public FlightSchedule Flight { get; set; }
        public int FlightScheduleId { get; set; }
        public FlightSchedule FlightSchedule { get; set; }
    }
}
