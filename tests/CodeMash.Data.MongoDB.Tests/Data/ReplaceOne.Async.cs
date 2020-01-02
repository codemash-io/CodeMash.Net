using System.Threading.Tasks;
using CodeMash.Data.MongoDB.Tests.Data;
using MongoDB.Driver;
using NUnit.Framework;

namespace CodeMash.Data.MongoDB.Tests
{
    [TestFixture]
    public partial class ReplaceOne
    {
        [Test]
        [Category("Data")]
        public async Task Replase_entire_object_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            await ProjectRepository.InsertOneAsync(Project2);

            var project = Project;
            project.Name = "Me updated project";

            var replaceOneResult = await ProjectRepository.ReplaceOneAsync(x => x.Name == "My first project", project);

            // Assert
            replaceOneResult.ShouldNotBeNull();
            replaceOneResult.MatchedCount.ShouldEqual(1);
            replaceOneResult.ModifiedCount.ShouldEqual(1);
        }


        [Test]
        [Category("Data")]
        public async Task Inserts_new_object_because_it_cannot_find_it_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            await ProjectRepository.InsertOneAsync(Project2);

            var project = new Project
            {
                Name = "My Updated project"
            };

            var replaceOneResult = await ProjectRepository.ReplaceOneAsync(x => x.Name == "My first project!", project,
                new UpdateOptions() {IsUpsert = true});

            // Assert
            replaceOneResult.ShouldNotBeNull();
            replaceOneResult.MatchedCount.ShouldEqual(0);
            replaceOneResult.ModifiedCount.ShouldEqual(0);
        }

        [Test]
        [Category("Data")]
        public async Task Dont_replace_anything_because_cannot_find_it_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            await ProjectRepository.InsertOneAsync(Project2);

            var project = Project;
            project.Name = "Me updated project";

            var replaceOneResult = await ProjectRepository.ReplaceOneAsync(x => x.Name == "My first project!", project);

            // Assert
            replaceOneResult.ShouldNotBeNull();
            replaceOneResult.MatchedCount.ShouldEqual(0);
            replaceOneResult.ModifiedCount.ShouldEqual(0);
        }
    }
}