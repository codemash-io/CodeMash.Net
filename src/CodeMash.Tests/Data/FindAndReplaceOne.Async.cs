using System.Linq;
using CodeMash.Interfaces.Data;
using CodeMash.Tests.Data;
using MongoDB.Driver;
using NUnit.Framework;
using System.Threading.Tasks;

namespace CodeMash.Tests
{
    [TestFixture]
    public partial class FindAndReplaceOne
    {

        [Test]
        [Category("Data")]
        public async Task Replace_the_name_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            await ProjectRepository.InsertOneAsync(Project2);

            Project.Name = "My Updated project";

            var replaceOneResult = await ProjectRepository.FindOneAndReplaceAsync(x => x.Name == "My first project", Project,
                new FindOneAndReplaceOptions<Project> {IsUpsert = false});

            // Assert
            replaceOneResult.ShouldNotBeNull();
            replaceOneResult.Id.ShouldNotBeNull();
            replaceOneResult.Name.ShouldEqual("My first project");
            
            var project = await ProjectRepository.FindOneAsync(x => x.Name == "My Updated project");

            project.ShouldNotBeNull();
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

            var replaceOneResult = await ProjectRepository.FindOneAndReplaceAsync(x => x.Name == "My first project!", project);
            
            // Assert
            replaceOneResult.ShouldBeNull();
        }
        
    }
}