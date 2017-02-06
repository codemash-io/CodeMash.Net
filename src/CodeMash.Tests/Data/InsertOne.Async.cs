using System;
using System.Threading.Tasks;
using CodeMash.Data;
using CodeMash.Interfaces.Data;
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
            ProjectRepository = MongoRepositoryFactory.Create<Project>(new MongoUrl("mongodb://localhost"),
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
