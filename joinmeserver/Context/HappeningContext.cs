using joinmeserver.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace joinmeserver.Context
{
    public class HappeningContext
    {
        private readonly IMongoDatabase _database = null;

        public HappeningContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);

            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<Happening> Happenings
        {
            get
            {
                return _database.GetCollection<Happening>("Happening");
            }
        }
    }
}
