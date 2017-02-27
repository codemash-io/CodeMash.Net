using System.Linq;
using CodeMash.Interfaces.Data;
using CodeMash.Data.MongoDB.Tests.Data;
using MongoDB.Driver;
using NUnit.Framework;

namespace CodeMash.Data.MongoDB.Tests
{
    [TestFixture]
    public partial class FindAndReplaceOne : TestBase
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
        public void Replace_the_name()
        {
            // Act
            ProjectRepository.InsertOne(Project);
            ProjectRepository.InsertOne(Project2);

            Project.Name = "My Updated project";

            var replaceOneResult = ProjectRepository.FindOneAndReplace(x => x.Name == "My first project", Project,
                new FindOneAndReplaceOptions<Project> {IsUpsert = false});

            // Assert
            replaceOneResult.ShouldNotBeNull();
            replaceOneResult.Id.ShouldNotBeNull();
            replaceOneResult.Name.ShouldEqual("My first project");
            
            var project = ProjectRepository.FindOne(x => x.Name == "My Updated project");

            project.ShouldNotBeNull();
        }

        [Test]
        [Category("Data")]
        public void Dont_replace_anything_because_cannot_find_it()
        {
            // Act
            ProjectRepository.InsertOne(Project);
            ProjectRepository.InsertOne(Project2);

            var project = Project;
            project.Name = "Me updated project";

            var replaceOneResult = ProjectRepository.FindOneAndReplace(x => x.Name == "My first project!", project);
            
            // Assert
            replaceOneResult.ShouldBeNull();
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