using WindTurbine.DataAccess.Context; 
using Microsoft.EntityFrameworkCore;
using WindTurbine.Business.Abstract;
using WindTurbine.Business.Concrete;
using WindTurbine.DataAccess.Abstract;
using WindTurbine.DataAccess.Repositories;
using WindTurbine.DataAccess.EntityFramework;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IAlertService, AlertManager>();
builder.Services.AddScoped<IAlertRepository, EfAlertRepository>();

builder.Services.AddScoped<IWindFarmService, WindFarmManager>();
builder.Services.AddScoped<IWindFarmRepository, EfWindFarmRepository>();

builder.Services.AddScoped<IUserRepository, EfUserRepository>();
builder.Services.AddScoped<IAuthService, AuthManager>();
builder.Services.AddScoped<ITurbineService, TurbineManager>();
builder.Services.AddScoped<ITurbineRepository, EfTurbineRepository>();
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<WindTurbineDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
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
