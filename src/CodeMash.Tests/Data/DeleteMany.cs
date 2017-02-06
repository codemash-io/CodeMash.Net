using System.Linq;
using CodeMash.Interfaces.Data;
using CodeMash.Tests.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using NUnit.Framework;

namespace CodeMash.Tests
{
    [TestFixture]
    public partial class DeleteMany : TestBase
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

            var ltLangage = new ResourceLanguage { Name = "Lithuanian", NativeName = "Lietuviu", CultureCode = "lt-LT" };
            var usLangage = new ResourceLanguage { Name = "English", NativeName = "English", CultureCode = "en-US" };
            var gbLangage = new ResourceLanguage { Name = "UK", NativeName = "Great Britain", CultureCode = "en-GB" };

            LanguagesRepository.InsertMany(new[] { gbLangage, usLangage, ltLangage });

            var ids = LanguagesRepository.Find(x => x.CultureCode == "en-US" || x.CultureCode == "en-GB").Select(x => x.Id).ToList();

            // Arrange
            Project = new Project
            {
                Name = "My first project",
                Categories = Defaults.DefaultResourceCategories(ids).ToList(),
                SupportedLanguages = ids
            };

            ids = LanguagesRepository.Find(x => true).Select(x => x.Id).ToList();

            Project2 = new Project
            {
                Name = "My first project",
                Categories = Defaults.DefaultResourceCategories(ids).ToList(),
                SupportedLanguages = ids
            };


        }

        [Test]
        [Category("Data")]
        public void Can_delete_item_from_db_when_expression_is_provided()
        {
            // Act
            ProjectRepository.InsertOne(Project);
            ProjectRepository.InsertOne(Project2);
            var deleteResult = ProjectRepository.DeleteMany(x => x.Name == "My first project");

            // Assert
            deleteResult.ShouldNotBeNull();
            deleteResult.DeletedCount.ShouldEqual(2);
        }


        [Test]
        [Category("Data")]
        public void Can_delete_item_from_db_when_filter_definition_is_provided()
        {
            // Act
            ProjectRepository.InsertOne(Project);
            ProjectRepository.InsertOne(Project2);
            var deleteResult = ProjectRepository.DeleteMany(Builders<Project>.Filter.Eq(x => x.Name, "My first project"));

            // Assert
            deleteResult.ShouldNotBeNull();
            deleteResult.DeletedCount.ShouldEqual(2);
        }

        [Test]
        [Category("Data")]
        public void Cannot_delete_from_db_when_filter_didnt_match()
        {
            // Act
            ProjectRepository.InsertOne(Project);
            ProjectRepository.InsertOne(Project2);
            var deleteResult = ProjectRepository.DeleteMany(Builders<Project>.Filter.Eq(x => x.Name, "My first project!"));

            // Assert
            deleteResult.ShouldNotBeNull();
            deleteResult.DeletedCount.ShouldEqual(0);
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