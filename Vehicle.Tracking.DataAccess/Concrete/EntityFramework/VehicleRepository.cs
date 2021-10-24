using Vehicle.Tracking.Core.DataAccess.EntityFramework;
using Vehicle.Tracking.DataAccess.Abstract;
using Vehicle.Tracking.DataAccess.Concrete.EntityFramework.Contexts;

namespace Vehicle.Tracking.DataAccess.Concrete.EntityFramework
{
    public class VehicleRepository : EfEntityRepositoryBase<Entities.Models.Vehicle, VehicleTrackDbContext>, IVehicleRepository
    {
        public VehicleRepository(VehicleTrackDbContext context) : base(context)
        {
        }

    }
}
