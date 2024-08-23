using Newtonsoft.Json;
using StackExchange.Redis;

namespace Persistence.Redis
{
    public class RedisCacheService : ICacheService
    {
        private readonly RedisServer _redisServer;
        private readonly ConnectionMultiplexer _connectionMultiplexer;

        public RedisCacheService(RedisServer redisServer)
        {
            _redisServer = redisServer;
        }

        public void Add(string key, object data)
        {
            string jsonData = JsonConvert.SerializeObject(data);
            _redisServer.Database.StringSet(key, jsonData, TimeSpan.FromMinutes(10));
        }

        public bool Any(string key)
        {
            return _redisServer.Database.KeyExists(key);
        }

        public void Clear()
        {
            _redisServer.FlushDatabase();
        }

        public bool Contains(string key)
        {
            return _redisServer.Database.KeyExists(key);
        }

        public T? Get<T>(string key)
        {
            var value = _redisServer.Database.StringGet(key);
            return value.HasValue ? JsonConvert.DeserializeObject<T>(value) : default;
        }

        public void Remove(string key)
        {
            _redisServer.Database.KeyDelete(key);
        }

        public bool IsRedisActive()
        {
            try
            {
                var pingResult = _redisServer.Database.Ping();
                return pingResult != TimeSpan.Zero;
            }
            catch (RedisConnectionException)
            {
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
