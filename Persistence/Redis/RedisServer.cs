using StackExchange.Redis;
using Microsoft.Extensions.Configuration;

namespace Persistence.Redis
{
    public class RedisServer
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabase _database;
        private readonly string _configurationString;
        private readonly int _currentDatabaseId = 0;

        public IDatabase Database => _database;

        public RedisServer(string configurationString)
        {
            _configurationString = configurationString;
            _connectionMultiplexer = ConnectionMultiplexer.Connect(_configurationString);
            _database = _connectionMultiplexer.GetDatabase(_currentDatabaseId);
        }

        public void FlushDatabase()
        {
            _connectionMultiplexer.GetServer(_configurationString).FlushDatabase(_currentDatabaseId);
        }
    }
}
