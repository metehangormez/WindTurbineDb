using Microsoft.EntityFrameworkCore;
using WindTurbine.Entities; 

namespace WindTurbine.DataAccess.Context
{
    public class WindTurbineDbContext : DbContext
    {
      
        public WindTurbineDbContext(DbContextOptions<WindTurbineDbContext> options) : base(options)
        {
        }

        public DbSet<GeneratedReport> GeneratedReports { get; set; }
        public DbSet<WindFarm> WindFarms { get; set; }
        public DbSet<Turbine> Turbines { get; set; }
        public DbSet<Alert> Alerts { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
           
            base.OnModelCreating(modelBuilder);
        }
    }
}
