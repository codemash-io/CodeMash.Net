using System;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DeleteResult = Isidos.CodeMash.ServiceContracts.DeleteResult;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class DeleteOneTests
    {
        // TODO : add all possible fields (Selections, Taxonomies, Files, Translatable fields)
        // TODO : play with cultures and translatable fields. 

        private IRepository<Schedule> Repository { get; set; }

        private Schedule _schedule;
        
        [TestInitialize]
        public void SetUp()
        {
            Repository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.json");
            
            _schedule = new Schedule
            {
                Destination = "Vilnius",
                Notes = "Express",
                Number = 54,
                Origin = "Kaunas"
            };

            _schedule = Repository.InsertOne(_schedule);
        }

        [TestMethod]
        public void Can_delete_one_integration_test()
        {
            var result = Repository.DeleteOne<Schedule>(_schedule.Id);
            
            result.ShouldBe<DeleteResult>();
            Assert.IsTrue(result.IsAcknowledged);
            Assert.IsTrue(result.DeletedCount > 0);
        }

        [TestMethod]
        public void Cannot_delete_one_no_filter_integration_test()
        {
            Assert.ThrowsException<ArgumentNullException>(() => Repository.DeleteOne<Schedule>(null));
        }
        
        [TestCleanup]
        public void TearDown()
        {
            Repository.DeleteMany<Schedule>(x => true);
        }
    }
}