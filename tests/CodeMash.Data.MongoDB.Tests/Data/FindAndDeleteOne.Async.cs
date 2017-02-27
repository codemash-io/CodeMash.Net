using System.Linq;
using System.Threading.Tasks;
using CodeMash.Interfaces.Data;
using MongoDB.Bson;
using NUnit.Framework;

namespace CodeMash.Data.MongoDB.Tests
{
    [TestFixture]
    public partial class FindAndDeleteOne
    {
        [Test]
        [Category("Data")]
        public async Task Find_and_delete_object_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            await ProjectRepository.InsertOneAsync(Project2);

            var deletedProject = await ProjectRepository.FindOneAndDeleteAsync(Project.Id);

            // Assert
            deletedProject.ShouldNotBeNull();
            deletedProject.Id.ShouldNotBeNull();
            deletedProject.Name.ShouldEqual("My first project");
        }


        [Test]
        [Category("Data")]
        public async Task Cannot_find_and_delete_object_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            await ProjectRepository.InsertOneAsync(Project2);
            

            var deletedProject = await ProjectRepository.FindOneAndDeleteAsync(ObjectId.GenerateNewId().ToString());

            // Assert
            deletedProject.ShouldBeNull();
        }
        
    }
}