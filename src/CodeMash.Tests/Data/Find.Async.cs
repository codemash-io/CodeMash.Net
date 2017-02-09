using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using CodeMash.Interfaces.Data;
using CodeMash.Tests.Data;
using MongoDB.Driver;
using NUnit.Framework;

namespace CodeMash.Tests
{
    [TestFixture]
    public partial class Find
    {

        [Test]
        [Category("Data")]
        public async Task Can_get_item_by_nested_property_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            await ProjectRepository.InsertOneAsync(Project2);


            var filterBuilder = Builders<Project>.Filter;

            var ltLanguageId = await LanguagesRepository.FindOneAsync(x => x.CultureCode == "lt-LT");
            var filterByLanguge = filterBuilder.ElemMatch(x => x.SupportedLanguages, x => x == ltLanguageId.Id);
            var nestedElementByCategoryNameFilter = filterBuilder.ElemMatch(x => x.Categories, x => x.Name == "Buttons");
            var keysFilter = Builders<ResourceCategory>.Filter.ElemMatch(x => x.Keys, x => x.Name == "Buttons_1");
            var nestedElementByCategoryKeyValueFilter = filterBuilder.ElemMatch(x => x.Categories, keysFilter);

            // TODO : bug with Find in Array
            var filter = "{ 'SupportedLanguages' : '" + ltLanguageId.Id + "'}" & nestedElementByCategoryNameFilter & nestedElementByCategoryKeyValueFilter;

            var projects = await ProjectRepository.FindAsync(filter);

            // Assert
            projects.ShouldNotBeNull();
            projects.Count.ShouldEqual(1);
        }

        [Test]
        [Category("Data")]
        public async Task Can_get_items_by_specified_name_filter_through_expression_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            await ProjectRepository.InsertOneAsync(Project2);

            var projects = ProjectRepository.Find(x => x.Name == "My first project");

            // Assert
            projects.ShouldNotBeNull();
            projects.Count.ShouldEqual(2);
        }

        [Test]
        [Category("Data")]
        public async Task Can_get_items_by_specified_name_filter_through_filter_of_mongodb_driver_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            await ProjectRepository.InsertOneAsync(Project2);

            var filter = Builders<Project>.Filter.Eq("Name", "My first project");
            var projects = ProjectRepository.Find(filter);
            

            // Assert
            projects.ShouldNotBeNull();
            projects.Count.ShouldEqual(2);
        }

        [Test]
        [Category("Data")]
        public async Task Cannot_get_items_when_filter_dont_match_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            await ProjectRepository.InsertOneAsync(Project2);

            var filter = Builders<Project>.Filter.Eq("Name", "My first project11");
            var projects = ProjectRepository.Find(filter);
            

            // Assert
            projects.ShouldNotBeNull();
            projects.Count.ShouldEqual(0);
        }


        [Test]
        [Category("Data")]
        public async Task Can_get_data_using_just_paging_async()
        {
            // Arrange
            await ProjectRepository.InsertManyAsync(Projects);

            var findOptions = new FindOptions<Project, ProjectProjection>
            {
                Skip = 0,
                Limit = 2
            };

            var projects = await ProjectRepository.FindAsync(x => true, findOptions);


            // Assert
            projects.ShouldNotBeNull();
            projects.Count.ShouldEqual(2);
        }


        [Test]
        [Category("Data")]
        public async Task Can_get_data_using_paging_and_sorting_async()
        {
            // Arrange
            await ProjectRepository.InsertManyAsync(Projects);

            var findOptions = new FindOptions<Project, ProjectProjection>
            {
                Skip = 0,
                Limit = 1,
                Sort = Builders<Project>.Sort.Ascending(x => x.Name)
            };
            

            var projects = await ProjectRepository.FindAsync(x => true, findOptions);


            // Assert
            projects.ShouldNotBeNull();
            projects.Count.ShouldEqual(1);
            projects.First().Name.ShouldEqual("A");
        }

        [Test]
        [Category("Data")]
        public async Task Can_get_data_using_paging_and_sorting_descending_async()
        {
            // Arrange
            await ProjectRepository.InsertManyAsync(Projects);

            var findOptions = new FindOptions<Project, ProjectProjection>
            {
                Skip = 0,
                Limit = 1,
                Sort = Builders<Project>.Sort.Descending(x => x.Name)
            };
            

            var projects = await ProjectRepository.FindAsync(x => true, findOptions);


            // Assert
            projects.ShouldNotBeNull();
            projects.Count.ShouldEqual(1);
            projects.First().Name.ShouldEqual("C");
        }


        [Test]
        [Category("Data")]
        public async Task Can_get_data_using_projection_async()
        {
            // Arrange
            await ProjectRepository.InsertManyAsync(Projects);

            Expression<Func<Project,ProjectProjection>> projectionExpression = x => new ProjectProjection {Name = x.Name, SupportedLanguages = x.SupportedLanguages};


            var findOptions = new FindOptions<Project, ProjectProjection>
            {
                Projection = new FindExpressionProjectionDefinition<Project, ProjectProjection>(projectionExpression)
            };

            var projects = await ProjectRepository.FindAsync(x => true, findOptions);

            // Assert
            projects.ShouldNotBeNull();
            projects.Count.ShouldEqual(3);
        }


        [Test]
        [Category("Data")]
        public async Task Can_get_data_using_projection_sorting_paging_async()
        {
            // Arrange
            await ProjectRepository.InsertManyAsync(Projects);

            Expression<Func<Project, ProjectProjection>> projectionExpression =
                x => new ProjectProjection { Name = x.Name, SupportedLanguages = x.SupportedLanguages };

            var findOptions = new FindOptions<Project, ProjectProjection>
            {
                Projection = new FindExpressionProjectionDefinition<Project, ProjectProjection>(projectionExpression),
                Sort = Builders<Project>.Sort.Ascending(x => x.Name)
            };

            var projects = await ProjectRepository.FindAsync(x => true, findOptions);

            // Assert
            projects.ShouldNotBeNull();
            projects.Count.ShouldEqual(3);
            projects.First().Name.ShouldEqual("A");
        }



    }
}