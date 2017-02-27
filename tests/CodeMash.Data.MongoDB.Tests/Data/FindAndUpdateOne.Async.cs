using System.Threading.Tasks;
using CodeMash.Data.MongoDB.Tests.Data;
using MongoDB.Driver;
using NUnit.Framework;

namespace CodeMash.Data.MongoDB.Tests
{
    [TestFixture]
    public partial class FindAndUpdateOne
    {
        [Test]
        [Category("Data")]
        public async Task Update_nested_element_into_item_async()
        {
            // Act
            await ProjectRepository.InsertOneAsync(Project);
            await ProjectRepository.InsertOneAsync(Project2);

            var ltLanguageId = await LanguagesRepository.FindOneAsync(x => x.CultureCode == "lt-LT");

            var updateResult = await ProjectRepository.FindOneAndUpdateAsync(x => x.Name == "My first project", Builders<Project>.Update.AddToSet(x => x.SupportedLanguages, ltLanguageId.Id), new FindOneAndUpdateOptions<Project>());
            
            // Assert
            updateResult.SupportedLanguages.Count.ShouldEqual(2);
            updateResult.Name.ShouldEqual("My first project");
        }
        
    }
}