using NUnit.Framework;

namespace CodeMash.Data.MongoDB.Tests
{
    [TestFixture]
    public partial class Count
    {
        [Test]
        [Category("Data")]
        public void Can_get_right_count_async()
        {
            // Act
            ProjectRepository.InsertMany(Projects);

            var count = ProjectRepository.Count(x => x.Name == "My first project2");
            
            // Assert
            count.ShouldEqual(2);
        }
        
    }
}