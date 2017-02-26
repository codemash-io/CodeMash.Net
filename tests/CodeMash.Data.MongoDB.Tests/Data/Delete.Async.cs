using System.Threading.Tasks;
using CodeMash.Tests.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using NUnit.Framework;

namespace CodeMash.Tests
{
    [TestFixture]
    public partial class Delete : TestBase
    {
        
        [Test]
        [Category("Data")]
        public async Task Can_delete_item_from_db_when_string_is_provided_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            var deleteResult = ProjectRepository.DeleteOne(Project.Id);

            // Assert
            deleteResult.ShouldNotBeNull();
            deleteResult.DeletedCount.ShouldEqual(1);
        }

        [Test]
        [Category("Data")]
        public async Task Can_delete_item_from_db_when_objectId_is_provided_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            var deleteResult = ProjectRepository.DeleteOne(ObjectId.Parse(Project.Id));

            // Assert
            deleteResult.ShouldNotBeNull();
            deleteResult.DeletedCount.ShouldEqual(1);
        }

        [Test]
        [Category("Data")]
        public async Task Can_delete_item_from_db_when_expression_is_provided_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            var deleteResult = ProjectRepository.DeleteOne(x => x.Id == Project.Id);

            // Assert
            deleteResult.ShouldNotBeNull();
            deleteResult.DeletedCount.ShouldEqual(1);
        }

        [Test]
        [Category("Data")]
        public async Task Can_delete_item_from_db_when_filter_definition_is_provided_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            var deleteResult = ProjectRepository.DeleteOne(Builders<Project>.Filter.Eq(x => x.Id, Project.Id));

            // Assert
            deleteResult.ShouldNotBeNull();
            deleteResult.DeletedCount.ShouldEqual(1);
        }


        [Test]
        [Category("Data")]
        public async Task Can_delete_first_matched_item_from_db_when_filter_is_provided_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project); Project.Id = string.Empty;
            await ProjectRepository.InsertOneAsync(Project); Project.Id = string.Empty;
            await ProjectRepository.InsertOneAsync(Project);
            var deleteResult = await ProjectRepository.DeleteOneAsync(Builders<Project>.Filter.Eq(x => x.Name, "My first project"));

            // Assert
            deleteResult.ShouldNotBeNull();
            deleteResult.DeletedCount.ShouldEqual(1);
        }

        [Test]
        [Category("Data")]
        public async Task Cannot_delete_item_from_db_when_filter_dont_match_the_record_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            var deleteResult = await ProjectRepository.DeleteOneAsync(Builders<Project>.Filter.Eq(x => x.Name, "My first project!"));

            // Assert
            deleteResult.ShouldNotBeNull();
            deleteResult.DeletedCount.ShouldEqual(0);
        }

        [Test]
        [Category("Data")]
        public async Task When_record_is_deleted_from_db_should_not_get_from_db_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            var deleteResult = await ProjectRepository.DeleteOneAsync(Builders<Project>.Filter.Eq(x => x.Name, "My first project"));

            var project = await ProjectRepository.FindOneByIdAsync(Project.Id);

            // Assert
            deleteResult.ShouldNotBeNull();
            deleteResult.DeletedCount.ShouldEqual(1);
            project.ShouldBeNull();
        }
        
    }
}