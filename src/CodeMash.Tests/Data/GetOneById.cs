using CodeMash.Interfaces.Data;
using CodeMash.Tests.Data;
using NUnit.Framework;
using System.Collections.Generic;
using MongoDB.Bson;

namespace CodeMash.Tests
{
    [TestFixture]
    public class GetOneById : TestBase
    {
        private Project Project { get; set; }
        public IMongoRepository<Project> ProjectRepository { get; set; }
        
        protected override void Initialize()
        {
            base.Initialize();

            ProjectRepository = Resolve<IMongoRepository<Project>>();
        
            // Arrange
            Project = new Project { Name = "My first project" };
        }
        
        [Test]
        [Category("Data")]
        public void Can_get_item_be_its_id_when_id_is_represented_as_string()
        {
            // Act
            ProjectRepository.InsertOne(Project);
            
            var project = ProjectRepository.FindOneById(Project.Id);
            
            // Assert
            project.ShouldNotBeNull();
            project.Name.ShouldEqual("My first project");
        }

        [Test]
        [Category("Data")]
        public void Can_get_item_be_its_id_when_id_is_represented_as_object_Id()
        {
            // Act
            ProjectRepository.InsertOne(Project);
            
            var project = ProjectRepository.FindOneById(new ObjectId(Project.Id));
            
            // Assert
            project.ShouldNotBeNull();
            project.Name.ShouldEqual("My first project");
        }
        
        protected override void Dispose()
        {
            base.Dispose();

            // TODO : drop collections, instead of removing one by one items. 

            var projectsRepo = Resolve<IMongoRepository<Project>>();
            projectsRepo?.DeleteMany(_ => true);
            
        }
    }
}