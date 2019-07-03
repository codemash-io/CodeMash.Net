using System;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using ErrorMessages = CodeMash.Repository.Statics.Database.ErrorMessages;
using ReplaceOneResult = Isidos.CodeMash.ServiceContracts.ReplaceOneResult;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class ReplaceOneTests
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
            
            _repository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.primary.json");

            _schedule = _repository.InsertOne(_schedule);
            _schedule2 = _repository.InsertOne(_schedule2);
            _schedule3 = _repository.InsertOne(_schedule3);
            _schedule4 = _repository.InsertOne(_schedule4);
        }

        [TestMethod]
        public void Can_replace_one_integration_test()
        {
            _schedule4.Origin = "Kaunas";
            _schedule4.Number = 1;

            var result = _repository.ReplaceOne(x => x.Id == _schedule4.Id, _schedule4, null);
            
            result.ShouldBe<ReplaceOneResult>();
            Assert.IsTrue(result.IsAcknowledged);

            var entityFromDb = _repository.FindOne<Schedule>(x => x.Id == _schedule4.Id);
            entityFromDb.ShouldBe<Schedule>();
            entityFromDb.ShouldNotNull();
            Assert.AreEqual(entityFromDb.Origin, _schedule4.Origin);
            Assert.AreEqual(entityFromDb.Number, _schedule4.Number);
        }
        
        [TestMethod]
        public void Cannot_replace_one_no_filter_integration_test()
        {
            _schedule4.Origin = "Kaunas";
            _schedule4.Number = 1;

            Assert.ThrowsException<ArgumentNullException>(
                () => _repository.ReplaceOne(null, _schedule4, null), 
                ErrorMessages.FilterIsNotDefined);
            
            Assert.ThrowsException<ArgumentNullException>(
                () => _repository.ReplaceOne(FilterDefinition<Schedule>.Empty, _schedule4, null),
                ErrorMessages.FilterIsNotDefined);
        }
        
        [TestMethod]
        public void Cannot_replace_one_no_entity_integration_test()
        {
            _schedule4.Origin = "Kaunas";
            _schedule4.Number = 1;

            Assert.ThrowsException<ArgumentNullException>(
                () => _repository.ReplaceOne<Schedule>(x => x.Id == _schedule4.Id, 
                    null, null), ErrorMessages.EntityIsNotDefined);
            
            Assert.ThrowsException<ArgumentNullException>(
                () => _repository.ReplaceOne(x => x.Id == _schedule4.Id, 
                    default(Schedule), null), ErrorMessages.EntityIsNotDefined);
        }
        
        [TestMethod]
        public void Cannot_replace_one_id_not_found_integration_test()
        {
            _schedule4.Origin = "Kaunas";
            _schedule4.Number = 1;
            _schedule4.Id = "";

            Assert.ThrowsException<ArgumentException>(
                () => _repository.ReplaceOne(
                    x => x.Id == new ObjectId().ToString(), 
                    _schedule4, null), ErrorMessages.DocumentNotFound);
        }

        [TestCleanup]
        public void TearDown()
        {
            _repository.DeleteMany<Schedule>(x => true);
        }
    }
}