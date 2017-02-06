using System.Linq;
using CodeMash.Interfaces.Data;
using CodeMash.Tests.Data;
using MongoDB.Driver;
using NUnit.Framework;

namespace CodeMash.Tests
{
    [TestFixture]
    public partial class ReplaceOne : TestBase
    {
        private Project Project { get; set; }
        private Project Project2 { get; set; }
        public IMongoRepository<Project> ProjectRepository { get; set; }
        public IMongoRepository<ResourceLanguage> LanguagesRepository { get; set; }

        protected override void Initialize()
        {
            base.Initialize();

            ProjectRepository = Resolve<IMongoRepository<Project>>();
            LanguagesRepository = Resolve<IMongoRepository<ResourceLanguage>>();

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
        public void Replase_entire_object()
        {
            // Act
            ProjectRepository.InsertOne(Project);
            ProjectRepository.InsertOne(Project2);

            var project = Project;
            project.Name = "Me updated project";

            var replaceOneResult = ProjectRepository.ReplaceOne(x => x.Name == "My first project", project);

            // Assert
            replaceOneResult.ShouldNotBeNull();
            replaceOneResult.MatchedCount.ShouldEqual(1);
            replaceOneResult.ModifiedCount.ShouldEqual(1);
        }


        [Test]
        [Category("Data")]
        public void Inserts_new_object_because_it_cannot_find_it()
        {
            // Act
            ProjectRepository.InsertOne(Project);
            ProjectRepository.InsertOne(Project2);

            var project = new Project
            {
                Name = "My Updated project"
            };

            var replaceOneResult = ProjectRepository.ReplaceOne(x => x.Name == "My first project!", project,
                new UpdateOptions() {IsUpsert = true});

            // Assert
            replaceOneResult.ShouldNotBeNull();
            replaceOneResult.MatchedCount.ShouldEqual(0);
            replaceOneResult.ModifiedCount.ShouldEqual(0);
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

            var replaceOneResult = ProjectRepository.ReplaceOne(x => x.Name == "My first project!", project);

            // Assert
            replaceOneResult.ShouldNotBeNull();
            replaceOneResult.MatchedCount.ShouldEqual(0);
            replaceOneResult.ModifiedCount.ShouldEqual(0);
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