using Microsoft.EntityFrameworkCore;
using MotorcycleRentalChallenge.Core.Interfaces.Repositories;
using MotorcycleRentalChallenge.Infrastructure;
using MotorcycleRentalChallenge.Infrastructure.Data;
using MotorcycleRentalChallenge.Infrastructure.Data.Repositories;
using MotorcycleRentalChallenge.Worker;

var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(
    options =>
    options.UseNpgsql(connectionString));

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
