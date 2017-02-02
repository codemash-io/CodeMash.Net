using CodeMash.Interfaces.Data;
using CodeMash.Tests.Data;
using NUnit.Framework;
using System.Collections.Generic;

namespace CodeMash.Tests
{
    [TestFixture]
    public class InsertMany : TestBase
    {

        public InsertMany()
        {
            Projects = new List<Project>();
        }
        
        private Project Project1 { get; set; }
        private Project Project2 { get; set; }
        private Project Project3 { get; set; }
        private List<Project> Projects { get; set; }
        public IMongoRepository<Project> ProjectRepository { get; set; }
        
        protected override void Initialize()
        {
            base.Initialize();

            ProjectRepository = Resolve<IMongoRepository<Project>>();
        
            // Arrange
            Project1 = new Project { Name = "My first project" };
            Project2 = new Project { Name = "My first project" };
            Project3 = new Project { Name = "My first project" };

            Projects.AddRange(new [] { Project1, Project2, Project3 });
        }
        
        [Test]
        [Category("Data")]
        public void Can_insert_many_projects()
        {
            // Act
            ProjectRepository.InsertMany(Projects);

            var projects = ProjectRepository.Find(x => true);
            
            // Assert
            projects.Count.ShouldEqual(3);
        }
        
        protected override void Dispose()
        {
            base.Dispose();

            // TODO : drop collections, instead of removing one by one items. 

            ProjectRepository.DeleteMany(x => true);
        }
    }
}