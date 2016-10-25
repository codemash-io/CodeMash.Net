using System.Linq;
using Machine.Specifications;
using System.Collections.Generic;

namespace CodeMash.Net.Tests
{
    /*
    [Tags("IntegrationTest", "FS11111")]
    [Subject(typeof (ResourceValue))]
    public class ResourceValuesSpec
    {
        protected static List<Project> Projects;

        // Just comment this line in case you want to see results which persist on the mongodb database
        // The same remains for each test separately
        Cleanup after = () => Bootstrapper.CleanUpProjectScope();

        [Tags("FS10100")]
        [Subject(typeof(ResourceValue))]
        public class When_get_resource_values_by_project_id_and_language_id
        {
            static List<ResourceValue> ResourceValues;
            static List<ResourceLanguage> Languages;

            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope();
                Languages = CodeMash.GetEntities<List<ResourceLanguage>>("Languages");
            };

            Because of = () =>
            {
                var languageId = Languages.First().Id;

                var filter = new Dictionary<string, object> { { "Categories.Keys.Values.ResourceLanguageId", languageId } };
                var result = CodeMash.GetEntity<Project>(_ => true, Projects.First().Id, filter);
                
                ResourceValues = result.Categories
                    .SelectMany(x => x.Keys)
                    .SelectMany(x => x.Values)
                    .Where(x => x.ResourceLanguageId == languageId)
                    .ToList();

            };
            It should_return_resource_values = () => ResourceValues.ShouldNotBeNull();
            It should_return_18_translations = () => ResourceValues.Count().ShouldEqual(18);
            
            Cleanup after = () => Bootstrapper.CleanUpProjectScope();
        }

        [Tags("FS10100")]
        [Subject(typeof(ResourceValue))]
        public class When_get_resource_values_by_project_id
        {
            static List<ResourceValue> ResourceValues;

            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope();
            };

            Because of = () =>
            {
                var result = CodeMash.GetEntity<Project>(_ => true, Projects.First().Id, null, new[] { "Categories.Keys.Values" });
                ResourceValues = result.Categories.SelectMany(x => x.Keys).SelectMany(x => x.Values).ToList();

            };
            It should_return_resource_values = () => ResourceValues.ShouldNotBeNull();
            It should_return_36_translations = () => ResourceValues.Count().ShouldEqual(36);

            Cleanup after = () => Bootstrapper.CleanUpProjectScope();
        }

        [Tags("FS10100")]
        [Subject(typeof(ResourceValue))]
        public class When_get_resource_values_by_project_id_language_id_and_by_resource_key
        {
            static List<ResourceValue> ResourceValues;
            static List<ResourceLanguage> Languages;

            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope();
                Languages = CodeMash.GetEntities<List<ResourceLanguage>>("Languages");
            };

            Because of = () =>
            {
                var languageId = Languages.First().Id;
                
                var filter = new Dictionary<string, object>
                {
                    { "Categories.Keys.Name", "Messages_1"},    
                    { "Categories.Keys.Values.ResourceLanguageId", languageId }
                    
                };

                // TODO : have more than one filter 
                var result = CodeMash.GetEntity<Project>(_ => true, Projects.First().Id, filter);
                ResourceValues = result.Categories
                    .SelectMany(x => x.Keys)
                    .Where(x => x.Name == "Messages_1")
                    .SelectMany(x => x.Values)
                    .Where(x => x.ResourceLanguageId == languageId)
                    .ToList();

            };
            It should_return_resource_values = () => ResourceValues.ShouldNotBeNull();
            It should_return_one_translations = () => ResourceValues.Count().ShouldEqual(1);

            Cleanup after = () => Bootstrapper.CleanUpProjectScope();
        }

        [Tags("FS10010")]
        [Subject(typeof(ResourceValue))]
        public class D_add_new_translation_for_resource_key
        {
            static ResourceValue ResourceValue;
            static List<ResourceLanguage> Languages;

            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope();
                Languages = CodeMash.GetEntities<List<ResourceLanguage>>("Languages");
            };

            Because of = () =>
            {
                var languageId = Languages.First().Id;

                var resourceValue = new ResourceValue
                {
                    ResourceLanguageId = languageId,
                    Value = "New Translation"
                };
                
                var upsertInformation = new UpsertInformation
                {
                    RootId = Projects.First().Id,
                    CriteriaPath = "Categories.Keys.Name",
                    CriteriaValue = "Labels_1",
                    InsertCollectionName = "Values"
                };

                ResourceValue = CodeMash.SaveEntity<ResourceValue>(_ => true, resourceValue, upsertInformation);

            };

            It should_be_inserted = () => ResourceValue.ShouldNotBeNull();
            It should_be_right_name = () => ResourceValue.Value.ShouldEqual("New Translation");

            Cleanup after = () => Bootstrapper.CleanUpProjectScope();
        }

        [Tags("FS10010")]
        [Subject(typeof(ResourceValue))]
        public class When_update_translation_by_project_id_resource_key
        {
            static ResourceValue ResourceValue;
            static List<ResourceLanguage> Languages;

            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope();
                Languages = CodeMash.GetEntities<List<ResourceLanguage>>("Languages");
            };

            Because of = () =>
            {
                var languageId = Languages.First().Id;

                var resourceValue = new ResourceValue
                {
                    ResourceLanguageId = languageId,
                    Value = "Updated Translation" 
                };

                var upsertInformation = new UpsertInformation
                {
                    RootId = Projects.First().Id,
                    CriteriaPath = "Categories.Keys.Values.ResourceLanguageId",
                    CriteriaValue = languageId
                };

                ResourceValue = CodeMash.SaveEntity<ResourceValue>(_ => true, resourceValue, upsertInformation);

            };

            It should_be_updated = () => ResourceValue.ShouldNotBeNull();
            It should_be_right_name = () => ResourceValue.Value.ShouldEqual("Updated Translation");

            Cleanup after = () => Bootstrapper.CleanUpProjectScope();
        }

    }
    */
}
