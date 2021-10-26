using System.Collections.Generic;
using Vehicle.Tracking.Entities.Models.Common;

namespace Vehicle.Tracking.Entities.Models.Response
{
    public class UserResponse
    {
        public User Entity { get; set; }

        public List<User> EntityList { get; set; }

        public Token Token { get; set; }

        public Error Error { get; set; }
    }
}
