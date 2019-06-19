using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class InsertOneTests
    {
        // TODO : add setup which runs before each test
        // TODO : add all possible fields (Selections, Taxonomies, Files, Translatable fields)
        // TODO : play with projections
        // TODO : play with paging and sorting
        // TODO : play with cultures and translatable fields. 

        private IRepository<Schedule> Repository { get; set; }

        private Schedule _schedule;
        
        public InsertOneTests()
        {
            Repository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.json");
            
            _schedule = new Schedule
            {
                Destination = "Vilnius",
                Notes = "Express",
                Number = 54,
                Origin = "Kaunas"
            };
        }

        [TestMethod]
        public void Can_insert_one_integration_test()
        {
            _schedule = Repository.InsertOne(_schedule);

            _schedule.ShouldBe<Schedule>();
            Assert.IsNotNull(_schedule);
            Assert.IsNotNull(_schedule.Id);
        }
    }
}