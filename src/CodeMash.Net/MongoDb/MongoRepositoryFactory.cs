using MongoDB.Driver;

namespace CodeMash.Net
{

    public static class MongoRepositoryFactory
    {
        //public static IMongoRepository<T> Create<T>() // where T : IEntity<string>
        //{            
        //    return new MongoRepository<T>();
        //}

        public static IMongoRepository<T> Create<T>(string connectionString) // where T : IEntity<string>
        {
            return new MongoRepository<T>(new MongoUrl(connectionString));
        }

        public static IMongoRepository<T> Create<T>(string connectionString, string collectionName) // where T : IEntity<string>
        {
            return new MongoRepository<T>(new MongoUrl(connectionString), collectionName);
        }

        public static IDatabaseRepository Create()
        {
            return new DatabaseRepository();
        }
    }
}
