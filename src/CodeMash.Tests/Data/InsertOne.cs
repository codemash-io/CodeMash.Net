using System;
using CodeMash.Data;
using CodeMash.Interfaces.Data;
using CodeMash.Tests.Data;
using MongoDB.Driver;
using NUnit.Framework;

namespace CodeMash.Tests
{
    [TestFixture]
    public partial class InsertOne : TestBase
    {
        private Guid UniqueIdentifierForTestSession { get; set; }
        private Project Project { get; set; }
        public IRepository<Project> ProjectRepository { get; set; }
        

        protected override void Initialize()
        {
            base.Initialize();

            ProjectRepository = Resolve<IRepository<Project>>();

            // Arrange
            Project = new Project
            {
                Name = "My first project"
            };

            UniqueIdentifierForTestSession = Guid.NewGuid();
        }
        
        [Test]
        [Category("Data")]
        public void Can_insert_project()
        {
            // Act
            ProjectRepository.InsertOne(Project);

            // Assert
            Project.ShouldNotBeNull();
            Project.Id.ShouldNotBeNull();
        }


        [Test]
        [Category("Data")]
        public void Can_insert_into_database_when_collection_name_comes_when_initializing_repo()
        {
            ProjectRepository = CodemashRepositoryFactory.Create<Project>(new MongoUrl("mongodb://localhost"),
                $"LovelyCollection-{UniqueIdentifierForTestSession}");

            ProjectRepository.InsertOne(Project);

            // Assert
            Project.ShouldNotBeNull();
            Project.Id.ShouldNotBeNull();
        }


        [Test]
        [Category("Data")]
        public void Can_insert_into_database_when_collection_name_comes_before_we_call_insert_action()
        {
            var repo = CodemashRepositoryFactory.Create<Project>();
            repo.WithCollection($"LovelyCollection -{UniqueIdentifierForTestSession}")
                .InsertOne(Project);

            var filter = Builders<Project>.Filter.Eq("Name", "My first project");
            var project = repo.WithCollection($"LovelyCollection -{UniqueIdentifierForTestSession}").FindOne(filter);

            project.ShouldNotBeNull();
        }

        protected override void Dispose()
        {
            base.Dispose();

            // TODO : drop collections, instead of removing one by one items. 

            ProjectRepository.DeleteMany(x => true);

            var bsonRepo = CodemashRepositoryFactory.Create<Project>(new MongoUrl("mongodb://localhost")).WithCollection($"LovelyCollection -{UniqueIdentifierForTestSession}");
            bsonRepo?.DeleteMany(_ => true);
        }
    }
}
