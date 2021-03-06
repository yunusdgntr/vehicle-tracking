using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Vehicle.Tracking.DataAccess.Concrete.EntityFramework.Contexts
{
    public sealed class MsDbContext : VehicleTrackDbContext
    {
        public MsDbContext(DbContextOptions<MsDbContext> options, IConfiguration configuration)
            : base(options, configuration)
        {
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseSqlServer(Configuration.GetConnectionString("VehicleTrackMsqlContext")));
            }
        }
    }
}
