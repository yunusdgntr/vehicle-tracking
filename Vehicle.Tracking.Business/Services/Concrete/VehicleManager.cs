using System;
using System.Threading.Tasks;
using Vehicle.Tracking.Business.Services.Abstract;
using Vehicle.Tracking.DataAccess.Abstract;

namespace Vehicle.Tracking.Business.Services.Concrete
{
    public class VehicleManager : IVehicleManager
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleManager(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public Entities.Models.Vehicle Add(Entities.Models.Vehicle entity)
        {
            return _vehicleRepository.Add(entity);
        }

        public void Delete(Entities.Models.Vehicle entity)
        {
            _vehicleRepository.Delete(entity);
        }

        public Entities.Models.Vehicle Update(Entities.Models.Vehicle entity)
        {
            return _vehicleRepository.Update(entity);
        }
    }
}
