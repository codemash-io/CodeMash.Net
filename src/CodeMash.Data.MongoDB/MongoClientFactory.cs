using System.Collections.Generic;
using System.Linq;
using MongoDB.Driver;

namespace CodeMash.Data.MongoDB
{
    public static class MongoClientFactory
    {
        private static readonly object Padlock = new object();

        public static MongoClient Create(MongoUrl mongoUrl)
        {
            lock (Padlock)
            {
                var cachedClient = RegisteredClients.Where(cached => cached.Key.Equals(mongoUrl)).Select(cached => cached.Value).FirstOrDefault();

                if (cachedClient != null)
                {
                    return cachedClient;
                }
                var newClient = new MongoClient(mongoUrl);

                RegisteredClients.Add(mongoUrl, newClient);

                return newClient;
            }
        }

        public static Dictionary<MongoUrl, MongoClient> RegisteredClients { get; } = new Dictionary<MongoUrl, MongoClient>();

        public static void ResetRegisteredClients()
        {
            lock (Padlock)
            {
                foreach (var cachedClientKey in RegisteredClients.Keys)
                {
                    RegisteredClients.Remove(cachedClientKey);
                }

            }
        }
    }
}