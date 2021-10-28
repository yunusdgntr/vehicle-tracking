using System.Threading.Tasks;

namespace Vehicle.Tracking.Business.Services.Abstract
{
    public interface IVehicleManager
    {
        Entities.Models.Vehicle Add(Entities.Models.Vehicle entity);
        Entities.Models.Vehicle Update(Entities.Models.Vehicle entity);
        void Delete(Entities.Models.Vehicle entity);
    }
}
