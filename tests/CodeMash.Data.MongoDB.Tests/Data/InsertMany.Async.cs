using NUnit.Framework;
using System.Threading.Tasks;

namespace CodeMash.Tests
{
    [TestFixture]
    public partial class InsertMany 
    {

        [Test]
        [Category("Data")]
        public async Task Can_insert_many_projects_async()
        {
            // Act
            await ProjectRepository.InsertManyAsync(Projects);

            var projects = await ProjectRepository.FindAsync(x => true);
            
            // Assert
            projects.Count.ShouldEqual(3);
        }
        
    }
}