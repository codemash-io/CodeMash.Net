using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CodeMash.Interfaces.Data;
using CodeMash.Tests.Data;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using NUnit.Framework;

namespace CodeMash.Tests
{
    [BsonIgnoreExtraElements]
    public class ProjectProjection
    {
        public string Name { get; set; }
        public List<string> SupportedLanguages { get; set; }
    }

    [TestFixture]
    public partial class Find : TestBase
    {
        public Find()
        {
            Projects = new List<Project>();
        }

        private Project Project { get; set; }
        private Project Project2 { get; set; }
        private Project Project3 { get; set; }
        private Project Project4 { get; set; }
        private Project Project5 { get; set; }

        public List<Project> Projects { get; set; }
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

            Project3 = new Project
            {
                Name = "A",
                Categories = Defaults.DefaultResourceCategories(ids).ToList(),
                SupportedLanguages = ids
            };

            Project4 = new Project
            {
                Name = "B",
                Categories = Defaults.DefaultResourceCategories(ids).ToList(),
                SupportedLanguages = ids
            };

            Project5 = new Project
            {
                Name = "C",
                Categories = Defaults.DefaultResourceCategories(ids).ToList(),
                SupportedLanguages = ids
            };

            Projects = new List<Project>(new [] { Project3, Project4, Project5});

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
            var filterByLanguge = filterBuilder.ElemMatch(x => x.SupportedLanguages, x => x == ltLanguageId.Id);
            var nestedElementByCategoryNameFilter = filterBuilder.ElemMatch(x => x.Categories, x => x.Name == "Buttons");
            var keysFilter = Builders<ResourceCategory>.Filter.ElemMatch(x => x.Keys, x => x.Name == "Buttons_1");
            var nestedElementByCategoryKeyValueFilter = filterBuilder.ElemMatch(x => x.Categories, keysFilter);

            // TODO : bug with Find in Array
            var filter = "{ 'SupportedLanguages' : '" + ltLanguageId.Id + "'}" & nestedElementByCategoryNameFilter & nestedElementByCategoryKeyValueFilter;

            var projects = ProjectRepository.Find(filter);

            // Assert
            projects.ShouldNotBeNull();
            projects.Count.ShouldEqual(1);
        }

        [Test]
        [Category("Data")]
        public void Can_get_items_by_specified_name_filter_through_expression()
        {
            // Act
            ProjectRepository.InsertOne(Project);
            ProjectRepository.InsertOne(Project2);

            var projects = ProjectRepository.Find(x => x.Name == "My first project");

            // Assert
            projects.ShouldNotBeNull();
            projects.Count.ShouldEqual(2);
        }

        [Test]
        [Category("Data")]
        public void Can_get_items_by_specified_name_filter_through_filter_of_mongodb_driver()
        {
            // Act
            ProjectRepository.InsertOne(Project);
            ProjectRepository.InsertOne(Project2);

            var filter = Builders<Project>.Filter.Eq("Name", "My first project");
            var projects = ProjectRepository.Find(filter);
            

            // Assert
            projects.ShouldNotBeNull();
            projects.Count.ShouldEqual(2);
        }

        [Test]
        [Category("Data")]
        public void Cannot_get_items_when_filter_dont_match()
        {
            // Act
            ProjectRepository.InsertOne(Project);
            ProjectRepository.InsertOne(Project2);

            var filter = Builders<Project>.Filter.Eq("Name", "My first project11");
            var projects = ProjectRepository.Find(filter);
            

            // Assert
            projects.ShouldNotBeNull();
            projects.Count.ShouldEqual(0);
        }


        [Test]
        [Category("Data")]
        public void Can_get_data_using_just_paging()
        {
            // Arrange
            ProjectRepository.InsertMany(Projects);

            var projects = ProjectRepository.Find(x => true, null, 0, 2);
            

            // Assert
            projects.ShouldNotBeNull();
            projects.Count.ShouldEqual(2);
        }


        [Test]
        [Category("Data")]
        public void Can_get_data_using_paging_and_sorting()
        {
            // Arrange
            ProjectRepository.InsertMany(Projects);

            var sortByName = Builders<Project>.Sort.Ascending(x => x.Name);

            var projects = ProjectRepository.Find(x => true, sortByName, 0, 1);
            

            // Assert
            projects.ShouldNotBeNull();
            projects.Count.ShouldEqual(1);
            projects.First().Name.ShouldEqual("A");
        }

        [Test]
        [Category("Data")]
        public void Can_get_data_using_paging_and_sorting_descending()
        {
            // Arrange
            ProjectRepository.InsertMany(Projects);

            var sortByName = Builders<Project>.Sort.Descending(x => x.Name);

            var projects = ProjectRepository.Find(x => true, sortByName, 0, 1);
            

            // Assert
            projects.ShouldNotBeNull();
            projects.Count.ShouldEqual(1);
            projects.First().Name.ShouldEqual("C");
        }


        [Test]
        [Category("Data")]
        public void Can_get_data_using_projection()
        {
            // Arrange
            ProjectRepository.InsertMany(Projects);
            var projects = ProjectRepository.Find(x => true, x => new ProjectProjection { Name = x.Name, SupportedLanguages = x.SupportedLanguages});
            
            // Assert
            projects.ShouldNotBeNull();
            projects.Count.ShouldEqual(3);
        }


        [Test]
        [Category("Data")]
        public void Can_get_data_using_projection_sorting_paging()
        {
            // Arrange
            ProjectRepository.InsertMany(Projects);

            Expression<Func<Project, ProjectProjection>> projectionExpression =
                x => new ProjectProjection {Name = x.Name, SupportedLanguages = x.SupportedLanguages};

            var sorting = Builders<Project>.Sort.Ascending(x => x.Name);

            var projects = ProjectRepository.Find(x => true, projectionExpression, sorting, 0, 3);
            
            // Assert
            projects.ShouldNotBeNull();
            projects.Count.ShouldEqual(3);
            projects.First().Name.ShouldEqual("A");
        }


        // TODO : add tests for paging, sorting, projection

        protected override void Dispose()
        {
            base.Dispose();

            // TODO : drop collections, instead of removing one by one items. 
            ProjectRepository.DeleteMany(x => true);

            LanguagesRepository.DeleteMany(x => true);

        }
    }
}