using System;
using CodeMash.Client;
using CodeMash.Interfaces;

namespace CodeMash.Repository
{
    public static class CodeMashRepositoryFactory
    {
        /// <summary>
        /// Get repository instance by reading CodeMash configuration from config file. 
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <returns>instance of IRepository</returns>
        public static IRepository<T> Create<T>(string settingsFileName = "appsettings.json") where T : IEntity
        {
            var settings = new CodeMashSettings(null, settingsFileName);
            
            return new CodeMashRepository<T>(settings);
        }
        
        /// <summary>
        /// Get repository instance by reading CodeMash configuration from config file. 
        /// </summary>
        /// <typeparam name="T">Entity</typeparam>
        /// <returns>instance of IRepository</returns>
        public static IRepository<T> Create<T>(string apiKey, Guid projectId) where T : IEntity
        {
            var settings = new CodeMashSettings(apiKey, projectId);
            
            return new CodeMashRepository<T>(settings);
        }
    }
}