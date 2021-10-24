using Vehicle.Tracking.Core.DataAccess.EntityFramework;
using Vehicle.Tracking.DataAccess.Abstract;
using Vehicle.Tracking.DataAccess.Concrete.EntityFramework.Contexts;
using Vehicle.Tracking.Entities.Models;

namespace Vehicle.Tracking.DataAccess.Concrete.EntityFramework
{
    public class RoleRepository : EfEntityRepositoryBase<Role, VehicleTrackDbContext>, IRoleRepository
    {
        public RoleRepository(VehicleTrackDbContext context) : base(context)
        {
        }
    }
}
