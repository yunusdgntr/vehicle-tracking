using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Vehicle.Tracking.DataAccess.Concrete.EntityFramework.Contexts
{
    public sealed class OracleDbContext : VehicleTrackDbContext
    {
        public OracleDbContext(DbContextOptions<OracleDbContext> options, IConfiguration configuration)
            : base(options, configuration)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseOracle(Configuration.GetConnectionString("ConnectionStrings:VehicleTrackOracleContext")));
            }
        }
    }
}
