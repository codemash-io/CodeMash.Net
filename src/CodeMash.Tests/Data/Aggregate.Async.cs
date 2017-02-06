using System.Collections.Generic;
using System.Threading.Tasks;
using CodeMash.Data;
using CodeMash.Interfaces.Data;
using CodeMash.Tests.Data;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using NUnit.Framework;

namespace CodeMash.Tests
{
    
    [TestFixture]
    public partial class Aggregate
    {
        [Test]
        [Category("Data")]
        public async Task Can_get_aggregated_data_async()
        {
            
            // Act
            await ProjectRepository.InsertManyAsync(Projects);
            
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


            var list = await ProjectRepository.AggregateAsync(pipeline, new AggregateOptions() {});
            
            // Assert
            list.Count.ShouldEqual(1);
        }
        
    }
}