using System.Linq;
using System.Threading.Tasks;
using CodeMash.Interfaces.Data;
using CodeMash.Data.MongoDB.Tests.Data;
using MongoDB.Driver;
using NUnit.Framework;

namespace CodeMash.Data.MongoDB.Tests
{
    [TestFixture]
    public partial class FindOne
    {
        [Test]
        [Category("Data")]
        public async Task Can_get_item_by_specified_name_filter_through_expression_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            
            var project = await ProjectRepository.FindOneAsync(x => x.Name == "My first project");
            
            // Assert
            project.ShouldNotBeNull();
            project.Id.ShouldEqual(Project.Id);
        }

        [Test]
        [Category("Data")]
        public async Task Can_get_item_by_specified_name_filter_through_filter_of_mongodb_driver_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);

            var filter1 = Builders<Project>.Filter.Eq("Name", "My first project");
            var projectResult1 = await ProjectRepository.FindOneAsync(filter1);

            var filter2 = Builders<Project>.Filter.Eq(x => x.Name, "My first project");
            var projectResult2 = await ProjectRepository.FindOneAsync(filter2);

            // Assert
            projectResult1.ShouldNotBeNull();
            projectResult1.Id.ShouldEqual(Project.Id);
            projectResult2.ShouldNotBeNull();
            projectResult2.Id.ShouldEqual(Project.Id);
        }


        


        [Test]
        [Category("Data")]
        public async Task Cannot_get_item_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            await ProjectRepository.InsertOneAsync(Project2);

            var project = await ProjectRepository.FindOneAsync(Builders<Project>.Filter.Eq("Categories.Name", "Buttons7"));

            // Assert
            project.ShouldBeNull();
        }
        
    }
}