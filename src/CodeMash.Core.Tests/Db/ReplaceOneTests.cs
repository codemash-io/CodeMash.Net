using System;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using ServiceStack.FluentValidation.Results;
using ReplaceOneResult = Isidos.CodeMash.ServiceContracts.ReplaceOneResult;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class ReplaceOneTests
    {
        // TODO : add all possible fields (Selections, Taxonomies, Files, Translatable fields)
        // TODO : play with cultures and translatable fields. 

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
            Assert.AreEqual(entityFromDb, _schedule4);
        }
        
        [TestMethod]
        public void Cannot_replace_one_no_filter_integration_test()
        {
            _schedule4.Origin = "Kaunas";
            _schedule4.Number = 1;

            Assert.ThrowsException<ArgumentNullException>(
                () => _repository.ReplaceOne(null, _schedule4, null));
            
            Assert.ThrowsException<ArgumentNullException>(
                () => _repository.ReplaceOne(FilterDefinition<Schedule>.Empty, _schedule4, null));
        }
        
        [TestMethod]
        public void Cannot_replace_one_no_entity_integration_test()
        {
            _schedule4.Origin = "Kaunas";
            _schedule4.Number = 1;

            Assert.ThrowsException<ArgumentNullException>(
                () => _repository.ReplaceOne<Schedule>(x => x.Id == _schedule4.Id, 
                    null, null));
            
            Assert.ThrowsException<ArgumentNullException>(
                () => _repository.ReplaceOne(x => x.Id == _schedule4.Id, 
                    default(Schedule), null));
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
                    _schedule4, null));
        }

        [TestCleanup]
        public void TearDown()
        {
            _repository.DeleteMany<Schedule>(x => true);
        }
    }
}