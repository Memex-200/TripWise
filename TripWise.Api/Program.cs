using Microsoft.EntityFrameworkCore;
using TripWise.Application.Interfaces.Repositories;
using TripWise.Application.Interfaces.Services;
using TripWise.Infrastructure.Repositories;
using TripWise.Infrastructure.Services;
using TripWise.Persistence;
//using TripWise.EntityFramework;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITransportCompanyRepository, TransportCompanyRepository>();
builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddScoped<IOfferRepository, OfferRepository>();


builder.Services.AddScoped<IHotelService, HotelService>();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<ITransportCompanyService, TransportCompanyService>();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

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
