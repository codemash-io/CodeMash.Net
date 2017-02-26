using CodeMash.Interfaces.Data;
using CodeMash.Tests.Data;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace CodeMash.Tests
{
    [TestFixture]
    public partial class FindOneById 
    {
        [Test]
        [Category("Data")]
        public async Task Can_get_item_be_its_id_when_id_is_represented_as_string_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            
            var project = await ProjectRepository.FindOneByIdAsync(Project.Id);
            
            // Assert
            project.ShouldNotBeNull();
            project.Name.ShouldEqual("My first project");
        }

        [Test]
        [Category("Data")]
        public async Task Can_get_item_be_its_id_when_id_is_represented_as_object_Id_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            
            var project = await ProjectRepository.FindOneByIdAsync(new ObjectId(Project.Id));
            
            // Assert
            project.ShouldNotBeNull();
            project.Name.ShouldEqual("My first project");
        }
        
    }
}