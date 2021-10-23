using System;

namespace Vehicle.Tracking.Entities.Models.Base
{
    public class ModelBase : IEntity
    {
        public int Id { get; set; }

        public int Status { get; set; }

        public int AddedBy { get; set; }

        public DateTime AddedDate { get; set; }

        public int? ChangedBy { get; set; }

        public DateTime? ChangedDate { get; set; }
    }
}
