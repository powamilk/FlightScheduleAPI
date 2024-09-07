using FlightScheduleAPI.Service;
using FlightScheduleAPI.ViewModel.Validation;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using FlightScheduleAPI.ViewModel;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký các dịch vụ trong DI
builder.Services.AddControllers();

// Cấu hình AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// Đăng ký các service
builder.Services.AddScoped<IFlightScheduleService, FlightScheduleService>();

// Đăng ký các validator cho FluentValidation
builder.Services.AddScoped<IValidator<CreateFlightScheduleVM>, CreateFlightValidation>();
builder.Services.AddScoped<IValidator<UpdateFlightScheduleVM>, UpdateFlightValidation>();
builder.Services.AddScoped<IValidator<CreateKhachHangVM>, CreateKhachHangValidator>();
builder.Services.AddScoped<IValidator<UpdateKhachHangVM>, UpdateKhachHangValidator>();

// Cấu hình Swagger (tuỳ chọn, nếu bạn muốn dùng Swagger để test API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Cấu hình middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
