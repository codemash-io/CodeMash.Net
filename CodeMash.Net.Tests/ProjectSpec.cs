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
        Cleanup after = () => Bootstrapper.CleanUpProjectScope().GetAwaiter().GetResult();

        [Tags("FS11000")]
        [Subject(typeof(Project))]
        public class When_add_new_project
        {
            static Project Project;

            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope().Result;
            };

            Because of = () =>
            {
                var availableLanguages = CodeMash.FindAsync<ResourceLanguage>("Languages", _ => true).Result;
                var availableUsers = CodeMash.FindAsync<User>("Users", _ => true).Result;

                var project = new Project
                {
                    Name = "Project1",
                    Description = "It's all about projects right ?",
                    Categories = Defaults.DefaultResourceCategories(availableLanguages.Select(x => x.Id).ToList()).ToList(),
                    SupportedLanguages = availableLanguages.Select(x => x.Id).ToList(),
                    Users = availableUsers.Select(x => x.Id).ToList()
                };

                Project = CodeMash.InsertOneAsync("Projects", project).Result;
            };

            It should_create_the_project = () => Project.ShouldNotBeNull();
            It should_create_project_with_right_name = () => Project.Name.ShouldEqual("Project1");
            It should_create_project_with_6_categories = () => Project.Categories.Count.ShouldEqual(6);

            Cleanup after = () => Bootstrapper.CleanUpProjectScope().GetAwaiter().GetResult();
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
                Projects = Bootstrapper.InitializeProjectScope().Result;
            };
            
            Because of = () =>
            {
                var projects = CodeMash.FindAsync("Projects", x => true, Builders<Project>.Sort.Descending(p => p.CreatedOn), null, 0, 1).Result;
                var project = projects.First();
                UpdateResult = CodeMash.UpdateOneAsync("Projects", x => x.Id == project.Id, Builders<Project>.Update.AddToSet("SupportedLanguages", NewLanguageId)).Result;
                Project = CodeMash.FindOneAsync<Project>("Projects", x => x.Id == project.Id).Result;
            };

            It should_return_project = () => UpdateResult.ShouldNotBeNull();
            It should_update_supported_languages = () => UpdateResult.ModifiedCount.ShouldEqual(1);
            It should_have_3_supported_languages = () => Project.SupportedLanguages.Count.ShouldEqual(2);

            Cleanup after = () => Bootstrapper.CleanUpProjectScope().GetAwaiter().GetResult();
        }

        [Tags("FS10100")]
        [Subject(typeof(Project))]
        public class When_get_list_of_projects
        {
            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope().Result;
            };                        
            It should_return_projects = () => Projects.ShouldNotBeNull();           
            Cleanup after = () => Bootstrapper.CleanUpProjectScope().GetAwaiter().GetResult();
        }
        
        [Tags("FS10100")]
        [Subject(typeof(Project))]
        public class When_get_project_by_id_with_expression
        {
            static Project Project;

            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope().Result;
            };

            Because of = () =>
            {
                var projects = CodeMash.FindAsync("Projects", x => true, Builders<Project>.Sort.Descending(p => p.CreatedOn), null, 0, 1).Result;
                var project = projects.First();
                Project = CodeMash.FindOneAsync<Project>("Projects", x => x.Id == project.Id).Result;
            };
            
            It should_return_project_by_id_with_expression = () => Project.ShouldNotBeNull();
            Cleanup
                
                after = () => Bootstrapper.CleanUpProjectScope().GetAwaiter().GetResult();
        }

        [Tags("FS10100")]
        [Subject(typeof(Project))]
        public class When_get_project_by_id
        {
            static Project Project;

            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope().Result;
            };

            Because of = () =>
            {
                var projects = CodeMash.FindAsync("Projects", x => true, Builders<Project>.Sort.Descending(p => p.CreatedOn), null, 0, 1).Result;
                var project = projects.First();
                Project = CodeMash.FindOneByIdAsync<Project>("Projects", project.Id.ToString()).Result;
            };

            It should_return_project_by_id = () => Project.ShouldNotBeNull();
            Cleanup after = () => Bootstrapper.CleanUpProjectScope().GetAwaiter().GetResult();
        }

        [Subject(typeof(Project))]
        [Tags("FS10001")]
        public class When_delete_project_by_id
        {
            static global::CodeMash.Net.DataContracts.DeleteResult deleteResult;
            
            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope().Result;
            };

            Because of = () =>
            {
                var projects = CodeMash.FindAsync("Projects", x => true, Builders<Project>.Sort.Descending(p => p.CreatedOn), null, 0, 1).Result;
                var project = projects.First();
                deleteResult = CodeMash.DeleteOneAsync<Project>("Projects", project.Id.ToString()).Result;
            };

            It should_delete_project_by_id = () => deleteResult.DeletedCount.ShouldEqual(1);

            Cleanup after = () => Bootstrapper.CleanUpProjectScope().GetAwaiter().GetResult();
        }



        
        [Subject(typeof(Project))]
        [Tags("FS10001")]
        public class When_delete_project_by_id_using_expression_filter
        {
            static global::CodeMash.Net.DataContracts.DeleteResult deleteResult;

            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope().Result;
            };

            Because of = () =>
            {
                var projects = CodeMash.FindAsync("Projects", x => true, Builders<Project>.Sort.Descending(p => p.CreatedOn), null, 0, 1).Result;
                var project = projects.First();
                deleteResult = CodeMash.DeleteOneAsync<Project>("Projects", x => x.Id == project.Id).Result;
            };

            It should_delete_project_by_id_using_expression = () => deleteResult.DeletedCount.ShouldEqual(1);

            Cleanup after = () => Bootstrapper.CleanUpProjectScope().GetAwaiter().GetResult();
        }
        
        [Subject(typeof(Project))]
        [Tags("FS10001")]
        public class When_delete_project_by_id_using_filter
        {
            static global::CodeMash.Net.DataContracts.DeleteResult deleteResult;

            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope().Result;
            };

            Because of = () =>
            {
                var projects = CodeMash.FindAsync("Projects", x => true, Builders<Project>.Sort.Descending(p => p.CreatedOn), null, 0, 1).Result;
                var project = projects.First();
                deleteResult = CodeMash.DeleteOneAsync<Project>("Projects", Builders<Project>.Filter.Eq(x => x.Id, project.Id)).Result;
            };

            It should_delete_project_by_id_using_filter_definition = () => deleteResult.DeletedCount.ShouldEqual(1);

            Cleanup after = () => Bootstrapper.CleanUpProjectScope().GetAwaiter().GetResult();
        }
        

        [Tags("FS11000")]
        [Subject(typeof(Project))]
        public class When_get_projects_by_user_id
        {
            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope().Result;
            };

            Because of = () =>
            {
                var users = CodeMash.FindAsync<User>("Users", _ => true).Result;
                var filter = Builders<Project>.Filter.AnyEq(x => x.Users, users.First().Id);
                Projects = CodeMash.FindAsync("Projects", filter).Result;
            };

            It should_return_project_by_user_id = () => Projects.ShouldNotBeNull();
            It should_return_one_project_by_user_id = () => Projects.Count().ShouldEqual(2);

            Cleanup after = () => Bootstrapper.CleanUpProjectScope().GetAwaiter().GetResult();
        }
        
        [Tags("FS10100")]
        [Subject(typeof(Project))]
        public class When_get_list_of_projects_by_page
        {
            static Project Project;
            protected static List<Project> ProjectsWithPage;

            Establish context = () =>
            {
                Projects = Bootstrapper.InitializeProjectScope().Result;
                var availableLanguages = CodeMash.FindAsync<ResourceLanguage>("Languages", _ => true).Result;
                var availableUsers = CodeMash.FindAsync<User>("Users", _ => true).Result;

                var project = new Project
                {
                    Name = "Project2",
                    Description = "It's all about projects right ?",
                    Categories = Defaults.DefaultResourceCategories(availableLanguages.Select(x => x.Id).ToList()).ToList(),
                    SupportedLanguages = availableLanguages.Select(x => x.Id).ToList(),
                    Users = availableUsers.Select(x => x.Id).ToList()
                };
                Project = CodeMash.InsertOneAsync("Projects", project).Result;
            };

            Because of = () =>
            {
                Projects = CodeMash.FindAsync<Project>("Projects", _ => true).Result;
                ProjectsWithPage = CodeMash.FindAsync<Project>("Projects", _ => true, null, null, 0, 1).Result;
            };

            It should_return_projects = () => Projects.ShouldNotBeNull();
            It should_return_projects_with_page = () => ProjectsWithPage.ShouldNotBeNull();
            It should_return_length_of_3 = () => Projects.Count().ShouldEqual(3);
            It should_return_length_of_1 = () => ProjectsWithPage.Count().ShouldEqual(1);

            Cleanup after = () => Bootstrapper.CleanUpProjectScope().GetAwaiter().GetResult();
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
                Projects = Bootstrapper.InitializeProjectScope().Result;
                var availableLanguages = CodeMash.FindAsync<ResourceLanguage>("Languages", _ => true).Result;
                var availableUsers = CodeMash.FindAsync<User>("Users", _ => true).Result;

                var project = new Project
                {
                    Name = "Project2",
                    Description = "It's all about projects right ?",
                    Categories = Defaults.DefaultResourceCategories(availableLanguages.Select(x => x.Id).ToList()).ToList(),
                    SupportedLanguages = availableLanguages.Select(x => x.Id).ToList(),
                    Users = availableUsers.Select(x => x.Id).ToList()
                };
                Project = CodeMash.InsertOneAsync("Projects", project).Result;
            };

            Because of = () =>
            {
                Projects = CodeMash.FindAsync<Project>("Projects", _ => true).Result;
                ProjectsWithPageAscending = CodeMash.FindAsync("Projects", _ => true, Builders<Project>.Sort.Ascending(x => x.Name), null, 0,1).Result;
                ProjectsWithPageDescending = CodeMash.FindAsync("Projects", _ => true, Builders<Project>.Sort.Descending(x => x.Name), null, 0, 1).Result;
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

            Cleanup after = () => Bootstrapper.CleanUpProjectScope().GetAwaiter().GetResult();
        }
        
    }
}
