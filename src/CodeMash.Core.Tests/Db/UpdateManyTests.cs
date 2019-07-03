using System;
using System.Collections.Generic;
using System.Linq;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using UpdateResult = Isidos.CodeMash.ServiceContracts.UpdateResult;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class UpdateManyTests
    {
        private Schedule _schedule, _schedule2, _schedule3, _schedule4;
        private IRepository<Schedule> _repository;
        
        [TestInitialize]
        public void SetUp()
        {
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
            
            _repository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.json");

            _schedule = _repository.InsertOne(_schedule);
            _schedule2 = _repository.InsertOne(_schedule2);
            _schedule3 = _repository.InsertOne(_schedule3);
            _schedule4 = _repository.InsertOne(_schedule4);
        }

        [TestMethod]
        public void Can_update_many_integration_test()
        {
            var update = new UpdateDefinitionBuilder<Schedule>()
            .Set(x => x.Origin, "Kaunas")
            .Set(x => x.Number, 1);

            var result = _repository.UpdateMany(x => true, update, null);
            
            result.ShouldBe<UpdateResult>();
            Assert.IsTrue(result.IsAcknowledged);

            var entitiesFromDb = _repository.Find<Schedule>(x => true);
            entitiesFromDb.ShouldBe<List<Schedule>>();
            entitiesFromDb.ShouldNotNull();
            Assert.AreEqual(entitiesFromDb.Count(x => x.Origin == "Kaunas"), 4);
        }

        [TestMethod]
        public void Cannot_update_many_no_update_definition_integration_test()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => _repository.UpdateMany<Schedule>(x => true, 
                    null, null));
        }
        
        [TestMethod]
        public void Cannot_update_many_filter_not_found_integration_test()
        {
            var update = new UpdateDefinitionBuilder<Schedule>()
                .Set(x => x.Origin, "Kaunas")
                .Set(x => x.Number, 1);

            Assert.ThrowsException<ArgumentException>(
                () => _repository.UpdateMany(
                    x => x.Id == new ObjectId().ToString(), 
                    update, null));
        }

        [TestCleanup]
        public void TearDown()
        {
            _repository.DeleteMany<Schedule>(x => true);
        }
    }
}