using Microsoft.EntityFrameworkCore;
using WindTurbine.Business.Abstract;
using WindTurbine.Business.Concrete;
using WindTurbine.DataAccess.Abstract;
using WindTurbine.DataAccess.Context;
using WindTurbine.DataAccess.EntityFramework; 
using WindTurbine.DataAccess.Repositories;
using WindTurbine.ScadaCollector;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
      
        var connectionString = hostContext.Configuration.GetConnectionString("DefaultConnection");

        services.AddDbContext<WindTurbineDbContext>(options =>
            options.UseSqlServer(connectionString));

      
        services.AddScoped<IAlertService, AlertManager>();
        services.AddScoped<IAlertRepository, EfAlertRepository>();

       
        services.AddHostedService<Worker>();
    })
    .Build();

host.Run();