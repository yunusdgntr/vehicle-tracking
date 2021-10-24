using Vehicle.Tracking.Core.DataAccess.EntityFramework;
using Vehicle.Tracking.DataAccess.Abstract;
using Vehicle.Tracking.DataAccess.Concrete.EntityFramework.Contexts;
using Vehicle.Tracking.Entities.Models;

namespace Vehicle.Tracking.DataAccess.Concrete.EntityFramework
{
    public class LocationHistoryRepository:EfEntityRepositoryBase<LocationHistory, VehicleTrackDbContext>, ILocationHistoryRepository
    {
        public LocationHistoryRepository(VehicleTrackDbContext context) : base(context)
        {
        }
    }
}
