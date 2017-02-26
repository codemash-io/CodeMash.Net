using System;
using System.Linq;
using CodeMash.Interfaces.Data;
using CodeMash.Tests.Data;
using MongoDB.Bson;
using MongoDB.Driver;
using NUnit.Framework;

namespace CodeMash.Tests
{
    [TestFixture]
    public partial class Delete : TestBase
    {
        private Project Project { get; set; }
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
            

        }

        [Test]
        [Category("Data")]
        public void Can_delete_item_from_db_when_string_is_provided()
        {
            // Act
            ProjectRepository.InsertOne(Project);
            var deleteResult = ProjectRepository.DeleteOne(Project.Id);

            // Assert
            deleteResult.ShouldNotBeNull();
            deleteResult.DeletedCount.ShouldEqual(1);
        }

        [Test]
        [Category("Data")]
        public void Can_delete_item_from_db_when_objectId_is_provided()
        {
            // Act
            ProjectRepository.InsertOne(Project);
            var deleteResult = ProjectRepository.DeleteOne(ObjectId.Parse(Project.Id));

            // Assert
            deleteResult.ShouldNotBeNull();
            deleteResult.DeletedCount.ShouldEqual(1);
        }

        [Test]
        [Category("Data")]
        public void Can_delete_item_from_db_when_expression_is_provided()
        {
            // Act
            ProjectRepository.InsertOne(Project);
            var deleteResult = ProjectRepository.DeleteOne(x => x.Id == Project.Id);

            // Assert
            deleteResult.ShouldNotBeNull();
            deleteResult.DeletedCount.ShouldEqual(1);
        }

        [Test]
        [Category("Data")]
        public void Can_delete_item_from_db_when_filter_definition_is_provided()
        {
            // Act
            ProjectRepository.InsertOne(Project);
            var deleteResult = ProjectRepository.DeleteOne(Builders<Project>.Filter.Eq(x => x.Id, Project.Id));

            // Assert
            deleteResult.ShouldNotBeNull();
            deleteResult.DeletedCount.ShouldEqual(1);
        }


        [Test]
        [Category("Data")]
        public void Can_delete_first_matched_item_from_db_when_filter_is_provided()
        {
            // Act
            ProjectRepository.InsertOne(Project); Project.Id = string.Empty;
            ProjectRepository.InsertOne(Project); Project.Id = string.Empty;
            ProjectRepository.InsertOne(Project);
            var deleteResult = ProjectRepository.DeleteOne(Builders<Project>.Filter.Eq(x => x.Name, "My first project"));

            // Assert
            deleteResult.ShouldNotBeNull();
            deleteResult.DeletedCount.ShouldEqual(1);
        }

        [Test]
        [Category("Data")]
        public void Cannot_delete_item_from_db_when_filter_dont_match_the_record()
        {
            // Act
            ProjectRepository.InsertOne(Project); 
            var deleteResult = ProjectRepository.DeleteOne(Builders<Project>.Filter.Eq(x => x.Name, "My first project!"));

            // Assert
            deleteResult.ShouldNotBeNull();
            deleteResult.DeletedCount.ShouldEqual(0);
        }

        [Test]
        [Category("Data")]
        public void When_record_is_deleted_from_db_should_not_get_from_db()
        {
            // Act
            ProjectRepository.InsertOne(Project); 
            var deleteResult = ProjectRepository.DeleteOne(Builders<Project>.Filter.Eq(x => x.Name, "My first project"));
            
            var project = ProjectRepository.FindOneById(Project.Id);
            
            // Assert
            deleteResult.ShouldNotBeNull();
            deleteResult.DeletedCount.ShouldEqual(1);
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