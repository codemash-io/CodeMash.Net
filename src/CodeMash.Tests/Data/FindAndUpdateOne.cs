using System.Linq;
using CodeMash.Interfaces.Data;
using CodeMash.Tests.Data;
using MongoDB.Driver;
using NUnit.Framework;

namespace CodeMash.Tests
{
    [TestFixture]
    public partial class FindAndUpdateOne : TestBase
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

            Project2 = new Project
            {
                Name = "My first project",
                Categories = Defaults.DefaultResourceCategories(ids).ToList(),
                SupportedLanguages = ids
            };
        }


        [Test]
        [Category("Data")]
        public void Update_nested_element_into_item()
        {
            // Act
            ProjectRepository.InsertOne(Project);
            ProjectRepository.InsertOne(Project2);

            var ltLanguageId = LanguagesRepository.FindOne(x => x.CultureCode == "lt-LT");

            var updateResult = ProjectRepository.FindOneAndUpdate(x => x.Name == "My first project", Builders<Project>.Update.AddToSet(x => x.SupportedLanguages, ltLanguageId.Id), new FindOneAndUpdateOptions<Project>());
            
            // Assert
            updateResult.SupportedLanguages.ShouldEqual(2);
            updateResult.Name.ShouldEqual("My first project");
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