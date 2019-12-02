using System;
using CodeMash.Interfaces;
using CodeMash.Common;

namespace CodeMash.Repository
{
    public static class CodeMashRepositoryFactory
    {
        /// <summary>
        /// Get repository instance by reading CodeMash configuration from config file. 
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <returns>instance of IRepository</returns>
        public static IRepository<T> Create<T>(string settingsFileName = "appsettings.json") where T :  IEntity
        {
            var settings = new CodeMashDatabaseSettings(null, settingsFileName);
            
            return new CodeMashRepository<T>(settings);
        }
        
        /// <summary>
        /// Get repository instance by reading CodeMash configuration from config file. 
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <returns>instance of IRepository</returns>
        public static IRepository<T> Create<T>(string apiKey, Guid projectId, string region = null) where T :  IEntity
        {
            var settings = new CodeMashDatabaseSettings(apiKey, projectId, region);
            
            return new CodeMashRepository<T>(settings);
        }
    }
}