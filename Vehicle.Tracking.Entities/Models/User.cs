using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Vehicle.Tracking.Entities.Models.Base;

namespace Vehicle.Tracking.Entities.Models
{
    public class User: ModelBase
    {
        [MaxLength(40)]
        public string Name { get; set; }

        [MaxLength(40)]
        public string Surname { get; set; }

        [Required]
        [MaxLength(255)]
        public string Email { get; set; }

        public string RefreshToken { get; set; }

        public DateTimeOffset RefreshTokenExpiresDateTime { get; set; }

        [Required]
        [MaxLength(255)]
        public string Password { get; set; }

        public string NameSurname
        {
            get
            {
                return String.Format("{0} {1}", this.Name, this.Surname);
            }
            set
            {
                string _text = String.Format("{0} {1}", this.Name, this.Surname);
                _text = value;
            }
        }

        public virtual ICollection<Right> Right { get; set; }
    }
}
