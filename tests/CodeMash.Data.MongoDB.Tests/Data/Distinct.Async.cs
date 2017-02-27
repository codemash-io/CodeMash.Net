using System.Threading.Tasks;
using MongoDB.Driver;
using NUnit.Framework;

namespace CodeMash.Data.MongoDB.Tests
{
    [TestFixture]
    public partial class Distinct 
    {
        [Test]
        [Category("Data")]
        public async Task Can_get_right_data_async()
        {
            // Act
            await ProjectRepository.InsertManyAsync(Projects);

            var list = await ProjectRepository.DistinctAsync("Name", x => x.Name == "My first project2", new DistinctOptions());
            
            // Assert
            list.Count.ShouldEqual(1);
        }
    }
}