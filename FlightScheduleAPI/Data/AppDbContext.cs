using FlightScheduleAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FlightScheduleAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<FlightSchedule> Flights { get; set; }
        public DbSet<KhachHang> HanhKhachs { get; set; }
    }
}
