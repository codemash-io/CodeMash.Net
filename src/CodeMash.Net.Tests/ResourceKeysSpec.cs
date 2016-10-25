using System;
using System.Linq;
using Machine.Specifications;
using System.Collections.Generic;

namespace CodeMash.Net.Tests
{
    /*
    [Tags("IntegrationTest", "FS11111")]
    [Subject(typeof (ResourceKey))]
    public class ResourceKeysSpec
    {
        protected static List<Project> Projects = Bootstrapper.InitializeProjectScope();

        // Just comment this line in case you want to see results which persist on the mongodb database
        // The same remains for each test separately
        Cleanup after = () => Bootstrapper.CleanUpProjectScope();

        [Tags("FS11000")]
        [Subject(typeof(ResourceKey))]
        public class When_get_resource_keys_by_project_category
        {
            static List<ResourceKey> ResourceKeys;
            
            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope();
            };
            
            Because of = () =>
            {
                Projects = CodeMash.GetEntities<List<Project>>(_ => true);
                
                var filter = new Dictionary<string, object> {{ "Categories.Name", "Messages" }};
                var result = CodeMash.GetEntity<Project>("Projects", Projects.First().Id, filter, new [] { "Categories.$" });
                ResourceKeys = result.Categories.SelectMany(x => x.Keys).ToList();

            };
            It should_return_result = () => ResourceKeys.ShouldNotBeNull();

            Cleanup after = () => Bootstrapper.CleanUpProjectScope();
        }

        [Tags("FS11000")]
        [Subject(typeof(ResourceKey))]
        public class When_get_resource_key_by_project_category
        {
            static ResourceKey ResourceKey;

            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope();
            };
            
            Because of = () =>
            {
                Projects = CodeMash.GetEntities<List<Project>>(_ => true);
                
                var filter = new Dictionary<string, object> {{ "Categories.Keys.Name", "Messages_1" }};
                var result = CodeMash.GetEntity<Project>("Projects", Projects.First().Id, filter, new [] { "Categories.Keys.$" });
                ResourceKey = result.Categories.SelectMany(x => x.Keys).FirstOrDefault(x => x.Name == "Messages_1");

            };
            It should_return_resource_key = () => ResourceKey.ShouldNotBeNull();

            Cleanup after = () => Bootstrapper.CleanUpProjectScope();
        }

        [Tags("FS10001")]
        [Subject(typeof(ResourceKey))]
        public class When_remove_resource_key_by_project_category
        {
            static bool IsDeleted;

            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope();
            };

            Because of = () =>
            {
                var filter = new Dictionary<string, object> {{ "Categories.Keys.Name", "Messages_3" }};
                IsDeleted  = CodeMash.DeleteEntity(_ => true, Projects.First().Id, filter);

            };
            It should_be_deleted = () => IsDeleted.ShouldBeTrue();

            Cleanup after = () => Bootstrapper.CleanUpProjectScope();
        }

        [Tags("FS10010")]
        [Subject(typeof(ResourceKey))]
        public class When_add_new_resource_key_to_project_category
        {
            static ResourceKey ResourceKey;

            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope();
            };

            Because of = () =>
            {
                var resourceKey = new ResourceKey
                {
                    Key = Guid.NewGuid().ToString(),
                    Name = "New Label",
                    Values = new List<ResourceValue>()
                };

                var upsertInformation = new UpsertInformation
                {
                    RootId = Projects.First().Id,
                    CriteriaPath = "Categories.Name",
                    CriteriaValue = "Labels",
                    InsertCollectionName = "Keys"
                };
                
                ResourceKey = CodeMash.SaveEntity<ResourceKey>(_ => true, resourceKey, upsertInformation);
              
            };

            It should_be_inserted = () => ResourceKey.ShouldNotBeNull();
            It should_have_correct_name = () => ResourceKey.Name.ShouldEqual("New Label");
            
            Cleanup after = () => Bootstrapper.CleanUpProjectScope();
        }

        [Tags("FS10010")]
        [Subject(typeof(ResourceKey))]
        public class When_update_resource_key_of_project_category
        {
            static ResourceKey ResourceKey;

            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope();
            };
            
            Because of = () =>
            {
                var resourceKey = Projects.First().Categories.First(x => x.Name == "Labels").Keys.First(x => x.Name == "Labels_1");
                
                var upsertInformation = new UpsertInformation
                {
                    RootId = Projects.First().Id,
                    CriteriaPath = "Categories.Keys.Key",
                    CriteriaValue = resourceKey.Key
                };

                resourceKey.Name = resourceKey.Name + " Updated !!!";

                
                ResourceKey = CodeMash.SaveEntity<ResourceKey>(x => _true, resourceKey, upsertInformation);
              
            };

            It should_be_inserted = () => ResourceKey.ShouldNotBeNull();
            It should_have_right_name = () => ResourceKey.Name.ShouldEqual("Labels_1 Updated !!!");

            Cleanup after = () => Bootstrapper.CleanUpProjectScope();
        }

    }
    */
}
