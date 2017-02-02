using System;
using CodeMash.Data;
using CodeMash.Interfaces.Data;
using CodeMash.Tests.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using NUnit.Framework;

namespace CodeMash.Tests
{
    [TestFixture]
    public class InsertOne : TestBase
    {

        [BsonIgnoreExtraElements]
        private class TestProject
        {
            public string Name { get; set; }
            public DateTime StartDate { get; set; }
        }

        private class TestProject2
        {
            public string Name { get; set; }
            public DateTime StartDate { get; set; }
        }

        private Guid UniqueIdentifierForTestSession { get; set; }
        private Project Project { get; set; }
        public IMongoRepository<Project> ProjectRepository { get; set; }
        private IMongoRepository<TestProject> TestProjectRepository { get; set; }
        private IMongoRepository<TestProject2> TestProjectRepository2 { get; set; }
        

        protected override void Initialize()
        {
            base.Initialize();

            ProjectRepository = Resolve<IMongoRepository<Project>>();
            TestProjectRepository = MongoRepositoryFactory.Create<TestProject>();
            TestProjectRepository2 = MongoRepositoryFactory.Create<TestProject2>();

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
        public void Can_insert_entity_wich_doesnt_have_inherritance_from_IEntity()
        {
            var entity = new TestProject
            {
                Name = $"My little project - {UniqueIdentifierForTestSession}",
                StartDate = DateTime.Now.AddYears(-35)
            };

            // Act
            TestProjectRepository.InsertOne(entity);

            var testProject = TestProjectRepository.FindOne(x => x.Name == $"My little project - {UniqueIdentifierForTestSession}");

            // Assert
            testProject.ShouldNotBeNull();
            testProject.StartDate.Year.ShouldEqual(DateTime.Now.AddYears(-35).Year);

        }


        [Test]
        [Category("Data")]
        public void Cannot_insert_entity_wich_doesnt_have_inherritance_from_IEntity_and_ignore_extra_elements()
        {
            var entity = new TestProject2
            {
                Name = $"My little project - {UniqueIdentifierForTestSession}",
                StartDate = DateTime.Now.AddYears(-35)
            };

            TestProjectRepository2.InsertOne(entity);
            // Act
            var filter = Builders<TestProject2>.Filter.Eq("Name", $"My little project - {UniqueIdentifierForTestSession}");
            
            var exception = typeof(FormatException).ShouldBeThrownBy(() => TestProjectRepository2.FindOne(filter));
            
            // Assert
            exception.Message.Contains("Element '_id' does not match any field or property of class ").ShouldEqual(true);
        }

        [Test]
        [Category("Data")]
        public void Can_insert_into_database_bson_document()
        {
            var entity = new BsonDocument
            {
                {"Name", $"My little project - {UniqueIdentifierForTestSession}"},
                {"StartDate", DateTime.Now.AddYears(-35)}
            };

            // Act
            var repository = MongoRepositoryFactory.Create<BsonDocument>().WithCollection($"LovelyCollection-{UniqueIdentifierForTestSession}");
            repository.InsertOne(entity);

            var filter = Builders<BsonDocument>.Filter.Eq("Name", $"My little project - {UniqueIdentifierForTestSession}");
            var testProject = repository.FindOne(filter);

            // Assert
            testProject.ShouldNotBeNull();
            testProject["_id"].ShouldBe<BsonObjectId>();
            testProject["StartDate"].AsBsonDateTime.IsValidDateTime.ShouldEqual(true);
            testProject["StartDate"].AsBsonDateTime.ToUniversalTime().Year.ShouldEqual(DateTime.Now.AddYears(-35).Year);
        }

        [Test]
        [Category("Data")]
        public void Can_insert_into_database_when_collection_name_comes_when_initializing_repo()
        {
            ProjectRepository = MongoRepositoryFactory.Create<Project>(new MongoUrl("mongodb://localhost"),
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
            var repo = MongoRepositoryFactory.Create<BsonDocument>();
            repo.WithCollection($"LovelyCollection -{UniqueIdentifierForTestSession}")
                .InsertOne(Project.ToBsonDocument());

            var filter = Builders<BsonDocument>.Filter.Eq("Name", "My first project");
            var foundItem = repo.WithCollection($"LovelyCollection -{UniqueIdentifierForTestSession}").FindOne(filter);
            foundItem.ShouldNotBeNull();
            foundItem["_id"].ShouldBe<BsonNull>();
        }

        protected override void Dispose()
        {
            base.Dispose();

            // TODO : drop collections, instead of removing one by one items. 

            ProjectRepository.DeleteMany(x => true);
            TestProjectRepository.DeleteMany(_ => true);
            TestProjectRepository2.DeleteMany(_ => true);

            var bsonRepo = MongoRepositoryFactory.Create<Project>(new MongoUrl("mongodb://localhost")).WithCollection($"LovelyCollection -{UniqueIdentifierForTestSession}");
            bsonRepo?.DeleteMany(_ => true);
        }
    }
}
