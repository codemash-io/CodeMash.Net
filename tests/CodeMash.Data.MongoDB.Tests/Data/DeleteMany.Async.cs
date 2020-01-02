using System.Linq;
using CodeMash.Interfaces.Data;
using CodeMash.Data.MongoDB.Tests.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CodeMash.Data.MongoDB.Tests
{
    [TestFixture]
    public partial class DeleteMany
    {
        [Test]
        [Category("Data")]
        public async Task Can_delete_item_from_db_when_expression_is_provided_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            await ProjectRepository.InsertOneAsync(Project2);
            var deleteResult = ProjectRepository.DeleteMany(x => x.Name == "My first project");

            // Assert
            deleteResult.ShouldNotBeNull();
            deleteResult.DeletedCount.ShouldEqual(2);
        }


        [Test]
        [Category("Data")]
        public async Task Can_delete_item_from_db_when_filter_definition_is_provided_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            await ProjectRepository.InsertOneAsync(Project2);
            var deleteResult = await ProjectRepository.DeleteManyAsync(Builders<Project>.Filter.Eq(x => x.Name, "My first project"));

            // Assert
            deleteResult.ShouldNotBeNull();
            deleteResult.DeletedCount.ShouldEqual(2);
        }

        [Test]
        [Category("Data")]
        public async Task Cannot_delete_from_db_when_filter_didnt_match_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            await ProjectRepository.InsertOneAsync(Project2);
            var deleteResult = await ProjectRepository.DeleteManyAsync(Builders<Project>.Filter.Eq(x => x.Name, "My first project!"));

            // Assert
            deleteResult.ShouldNotBeNull();
            deleteResult.DeletedCount.ShouldEqual(0);
        }
    }
}