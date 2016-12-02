using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
namespace CodeMash.Net
{
    public static class MongoClientFactory
    {
        private static readonly Dictionary<MongoUrl, MongoClient> _CachedClients = new Dictionary<MongoUrl, MongoClient>();
        private static readonly object _Padlock = new object();

        public static MongoClient Create(MongoUrl mongoUrl)
        {
            lock (_Padlock)
            {
                MongoClient cachedClient = _CachedClients.Where(cached => cached.Key.Equals(mongoUrl)).Select(cached => cached.Value).FirstOrDefault();

                if (cachedClient == null)
                {
                    MongoClient newClient = new MongoClient(mongoUrl);
                    
                    _CachedClients.Add(mongoUrl, newClient);

                    return newClient;
                }

                return cachedClient;
            }
        }

        public static Dictionary<MongoUrl, MongoClient> RegisteredClients => _CachedClients;

        public static void ResetRegisteredClients()
        {
            lock (_Padlock)
            {
                foreach (var cachedClientKey in _CachedClients.Keys)
                {
                    _CachedClients.Remove(cachedClientKey);
                }
                
            }
        }
    }
}
