using System.Linq;
using System.Threading.Tasks;
using CodeMash.Tests.Data;
using MongoDB.Driver;
using NUnit.Framework;

namespace CodeMash.Tests
{
    [TestFixture]
    public partial class UpdateOne
    {
        [Test]
        [Category("Data")]
        public async Task Update_nested_elements_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);

            var ltLanguageId = await LanguagesRepository.FindOneAsync(x => x.CultureCode == "lt-LT");
            
            await ProjectRepository.UpdateOneAsync(x => x.Id == Project.Id, Builders<Project>.Update.AddToSet(x => x.SupportedLanguages, ltLanguageId.Id), new UpdateOptions());

            // TODO : bug with Find in Array
            var filter = "{ 'SupportedLanguages' : '" + ltLanguageId.Id + "'}";

            var project = await ProjectRepository.FindOneAsync(filter);
        
            // Assert
            project.ShouldNotBeNull();
            project.SupportedLanguages.Count.ShouldEqual(3);
        }


        [Test]
        [Category("Data")]
        public async Task Update_nested_element_into_item_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            await ProjectRepository.InsertOneAsync(Project2);

            var ltLanguageId = await LanguagesRepository.FindOneAsync(x => x.CultureCode == "lt-LT");
            
            var updateResult = await ProjectRepository.UpdateOneAsync(x => x.Name == "My first project", Builders<Project>.Update.AddToSet(x => x.SupportedLanguages, ltLanguageId.Id), new UpdateOptions());

            // TODO : bug with Find in Array
            var filter = "{ 'SupportedLanguages' : '" + ltLanguageId.Id + "'}";

            var project = await ProjectRepository.FindOneAsync(filter);

            // Assert
            updateResult.MatchedCount.ShouldEqual(1);
            updateResult.ModifiedCount.ShouldEqual(1);
        }
    }
}