using MongoDB.Driver;

namespace Auditory.API.Data.Context
{
    public class AuditoryMongoContext : IAuditoryContext
    {
        private readonly IConfiguration _configuration;
        private IMongoDatabase _mongoDatabase;
        public AuditoryMongoContext(IConfiguration configuration)
        {
            _configuration = configuration;            
            _mongoDatabase = new MongoClient(string.Concat(
                                                 "mongodb://",
                                                configuration.GetConnectionString("UserRecord:Username"), ":",
                                                configuration.GetConnectionString("UserRecord:Password"), "@",
                                                configuration.GetConnectionString("UserRecord:HostName"), ":",
                                                configuration.GetConnectionString("UserRecord:Port"),
                                                "/?authSource=admin")                                                
                                            ).GetDatabase(configuration.GetConnectionString("UserRecord:DataBase"));

        }
        public IMongoCollection<UserRecordHistoryMongo> UserRecordCollection =>
            _mongoDatabase.GetCollection<UserRecordHistoryMongo>(_configuration.GetConnectionString("UserRecord:Collection"));

    }
}
