using MongoDB.Bson;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace CodeMash.Net.Tests
{
    public static class Bootstrapper
    {
        public static ServiceStackClientType ServiceClientType = ServiceStackClientType.Json;
        public static string ServiceClientBaseUri = ConfigurationManager.AppSettings["CodeMashApiAddress"];

        public static async Task<List<Project>> InitializeProjectScope()
        {
            // save english
            var enLanguage = new ResourceLanguage { Name = "English", NativeName = "English", CultureCode = "en" };
            var ltLanguage = new ResourceLanguage { Name = "Lithuanian", NativeName = "Lietuvių", CultureCode = "lt" };
            
            // Prepare Languages
            enLanguage = await CodeMash.InsertOneAsync("Languages", enLanguage);
            ltLanguage = await CodeMash.InsertOneAsync("Languages", ltLanguage);
            
            // Add new users
            var user1 = await CodeMash.InsertOneAsync("Users", new User { Name = "Domantas", Password = "Very$ecretPa$$w0rd" });
            var user2 = await CodeMash.InsertOneAsync("Users", new User { Name = "User2", Password = "Very$ecretPa$$w0rdToo" });

            var supportedLanguages = new List<ObjectId> {enLanguage.Id, ltLanguage.Id};

            var newProject1 = new Project
            {
                SupportedLanguages = supportedLanguages,
                Name = "TestProject",
                Description = "Test Project for sdk testing purpose",
                Categories = Defaults.DefaultResourceCategories(supportedLanguages).ToList(),
                Users = new List<ObjectId> { user1.Id }
            };

            await  CodeMash.InsertOneAsync("Projects", newProject1);


            var newProject2 = new Project
            {
                SupportedLanguages = new List<ObjectId> {enLanguage.Id},
                Name = "TestProject2",
                Description = "Test Project for sdk testing purpose",
                Categories = Defaults.DefaultResourceCategories(supportedLanguages).ToList(),
                Users = new List<ObjectId> { user1.Id, user2.Id }
            };

            await CodeMash.InsertOneAsync("Projects", newProject2);


            return await CodeMash.FindAsync<Project>("Projects", x => true);
        }
        
        public static async Task CleanUpProjectScope()
        {
            await CodeMash.DeleteManyAsync<ResourceLanguage>("Languages", _ => true);
            await CodeMash.DeleteManyAsync<Project>("Projects", _ => true);
            await CodeMash.DeleteManyAsync<User>("Users", _ => true);
        }
        
    }
}   