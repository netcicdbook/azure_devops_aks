using MongoDB.Driver;

namespace Auditory.API.Data.Context
{
    public interface IAuditoryContext
    {
        IMongoCollection<UserRecordHistoryMongo> UserRecordCollection { get; }
    }
}
