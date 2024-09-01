using FlightScheduleAPI.Service;
using Microsoft.AspNetCore.Identity;
using FluentValidation;
using FlightScheduleAPI.ViewModel;
using FlightScheduleAPI.ViewModel.Validation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IValidator<CreateFlightScheduleVM>, CreateFlightValidation>();
builder.Services.AddControllers();

// ??ng ký FlightScheduleService v?i DI container
builder.Services.AddScoped<IFlightScheduleService, FlightScheduleService>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
