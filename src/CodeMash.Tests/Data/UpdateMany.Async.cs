using System.Linq;
using System.Threading.Tasks;
using CodeMash.Interfaces.Data;
using CodeMash.Tests.Data;
using MongoDB.Driver;
using NUnit.Framework;

namespace CodeMash.Tests
{
    [TestFixture]
    public partial class UpdateMany
    {
        [Test]
        [Category("Data")]
        public async Task Update_nested_elements_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            await ProjectRepository.InsertOneAsync(Project2);

            var ltLanguageId = await LanguagesRepository.FindOneAsync(x => x.CultureCode == "lt-LT");
            
            var updateResult = await ProjectRepository.UpdateManyAsync(x => x.Name == "My first project", Builders<Project>.Update.AddToSet(x => x.SupportedLanguages, ltLanguageId.Id), new UpdateOptions());

            // Assert
            updateResult.ShouldNotBeNull();
            updateResult.MatchedCount.ShouldEqual(2);
            updateResult.ModifiedCount.ShouldEqual(2);
        }


        [Test]
        [Category("Data")]
        public async Task Dont_update_nested_elements_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            await ProjectRepository.InsertOneAsync(Project2);

            var ltLanguageId = await LanguagesRepository.FindOneAsync(x => x.CultureCode == "lt-LT");
            
            var updateResult = await ProjectRepository.UpdateManyAsync(x => x.Name == "My first project!", Builders<Project>.Update.AddToSet(x => x.SupportedLanguages, ltLanguageId.Id), new UpdateOptions());

            // Assert
            updateResult.ShouldNotBeNull();
            updateResult.MatchedCount.ShouldEqual(0);
            updateResult.ModifiedCount.ShouldEqual(0);
        }
        
    }
}