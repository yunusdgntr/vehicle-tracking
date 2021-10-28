using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vehicle.Tracking.Entities.Models.Base;

namespace Vehicle.Tracking.Entities.Models
{
    public class Role : ModelBase
    {
        [MaxLength(255)]
        public string Name { get; set; }

        public ICollection<Right> Rights { get; set; }

    }
}
