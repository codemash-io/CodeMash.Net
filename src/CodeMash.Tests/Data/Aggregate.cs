using System.Collections.Generic;
using CodeMash.Data;
using CodeMash.Interfaces.Data;
using CodeMash.Tests.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using NUnit.Framework;

namespace CodeMash.Tests
{
    [BsonIgnoreExtraElements]
    public class ProjectWithTheSameName
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }


    [TestFixture]
    public partial class Aggregate : TestBase
    {
        public Aggregate()
        {
            Projects = new List<Project>();
        }
        
        private Project Project1 { get; set; }
        private Project Project2 { get; set; }
        private Project Project3 { get; set; }
        private List<Project> Projects { get; set; }
        public IMongoRepository<Project> ProjectRepository { get; set; }
        
        protected override void Initialize()
        {
            base.Initialize();

            ProjectRepository = Resolve<IMongoRepository<Project>>();
        
            // Arrange
            Project1 = new Project { Name = "My first project" };
            Project2 = new Project { Name = "My first project2" };
            Project3 = new Project { Name = "My first project2" };

            Projects.AddRange(new [] { Project1, Project2, Project3 });
        }
        
        [Test]
        [Category("Data")]
        public void Can_get_aggregated_data()
        {
            
            // Act
            ProjectRepository.InsertMany(Projects);
            
            var match = new BsonDocument
            {
                {
                    "$match",
                    new BsonDocument {{"Name", "My first project2" }}
                }
            };

            var group = new BsonDocument(
                "$group", new BsonDocument {
                    { "_id" , "$Name"},
                    {"Count", new BsonDocument("$sum", 1)}
            });

            var projection = BsonDocument.Parse("{ '$project': {'Name' : '$_id','Count' : 1}}");

            var pipeline = new BsonDocumentStagePipelineDefinition<Project, ProjectWithTheSameName>(new []
            {
                match,
                group,
                projection
            });


            var list = ProjectRepository.Aggregate(pipeline, new AggregateOptions() {});
            
            // Assert
            list.Count.ShouldEqual(1);
        }
        
        protected override void Dispose()
        {
            base.Dispose();

            // TODO : drop collections, instead of removing one by one items. 

            ProjectRepository.DeleteMany(x => true);
        }
    }
}