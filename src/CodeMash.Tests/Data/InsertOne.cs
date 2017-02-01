using CodeMash.Interfaces.Data;
using CodeMash.Tests.Data;
using NUnit.Framework;

namespace CodeMash.Tests
{
    [TestFixture]
    public class InsertOne : TestBase
    {
        public IMongoRepository<Project> ProjectRepository { get; set; }

        protected override void Initialize()
        {
            base.Initialize();

            ProjectRepository = Resolve<IMongoRepository<Project>>();
        }

        [Test]
        [Category("Data")]
        public void Can_insert_project()
        {

            // Arrange
            var project = new Project
            {
                Name = "My first project"
            };

            // Act
            ProjectRepository.InsertOne(project);

            // Assert
            project.ShouldNotBeNull();
            project.Id.ShouldNotBeNull();
        }

        protected override void Dispose()
        {
            base.Dispose();

            var projectsRepo = Resolve<IMongoRepository<Project>>();
            projectsRepo?.DeleteMany(_ => true);
        }
    }
}
