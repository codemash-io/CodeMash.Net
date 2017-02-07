using CodeMash.Interfaces.Data;
using MongoDB.Driver;

namespace CodeMash.Data
{
    public static class MongoRepositoryFactory
    {
        /// <summary>
        /// Get respository instance by reading apiKey and apiAddress from config file. As a fallback plan we use connection string of : "mongodb://localhost" 
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <returns>instance of IMongoRepository</returns>
        public static IMongoRepository<T> Create<T>() where T : IEntity<string>, new()
        {
            return new MongoRepository<T>();
        }

        /// <summary>
        /// Get respository instance which is defined in CodeMash. Specify apiKey of CodeMash. More : http://cloud.codemash.io/connections/api
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="apiKey">apiKey of CodeMash</param>
        /// <returns>instance of IMongoRepository</returns>
        public static IMongoRepository<T> Create<T>(string apiKey) where T : IEntity<string>, new()
        {
            return new MongoRepository<T>(apiKey);
        }

        /// <summary>
        /// Get repository instance by specified connection string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString">Connection string should be provided as MongoUrl instance</param>
        /// <returns>instance of IMongoRepository</returns>
        public static IMongoRepository<T> Create<T>(MongoUrl connectionString) where T : IEntity<string>, new()
        {
            return new MongoRepository<T>(connectionString);
        }

        /// <summary>
        /// Get repository instance by specified connection string and particular collection
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="connectionString">Connection string should be provided as MongoUrl instance</param>
        /// <param name="collectionName">Separate Collection name separately from entity</param>
        /// <returns>instance of IMongoRepository</returns>
        public static IMongoRepository<T> Create<T>(MongoUrl connectionString, string collectionName) where T : IEntity<string>, new()
        {
            return new MongoRepository<T>(connectionString, collectionName);
        }
    }
}