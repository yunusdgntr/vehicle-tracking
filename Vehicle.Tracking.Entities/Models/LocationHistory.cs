using System.ComponentModel.DataAnnotations.Schema;
using Vehicle.Tracking.Entities.Models.Base;

namespace Vehicle.Tracking.Entities.Models
{
    public class LocationHistory : ModelBase
    {
        public double Lat { get; set; }

        public double Long { get; set; }

        public int Speed { get; set; }

        public int VehicleId { get; set; }

        [ForeignKey("VehicleId")]
        public virtual Vehicle Vehicle { get; set; }

    }
}
