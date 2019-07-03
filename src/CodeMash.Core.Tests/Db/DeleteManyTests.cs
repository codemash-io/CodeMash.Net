using System;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using DeleteResult = Isidos.CodeMash.ServiceContracts.DeleteResult;
using ErrorMessages = CodeMash.Repository.Statics.Database.ErrorMessages;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class DeleteManyTests
    {
        private IRepository<Schedule> Repository { get; set; }
        private Schedule _schedule, _schedule2, _schedule3, _schedule4;

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
            
            _schedule2 = new Schedule
            {
                Destination = "Kaunas",
                Notes = "Express",
                Number = 154,
                Origin = "Klaipeda"
            };
            
            _schedule3 = new Schedule
            {
                Destination = "Kaunas",
                Notes = "Local",
                Number = 1540,
                Origin = "Palemonas"
            };
            
            _schedule4 = new Schedule
            {
                Destination = "Vilnius",
                Notes = "Local",
                Number = 1380,
                Origin = "Trakai"
            };
            _schedule = Repository.InsertOne(_schedule);
            _schedule2 = Repository.InsertOne(_schedule2);
            _schedule3 = Repository.InsertOne(_schedule3);
            _schedule4 = Repository.InsertOne(_schedule4);
        }

        [TestMethod]
        public void Can_delete_Many_integration_test()
        {
            var result = Repository.DeleteMany<Schedule>(x => true);
            
            result.ShouldBe<DeleteResult>();
            Assert.IsTrue(result.IsAcknowledged);
            Assert.IsTrue(result.DeletedCount > 0);
        }
        
        [TestMethod]
        public void Cannot_delete_many_no_filter_integration_test()
        {
            Assert.ThrowsException<ArgumentNullException>(() => Repository.DeleteMany<Schedule>(filter: null), ErrorMessages.FilterIsNotDefined);
        }
        
        [TestMethod]
        public void Can_delete_many_empty_filter_integration_test()
        {
            var result = Repository.DeleteMany(FilterDefinition<Schedule>.Empty);
            
            result.ShouldBe<DeleteResult>();
            Assert.IsTrue(result.IsAcknowledged);
            Assert.IsTrue(result.DeletedCount > 0);
        }
        
        [TestCleanup]
        public void TearDown()
        {
            Repository.DeleteMany<Schedule>(x => true);
        }
    }
}