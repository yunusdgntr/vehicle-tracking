using System.Collections.Generic;
using Vehicle.Tracking.Entities.Models.Filter;

namespace Vehicle.Tracking.Entities.Models.Request
{
    public class UserRequest
    {
        public User Entity { get; set; }

        public List<User> EntityList { get; set; }

        public UserFilter Filter { get; set; }
    }
}
