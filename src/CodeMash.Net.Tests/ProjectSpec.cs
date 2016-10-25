using Machine.Specifications;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CodeMash.Net.Tests
{
    /// <summary>
    /// Class ProjectSpec. Main tests (CRUD) for entity project.  
    /// </summary>
    [Tags("IntegrationTest", "FS11111")]
    [Subject(typeof (Project))]
    public class ProjectSpec
    {
        protected static List<Project> Projects;

        // Just comment this line in case you want to see results which persist on the mongodb database
        // The same remains for each test separately
        Cleanup after = () => Bootstrapper.CleanUpProjectScope();

        [Tags("FS11000")]
        [Subject(typeof(Project))]
        public class When_add_new_project
        {
            static Project Project;

            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope();
            };

            Because of = () =>
            {
                var availableLanguages = Db.Find<ResourceLanguage>(_ => true);
                var availableUsers = Db.Find<User>(_ => true);

                Project = new Project
                {
                    Name = "Project1",
                    Description = "It's all about projects right ?",
                    Categories = Defaults.DefaultResourceCategories(availableLanguages.Select(x => x.Id).ToList()).ToList(),
                    SupportedLanguages = availableLanguages.Select(x => x.Id).ToList(),
                    Users = availableUsers.Select(x => x.Id).ToList()
                };

                Db.InsertOne(Project);
            };

            It should_create_the_project = () => Project.ShouldNotBeNull();
            It should_create_project_with_right_name = () => Project.Name.ShouldEqual("Project1");
            It should_create_project_with_6_categories = () => Project.Categories.Count.ShouldEqual(6);

            Cleanup after = () => Bootstrapper.CleanUpProjectScope();
        }

        [Tags("FS10010")]
        [Subject(typeof(Project))]
        public class When_edit_project_by_adding_new_language
        {
            static Project Project;
            static global::CodeMash.Net.DataContracts.UpdateResult UpdateResult;
            static ObjectId NewLanguageId = ObjectId.GenerateNewId();
            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope();
            };
            
            Because of = () =>
            {
                var projects = Db.Find(x => true, Builders<Project>.Sort.Descending(p => p.CreatedOn), null, 0, 1);
                var project = projects.First();
                UpdateResult = Db.UpdateOne(x => x.Id == project.Id, Builders<Project>.Update.AddToSet("SupportedLanguages", NewLanguageId));
                Project = Db.FindOne<Project>(x => x.Id == project.Id);
            };

            It should_return_project = () => UpdateResult.ShouldNotBeNull();
            It should_update_supported_languages = () => UpdateResult.ModifiedCount.ShouldEqual(1);
            It should_have_3_supported_languages = () => Project.SupportedLanguages.Count.ShouldEqual(2);

            Cleanup after = () => Bootstrapper.CleanUpProjectScope();
        }

        [Tags("FS10100")]
        [Subject(typeof(Project))]
        public class When_get_list_of_projects
        {
            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope();
            };                        
            It should_return_projects = () => Projects.ShouldNotBeNull();           
            Cleanup after = () => Bootstrapper.CleanUpProjectScope();
        }
        
        [Tags("FS10100")]
        [Subject(typeof(Project))]
        public class When_get_project_by_id_with_expression
        {
            static Project Project;

            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope();
            };

            Because of = () =>
            {
                var projects = Db.Find(x => true, Builders<Project>.Sort.Descending(p => p.CreatedOn), null, 0, 1);
                var project = projects.First();
                Project = Db.FindOne<Project>(x => x.Id == project.Id);
            };
            
            It should_return_project_by_id_with_expression = () => Project.ShouldNotBeNull();
            Cleanup
                
                after = () => Bootstrapper.CleanUpProjectScope();
        }

        [Tags("FS10100")]
        [Subject(typeof(Project))]
        public class When_get_project_by_id
        {
            static Project Project;

            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope();
            };

            Because of = () =>
            {
                var projects = Db.Find(x => true, Builders<Project>.Sort.Descending(p => p.CreatedOn), null, 0, 1);
                var project = projects.First();
                Project = Db.FindOneById<Project>(project.Id.ToString());
            };

            It should_return_project_by_id = () => Project.ShouldNotBeNull();
            Cleanup after = () => Bootstrapper.CleanUpProjectScope();
        }

        [Subject(typeof(Project))]
        [Tags("FS10001")]
        public class When_delete_project_by_id
        {
            static global::CodeMash.Net.DataContracts.DeleteResult deleteResult;
            
            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope();
            };

            Because of = () =>
            {
                var projects = Db.Find(x => true, Builders<Project>.Sort.Descending(p => p.CreatedOn), null, 0, 1);
                var project = projects.First();
                deleteResult = Db.DeleteOne<Project>(project.Id.ToString());
            };

            It should_delete_project_by_id = () => deleteResult.DeletedCount.ShouldEqual(1);

            Cleanup after = () => Bootstrapper.CleanUpProjectScope();
        }



        
        [Subject(typeof(Project))]
        [Tags("FS10001")]
        public class When_delete_project_by_id_using_expression_filter
        {
            static global::CodeMash.Net.DataContracts.DeleteResult deleteResult;

            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope();
            };

            Because of = () =>
            {
                var projects = Db.Find(x => true, Builders<Project>.Sort.Descending(p => p.CreatedOn), null, 0, 1);
                var project = projects.First();
                deleteResult = Db.DeleteOne<Project>(x => x.Id == project.Id);
            };

            It should_delete_project_by_id_using_expression = () => deleteResult.DeletedCount.ShouldEqual(1);

            Cleanup after = () => Bootstrapper.CleanUpProjectScope();
        }
        
        [Subject(typeof(Project))]
        [Tags("FS10001")]
        public class When_delete_project_by_id_using_filter
        {
            static global::CodeMash.Net.DataContracts.DeleteResult deleteResult;

            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope();
            };

            Because of = () =>
            {
                var projects = Db.Find(x => true, Builders<Project>.Sort.Descending(p => p.CreatedOn), null, 0, 1);
                var project = projects.First();
                deleteResult = Db.DeleteOne<Project>(Builders<Project>.Filter.Eq(x => x.Id, project.Id));
            };

            It should_delete_project_by_id_using_filter_definition = () => deleteResult.DeletedCount.ShouldEqual(1);

            Cleanup after = () => Bootstrapper.CleanUpProjectScope();
        }
        

        [Tags("FS11000")]
        [Subject(typeof(Project))]
        public class When_get_projects_by_user_id
        {
            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope();
            };

            Because of = () =>
            {
                var users = Db.Find<User>(_ => true);
                var filter = Builders<Project>.Filter.AnyEq(x => x.Users, users.First().Id);
                Projects = Db.Find(filter);
            };

            It should_return_project_by_user_id = () => Projects.ShouldNotBeNull();
            It should_return_one_project_by_user_id = () => Projects.Count().ShouldEqual(2);

            Cleanup after = () => Bootstrapper.CleanUpProjectScope();
        }
        
        [Tags("FS10100")]
        [Subject(typeof(Project))]
        public class When_get_list_of_projects_by_page
        {
            static Project Project;
            protected static List<Project> ProjectsWithPage;

            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope();
                var availableLanguages = Db.Find<ResourceLanguage>(_ => true);
                var availableUsers = Db.Find<User>(_ => true);

                Project = new Project
                {
                    Name = "Project2",
                    Description = "It's all about projects right ?",
                    Categories = Defaults.DefaultResourceCategories(availableLanguages.Select(x => x.Id).ToList()).ToList(),
                    SupportedLanguages = availableLanguages.Select(x => x.Id).ToList(),
                    Users = availableUsers.Select(x => x.Id).ToList()
                };
                Db.InsertOne(Project);
            };

            Because of = () =>
            {
                Projects = Db.Find<Project>(_ => true);
                ProjectsWithPage = Db.Find<Project>(_ => true, null, null, 0, 1);
            };

            It should_return_projects = () => Projects.ShouldNotBeNull();
            It should_return_projects_with_page = () => ProjectsWithPage.ShouldNotBeNull();
            It should_return_length_of_3 = () => Projects.Count().ShouldEqual(3);
            It should_return_length_of_1 = () => ProjectsWithPage.Count().ShouldEqual(1);

            Cleanup after = () => Bootstrapper.CleanUpProjectScope();
        }
        
        [Tags("FS10100")]
        [Subject(typeof(Project))]
        public class When_get_list_of_projects_by_page_in_ascending_order
        {
            static Project Project;
            protected static List<Project> ProjectsWithPageAscending;
            protected static List<Project> ProjectsWithPageDescending;

            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope();
                var availableLanguages = Db.Find<ResourceLanguage>(_ => true);
                var availableUsers = Db.Find<User>(_ => true);

                Project = new Project
                {
                    Name = "Project2",
                    Description = "It's all about projects right ?",
                    Categories = Defaults.DefaultResourceCategories(availableLanguages.Select(x => x.Id).ToList()).ToList(),
                    SupportedLanguages = availableLanguages.Select(x => x.Id).ToList(),
                    Users = availableUsers.Select(x => x.Id).ToList()
                };
                Db.InsertOne(Project);
            };

            Because of = () =>
            {
                Projects = Db.Find<Project>(_ => true);
                ProjectsWithPageAscending = Db.Find(_ => true, Builders<Project>.Sort.Ascending(x => x.Name), null, 0,1);
                ProjectsWithPageDescending = Db.Find(_ => true, Builders<Project>.Sort.Descending(x => x.Name), null, 0, 1);
            };

            It should_return_projects = () => Projects.ShouldNotBeNull();
            It should_return_projects_with_ascending_order = () => ProjectsWithPageAscending.ShouldNotBeNull();
            It should_return_projects_with_descending_order = () => ProjectsWithPageDescending.ShouldNotBeNull();
            It should_return_length_of_3 = () => Projects.Count().ShouldEqual(3);
            It should_return_length_of_1 = () => ProjectsWithPageAscending.Count().ShouldEqual(1);
            It should_return_length_of = () => ProjectsWithPageDescending.Count().ShouldEqual(1);

            It should_return_project_with_right_name_ascending =
                () => ProjectsWithPageAscending.FirstOrDefault().Name.ShouldEqual("Project2");

            It should_return_project_with_right_name_descending =
                () => ProjectsWithPageDescending.FirstOrDefault().Name.ShouldEqual("TestProject2");

            Cleanup after = () => Bootstrapper.CleanUpProjectScope();
        }
        
    }
}
