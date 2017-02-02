using System.Linq;
using CodeMash.Data;
using CodeMash.Interfaces.Data;
using CodeMash.Tests.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using NUnit.Framework;

namespace CodeMash.Tests
{
    [TestFixture]
    public class GetOne : TestBase
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

            LanguagesRepository.InsertMany(new [] { gbLangage, usLangage, ltLangage});

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
        public void Can_get_item_by_specified_name_filter_through_expression()
        {
            // Act
            ProjectRepository.InsertOne(Project);
            
            var project = ProjectRepository.FindOne(x => x.Name == "My first project");
            
            // Assert
            project.ShouldNotBeNull();
            project.Id.ShouldEqual(Project.Id);
        }

        [Test]
        [Category("Data")]
        public void Can_get_item_by_specified_name_filter_through_filter_of_mongodb_driver()
        {
            // Act
            ProjectRepository.InsertOne(Project);

            var filter1 = Builders<Project>.Filter.Eq("Name", "My first project");
            var projectResult1 = ProjectRepository.FindOne(filter1);

            var filter2 = Builders<Project>.Filter.Eq(x => x.Name, "My first project");
            var projectResult2 = ProjectRepository.FindOne(filter2);

            // Assert
            projectResult1.ShouldNotBeNull();
            projectResult1.Id.ShouldEqual(Project.Id);
            projectResult2.ShouldNotBeNull();
            projectResult2.Id.ShouldEqual(Project.Id);
        }


        [Test]
        [Category("Data")]
        public void Can_get_item_by_nested_property()
        {
            // Act
            ProjectRepository.InsertOne(Project);
            ProjectRepository.InsertOne(Project2);

            
            var filterBuilder = Builders<Project>.Filter;
            
            var ltLanguageId = LanguagesRepository.FindOne(x => x.CultureCode == "lt-LT");
            var filterByLanguge = filterBuilder.ElemMatch(x => x.SupportedLanguages, ltLanguageId.Id);
            var nestedElementByCategoryNameFilter = filterBuilder.ElemMatch(x => x.Categories, x => x.Name == "Buttons");
            var keysFilter = Builders<ResourceCategory>.Filter.ElemMatch(x => x.Keys, x => x.Name == "Buttons_1");
            var nestedElementByCategoryKeyValueFilter = filterBuilder.ElemMatch(x => x.Categories, keysFilter);

            // TODO : bug with Find in Array
            var filter = "{ 'SupportedLanguages' : '" + ltLanguageId.Id + "'}" &  nestedElementByCategoryNameFilter & nestedElementByCategoryKeyValueFilter;
            
            var projects = ProjectRepository.Find(filter);

            // Assert
            projects.ShouldNotBeNull();
            projects.Count.ShouldEqual(1);
        }


        [Test]
        [Category("Data")]
        public void Cannot_get_item()
        {
            // Act
            ProjectRepository.InsertOne(Project);
            ProjectRepository.InsertOne(Project2);

            var project = ProjectRepository.FindOne(Builders<Project>.Filter.Eq("Categories.Name", "Buttons7"));

            // Assert
            project.ShouldBeNull();
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