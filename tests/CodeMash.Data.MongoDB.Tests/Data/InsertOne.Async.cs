using System;
using System.Configuration;
using System.Threading.Tasks;
using CodeMash.Data;
using CodeMash.Extensions;
using CodeMash.Interfaces.Data;
using CodeMash.ServiceModel;
using CodeMash.Tests.Data;
using MongoDB.Driver;
using NUnit.Framework;

namespace CodeMash.Tests
{
    [TestFixture]
    public partial class InsertOne
    {
        [Test]
        [Category("Data")]
        public async Task Can_insert_project_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);

            // Assert
            Project.ShouldNotBeNull();
            Project.Id.ShouldNotBeNull();
        }


        [Test]
        [Category("Data")]
        public async Task Can_insert_into_database_when_collection_name_comes_when_initializing_repo_async()
        {

            var connectionString = ConfigurationManager.AppSettings["MyConnectionString"];
            MongoUrl url = null;
            if (string.IsNullOrEmpty(connectionString))
            {
                var settings = CodeMashBase.Client.Get(new GetAccount());
                if (settings.HasData() && settings.Result.DataBase != null)
                {
                    url = new MongoUrl(settings.Result.DataBase.ConnectionString);
                }
            }
            else
            {
                url = new MongoUrl(connectionString);
            }

            ProjectRepository = MongoRepositoryFactory.Create<Project>(url,
                $"LovelyCollection-{UniqueIdentifierForTestSession}");

            await ProjectRepository.InsertOneAsync(Project);

            // Assert
            Project.ShouldNotBeNull();
            Project.Id.ShouldNotBeNull();
        }


        [Test]
        [Category("Data")]
        public async Task Can_insert_into_database_when_collection_name_comes_before_we_call_insert_action_async()
        {
            var repo = MongoRepositoryFactory.Create<Project>();
            await repo.WithCollection($"LovelyCollection -{UniqueIdentifierForTestSession}")
                .InsertOneAsync(Project);

            var filter = Builders<Project>.Filter.Eq("Name", "My first project");
            var project = await repo.WithCollection($"LovelyCollection -{UniqueIdentifierForTestSession}").FindOneAsync(filter);

            project.ShouldNotBeNull();
        }
        
    }
}
