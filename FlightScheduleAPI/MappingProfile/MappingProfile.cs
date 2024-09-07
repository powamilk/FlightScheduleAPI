using AutoMapper;
using FlightScheduleAPI.Entities;
using FlightScheduleAPI.ViewModel;

namespace FlightScheduleAPI.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<FlightSchedule, FlightScheduleVM>().ReverseMap();
            CreateMap<KhachHang, KhachHangVM>().ReverseMap();
            CreateMap<CreateFlightScheduleVM, FlightSchedule>();
            CreateMap<UpdateFlightScheduleVM, FlightSchedule>();
            CreateMap<CreateKhachHangVM, KhachHang>();
            CreateMap<UpdateKhachHangVM, KhachHang>();
        }
    }
}
