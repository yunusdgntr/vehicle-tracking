using MongoDB.Bson;

namespace Vehicle.Tracking.Core.Entities
{
    public abstract class DocumentDbEntity
    {
        public ObjectId Id { get; set; }
        public string ObjectId => Id.ToString();
    }
}
