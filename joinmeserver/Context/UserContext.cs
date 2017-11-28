using joinmeserver.Models;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace joinmeserver.Context
{
    public class UserContext
    {
        readonly IMongoDatabase _database = null;

        public UserContext(IOptions<Settings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);

            if (client != null)
                _database = client.GetDatabase(settings.Value.Database);
        }

        public IMongoCollection<User> Users
        {
            get
            {
                return _database.GetCollection<User>("User");
            }
        }
    }
}
