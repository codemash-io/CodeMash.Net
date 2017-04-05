using CodeMash.Interfaces;
using MongoDB.Driver;

namespace CodeMash.Data.MongoDB
{
    public static class MongoRepositoryFactory
    {
        /// <summary>
        /// Get respository instance by reading apiKey and apiAddress from config file. As a fallback plan we use connection string of : "mongodb://localhost" 
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <returns>instance of IRepository</returns>
        public static IRepository<T> Create<T>() where T :  new()
        {
            return new Repository<T>();
        }

        /// <summary>
        /// Get respository instance which is defined in CodeMash. Specify apiKey of CodeMash. More : http://cloud.codemash.io/connections/api
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="apiKey">apiKey of CodeMash</param>
        /// <returns>instance of IRepository</returns>
        public static IRepository<T> Create<T>(string apiKey) where T : new()
        {
            return new Repository<T>(apiKey);
        }

        /// <summary>
        /// Get repository instance by specified connection string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="connectionString">Connection string should be provided as MongoUrl instance</param>
        /// <returns>instance of IRepository</returns>
        public static IRepository<T> Create<T>(MongoUrl connectionString) where T : new()
        {
            return new Repository<T>(connectionString);
        }

        /// <summary>
        /// Get repository instance by specified connection string and particular collection
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <param name="connectionString">Connection string should be provided as MongoUrl instance</param>
        /// <param name="collectionName">Separate Collection name separately from entity</param>
        /// <returns>instance of IRepository</returns>
        public static IRepository<T> Create<T>(MongoUrl connectionString, string collectionName) where T : new()
        {
            return new Repository<T>(connectionString, collectionName);
        }
    }
}