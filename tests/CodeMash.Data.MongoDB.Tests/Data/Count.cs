using System.Collections.Generic;
using CodeMash.Data.MongoDB.Tests.Data;
using CodeMash.Interfaces.Data;
using NUnit.Framework;

namespace CodeMash.Data.MongoDB.Tests
{
    [TestFixture]
    public partial class Count : TestBase
    {
        public Count()
        {
            Projects = new List<Project>();
        }
        
        private Project Project1 { get; set; }
        private Project Project2 { get; set; }
        private Project Project3 { get; set; }
        private List<Project> Projects { get; set; }
        public IRepository<Project> ProjectRepository { get; set; }
        
        protected override void Initialize()
        {
            base.Initialize();

            ProjectRepository = Resolve<IRepository<Project>>();
        
            // Arrange
            Project1 = new Project { Name = "My first project" };
            Project2 = new Project { Name = "My first project2" };
            Project3 = new Project { Name = "My first project2" };

            Projects = new List<Project>();
            Projects.AddRange(new [] { Project1, Project2, Project3 });
        }
        
        [Test]
        [Category("Data")]
        public void Can_get_right_count()
        {
            // Act
            ProjectRepository.InsertMany(Projects);

            var count = ProjectRepository.Count(x => x.Name == "My first project2");
            
            // Assert
            count.ShouldEqual(2);
        }
        
        protected override void Dispose()
        {
            base.Dispose();

            // TODO : drop collections, instead of removing one by one items. 

            ProjectRepository.DeleteMany(x => true);
        }
    }
}