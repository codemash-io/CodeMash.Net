using System;
using System.Linq;
using Machine.Specifications;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using CodeMash.Net.Tests.DataContracts;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CodeMash.Net.Tests
{
    /// <summary>
    /// Class ResourceCategoriesSpec. Project entity has children - ResourceCategories.
    /// </summary>
    [Tags("IntegrationTest", "FS11111")]
    [Subject(typeof (ResourceCategory))]
    public class ResourceCategoriesSpec
    {
        protected static List<Project> Projects;

        // Just comment this line in case you want to see results which persist on the mongodb database
        // The same remains for each test separately
        Cleanup after = () => Bootstrapper.CleanUpProjectScope();

        /*[Subject(typeof(ResourceCategory))]
        public class When_do_projection_per_projects_and_ask_get_resource_categories
        {
            static List<ProjectProjectionDataContract> ResourceCategories;

            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope();
            };

            Because of = () =>
            {

                Expression<Func<Project, List<ResourceCategory>>> expr = p => p.Categories;
                Builders<Project>.Projection.Expression(x => x.Categories);
                
                ResourceCategories = Db.Find<Project, ProjectProjectionDataContract>(x => true, null, x => new ProjectProjectionDataContract { Categories = x.Categories});
            };

            It should_return_resource_category = () => ResourceCategories.ShouldNotBeNull();
            It should_return_right_amount_of_data = () => ResourceCategories.Count.ShouldEqual(2);
            It should_return_right_amount_of_categories = () => ResourceCategories[0].Categories.Count.ShouldEqual(6);


            Cleanup after = () => Bootstrapper.CleanUpProjectScope();
        }*/

        [Subject(typeof(ResourceCategory))]
        public class When_ask_get_aggregated_version
        {
            static List<ProjectAggregatedDataContract> ProjectsAggregated;

            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope();
            };

            // db.Projects.aggregate([
            //    { "$match" : { "Name" : "TestProject" }}, 
            //    { $unwind : "$Categories"}, 
            //    { $group : { _id : { Name : "$Name"},   "CategoriesCount" : { "$sum" : 1 } } },
            //    { $project : { Name : "$_id.Name", "CategoriesCount" : 1, _id : 0}}
            // ]);

            Because of = () =>
            {
                var match = new BsonDocument
                {
                    {
                        "$match",
                        new BsonDocument
                        {
                            {"Name", "TestProject"}
                        }
                    }
                };


                var unwind = new BsonDocument
                {
                    {
                        "$unwind",
                        "$Categories"
                    }
                };

                var group = new BsonDocument
                {
                    {
                        "$group",
                        new BsonDocument
                        {
                            { "_id", new BsonDocument {{ "Name","$Name" }}},
                            { "CategoriesCount", new BsonDocument
                                {
                                    {
                                        "$sum", 1
                                    }
                                }
                            }
                        }
                    }
                };

                var project = new BsonDocument
                {
                    {
                        "$project",
                        new BsonDocument
                        {
                            { "Name", "$_id.Name"},
                            { "CategoriesCount", 1 },
                            { "_id", 0 }
                        }
                    }
                };

                var pipeline = new[] { match, unwind, group, project};

                ProjectsAggregated = Db.Aggregate<Project, ProjectAggregatedDataContract>(pipeline, null);
            };

            It should_return_aggregated_data = () => ProjectsAggregated.ShouldNotBeNull();
            It should_return_aggregated_data_only_one_record = () => ProjectsAggregated.Count.ShouldEqual(1);
            It should_return_aggregated_data_only_one_record_with6_categories = () => ProjectsAggregated.First().CategoriesCount.ShouldEqual(6);


            Cleanup after = () => Bootstrapper.CleanUpProjectScope();
        }


        // Just comment this line in case you want to see results which persist on the mongodb database
        // The same remains for each test separately
        //Cleanup after = () => Bootstrapper.CleanUpProjectScope();

        /*[Tags("FS10010")]
        [Subject(typeof(ResourceCategory))]
        public class When_update_resource_category_using_category_identifier
        {
            static DataContracts.Core.UpdateResult UpdateResult;

            Establish context = async() =>
            {
                Projects = Bootstrapper.InitializeProjectScope();
            };

            Because of = async () =>
            {
                var key = Projects.First().Categories.FirstOrDefault(x => x.Name == "Error Messages").Key;

                var resourceCategory = new ResourceCategory
                {
                    Key = key,
                    Name = "Error Messages -- Updated !!!",
                    Keys = new List<ResourceKey>
                    {
                        new ResourceKey { Key = "Key1", Values = new List<ResourceValue>()},
                        new ResourceKey { Key = "Key2", Values = new List<ResourceValue>()},
                        new ResourceKey { Key = "Key3", Values = new List<ResourceValue>()},
                        new ResourceKey { Key = "Key4", Values = new List<ResourceValue>()}
                    }
                };


                UpdateResult = Db.UpdateOne(_ => true, Builders<Project>.Update.Set(x => x.Categories.FirstOrDefault(y => y.Name == "Error Messages"), resourceCategory));

            };

            It should_return_resource_category = () => UpdateResult.ShouldNotBeNull();
            It should_return_updated_name = () => UpdateResult.ModifiedCount.ShouldEqual(1);
            

            Cleanup after = async () => Bootstrapper.CleanUpProjectScope();
        }*/

        /*
        [Tags("FS10010")]
        [Subject(typeof(ResourceCategory))]
        public class When_add_new_resource_category_to_the_project
        {
            static ResourceCategory ResourceCategory;

            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope();
            };

            Because of = () =>
            {
                var resourceCategory = new ResourceCategory
                {
                    Key = Guid.NewGuid().ToString(),
                    Name = "It's a new Category boy",
                    Keys = new List<ResourceKey>
                    {
                        new ResourceKey { Key = "Key1", Values = new List<ResourceValue>()},
                        new ResourceKey { Key = "Key2", Values = new List<ResourceValue>()},
                        new ResourceKey { Key = "Key3", Values = new List<ResourceValue>()},
                        new ResourceKey { Key = "Key4", Values = new List<ResourceValue>()}
                    }
                };

                var upsertInformation = new UpsertInformation
                {
                    RootId = Projects.First().Id,
                    InsertCollectionName = "Categories"
                };

                ResourceCategory = CodeMash.SaveEntity<ResourceCategory>(resourceCategory, upsertInformation);

            };

            It should_return_resource_category = () => ResourceCategory.ShouldNotBeNull();
            It should_return_new_cat_name = () => ResourceCategory.Name.ShouldEqual("It's a new Category boy");
            It should_have_4_resource_keys = () => ResourceCategory.Keys.Count().ShouldEqual(4);

            //Cleanup after = () => Bootstrapper.CleanUpProjectScope();
        }
        */

        /*
    [Tags("FS10001")]
    [Subject(typeof (ResourceCategory))]
    public class When_remove_resource_category_by_key
    {
        static bool IsDeleted;

        Establish context = () =>
        {
            Projects = Bootstrapper.InitializeProjectScope();
        };

        private Because of = () =>
        {
            var deleteFilter = new Dictionary<string, object> {{"Categories.Name", "Buttons"}};
            IsDeleted = CodeMash.DeleteEntity(Projects.First().Id, deleteFilter);

        };

        It should_be_deleted = () => IsDeleted.ShouldBeTrue();

        Cleanup after = () => Bootstrapper.CleanUpProjectScope();
    }

    [Tags("FS11000")]
    [Subject(typeof(ResourceCategory))]
    public class When_get_all_resource_categories_by_user_id
    {
        static List<ResourceCategory> ResourceCategories;
        static List<User> Users;

        Establish context = () =>
        {
            Projects = Bootstrapper.InitializeProjectScope();
            Users = CodeMash.GetEntities<List<User>>();
        };

        Because of = () =>
        {
            var filter = new Dictionary<string, object> { { "Users", Users.Last().Id } };
            var result = CodeMash.GetEntities<List<Project>>("Projects", filter, new[] { "Categories" });
            ResourceCategories = result.SelectMany(x => x.Categories).ToList();

        };
        It should_return_result = () => ResourceCategories.ShouldNotBeNull();
        It should_be_6_categories = () => ResourceCategories.Count.ShouldEqual(6);

        Cleanup after = () => Bootstrapper.CleanUpProjectScope();
    }

    [Tags("FS11000")]
    [Subject(typeof(ResourceCategory))]
    public class When_get_resource_category_from_specified_project
    {
        static ResourceCategory ResourceCategory;

        Establish context = () =>
        {
            Projects = Bootstrapper.InitializeProjectScope();
        };

        Because of = () =>
        {
            var projectId = Projects.First().Id;

            var filter = new Dictionary<string, object> { { "Categories.Name", "Messages" }};
            var result = CodeMash.GetEntity<Project>(projectId, filter, new [] {"Categories.$" });
            ResourceCategory = result.Categories.FirstOrDefault();

        };
        It should_return_resource_category = () => ResourceCategory.ShouldNotBeNull();
        It should_return_right_resource_category = () => ResourceCategory.Name.ShouldEqual("Messages");

        Cleanup after = () => Bootstrapper.CleanUpProjectScope();
    }
    */
    }
}
