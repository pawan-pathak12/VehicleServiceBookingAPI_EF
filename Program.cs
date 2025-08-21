using Microsoft.EntityFrameworkCore;
using VehicleServiceBookingAPI_EF.Data;
using VehicleServiceBookingAPI_EF.Interface;
using VehicleServiceBookingAPI_EF.Mapper;
using VehicleServiceBookingAPI_EF.Repository;
using VehicleServiceBookingAPI_EF.Controllers;
using NuGet.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("MyCon")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));

builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IVechileRepository, VehicleRepository>();
builder.Services.AddScoped<IServiceRepository, ServiceTypeRepository>();
builder.Services.AddScoped<IServiceBookingRepository, ServiceBookingRepository>();

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
