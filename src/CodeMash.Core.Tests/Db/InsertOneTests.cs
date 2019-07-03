using System;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ErrorMessages = CodeMash.Repository.Statics.Database.ErrorMessages;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class InsertOneTests
    {
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
        }

        [TestMethod]
        public void Can_insert_one_integration_test()
        {
            _schedule = Repository.InsertOne(_schedule);

            _schedule.ShouldBe<Schedule>();
            Assert.IsNotNull(_schedule);
            Assert.IsNotNull(_schedule.Id);
        }

        [TestMethod]
        public void Exception_insert_one_entity_null_integration_test()
        {

            Assert.ThrowsException<ArgumentNullException>(() => Repository.InsertOne<Schedule>(null), ErrorMessages.EntityIsNotDefined);
        }
        
        [TestCleanup]
        public void TearDown()
        {
            Repository.DeleteMany<Schedule>(x => true);
        }
    }
}