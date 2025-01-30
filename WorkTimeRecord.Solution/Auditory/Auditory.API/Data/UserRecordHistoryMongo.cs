using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Auditory.API.Data
{
    public class UserRecordHistoryMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("userName")]
        public string UserName { get; set; }

        [BsonElement("firstName")]
        public string FirstName { get; set; }

        [BsonElement("lastName")]
        public string LastName { get; set; }

        [BsonElement("lastRecord")]
        public DateTime LastRecord { get; set; }

        [BsonElement("mode")]
        public string Mode { get; set; }
    }
}
