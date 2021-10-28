using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vehicle.Tracking.Entities.Models.Base;

namespace Vehicle.Tracking.Entities.Models
{
    public class Right : ModelBase
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public int RoleId { get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}
