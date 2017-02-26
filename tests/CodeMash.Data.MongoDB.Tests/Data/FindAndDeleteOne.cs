using System.Linq;
using CodeMash.Interfaces.Data;
using CodeMash.Tests.Data;
using MongoDB.Bson;
using NUnit.Framework;

namespace CodeMash.Tests
{
    [TestFixture]
    public partial class FindAndDeleteOne : TestBase
    {
        private Project Project { get; set; }
        private Project Project2 { get; set; }
        public IRepository<Project> ProjectRepository { get; set; }
        public IRepository<ResourceLanguage> LanguagesRepository { get; set; }

        protected override void Initialize()
        {
            base.Initialize();

            ProjectRepository = Resolve<IRepository<Project>>();
            LanguagesRepository = Resolve<IRepository<ResourceLanguage>>();

            var ltLangage = new ResourceLanguage {Name = "Lithuanian", NativeName = "Lietuviu", CultureCode = "lt-LT"};
            var usLangage = new ResourceLanguage {Name = "English", NativeName = "English", CultureCode = "en-US"};
            var gbLangage = new ResourceLanguage {Name = "UK", NativeName = "Great Britain", CultureCode = "en-GB"};

            LanguagesRepository.InsertMany(new[] {gbLangage, usLangage, ltLangage});

            var ids =
                LanguagesRepository.Find(x => x.CultureCode == "en-US" || x.CultureCode == "en-GB")
                    .Select(x => x.Id)
                    .ToList();

            // Arrange
            Project = new Project
            {
                Name = "My first project",
                Categories = Defaults.DefaultResourceCategories(ids).ToList(),
                SupportedLanguages = ids
            };

            Project2 = new Project
            {
                Name = "My first project",
                Categories = Defaults.DefaultResourceCategories(ids).ToList(),
                SupportedLanguages = ids
            };
        }
        

        [Test]
        [Category("Data")]
        public void Find_and_delete_object()
        {
            // Act
            ProjectRepository.InsertOne(Project);
            ProjectRepository.InsertOne(Project2);

            var deletedProject = ProjectRepository.FindOneAndDelete(Project.Id);

            // Assert
            deletedProject.ShouldNotBeNull();
            deletedProject.Id.ShouldNotBeNull();
            deletedProject.Name.ShouldEqual("My first project");
        }


        [Test]
        [Category("Data")]
        public void Cannot_find_and_delete_object()
        {
            // Act
            ProjectRepository.InsertOne(Project);
            ProjectRepository.InsertOne(Project2);
            

            var deletedProject = ProjectRepository.FindOneAndDelete(ObjectId.GenerateNewId().ToString());

            // Assert
            deletedProject.ShouldBeNull();
        }

        protected override void Dispose()
        {
            base.Dispose();

            // TODO : drop collections, instead of removing one by one items. 
            ProjectRepository.DeleteMany(x => true);

            LanguagesRepository.DeleteMany(x => true);

        }
    }
}