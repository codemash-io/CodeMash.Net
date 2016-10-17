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

        public static List<Project> InitializeProjectScope()
        {
            // save english
            var enLanguage = new ResourceLanguage { Name = "English", NativeName = "English", CultureCode = "en" };
            var ltLanguage = new ResourceLanguage { Name = "Lithuanian", NativeName = "Lietuvių", CultureCode = "lt" };
            
            // Prepare Languages
            enLanguage = DB.InsertOne("Languages", enLanguage);
            ltLanguage = DB.InsertOne("Languages", ltLanguage);
            
            // Add new users
            var user1 = DB.InsertOne("Users", new User { Name = "Domantas", Password = "Very$ecretPa$$w0rd" });
            var user2 = DB.InsertOne("Users", new User { Name = "User2", Password = "Very$ecretPa$$w0rdToo" });

            var supportedLanguages = new List<ObjectId> {enLanguage.Id, ltLanguage.Id};

            var newProject1 = new Project
            {
                SupportedLanguages = supportedLanguages,
                Name = "TestProject",
                Description = "Test Project for sdk testing purpose",
                Categories = Defaults.DefaultResourceCategories(supportedLanguages).ToList(),
                Users = new List<ObjectId> { user1.Id }
            };

             DB.InsertOne("Projects", newProject1);


            var newProject2 = new Project
            {
                SupportedLanguages = new List<ObjectId> {enLanguage.Id},
                Name = "TestProject2",
                Description = "Test Project for sdk testing purpose",
                Categories = Defaults.DefaultResourceCategories(supportedLanguages).ToList(),
                Users = new List<ObjectId> { user1.Id, user2.Id }
            };

            DB.InsertOne("Projects", newProject2);


            return DB.Find<Project>("Projects", x => true);
        }
        
        public static void CleanUpProjectScope()
        {
            DB.DeleteMany<ResourceLanguage>("Languages", _ => true);
            DB.DeleteMany<Project>("Projects", _ => true);
            DB.DeleteMany<User>("Users", _ => true);
        }
        
    }
}   