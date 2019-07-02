using System;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using ServiceStack;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class FindOneAndReplaceTests 
    {
        // TODO : add all possible fields (Selections, Taxonomies, Files, Translatable fields)
        // TODO : play with projections
        // TODO : play with paging and sorting
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
        public void Can_find_one_and_replace_by_id_integration_test()
        {
            _schedule.Destination = "test";
            _schedule.Number = -10;
            _repository.FindOneAndReplace(_schedule.Id, _schedule);

            var schedule = _repository.FindOneById<Schedule>(_schedule.Id);
            
            schedule.ShouldBe<Schedule>();
            Assert.IsNotNull(schedule);
            Assert.AreEqual(schedule.Destination, _schedule.Destination);
            Assert.AreEqual(schedule.Number, _schedule.Number);
        }

        [TestMethod]
        public void Can_find_one_and_replace_value_in_number_integration_test()
        {
            _schedule2.Destination = "test";
            _schedule2.Origin = "test-test";

            _repository.FindOneAndReplace(x => x.Number == _schedule2.Number, _schedule2);

            var schedule = _repository.FindOneById<Schedule>(_schedule2.Id);
            
            schedule.ShouldBe<Schedule>();
            Assert.IsNotNull(schedule);
            Assert.AreEqual(schedule.Destination, _schedule2.Destination);
            Assert.AreEqual(schedule.Origin, _schedule2.Origin);
        }

        [TestMethod]
        public void Exception_find_one_and_replace_with_no_filter_integration_test()
        {
            Assert.ThrowsException<ArgumentNullException>( () => _repository.FindOneAndReplace(null, _schedule2) );
        }

        [TestMethod]
        public void Exception_find_one_and_replace_with_no_entity_integration_test()
        {
            Assert.ThrowsException<ArgumentNullException>( () => _repository.FindOneAndReplace<Schedule>(_schedule.Id, null) );
        }

        [TestMethod]
        public void Exception_find_one_and_replace_not_found_integration_test()
        {
            Assert.ThrowsException<WebServiceException>( () => _repository.FindOneAndReplace<Schedule>(new ObjectId(), _schedule) );
        }

        [TestMethod]
        public void Can_find_one_and_replace_by_object_id_integration_test()
        {
            _schedule.Destination = "test";
            _schedule.Number = -10;

            var a = _repository.FindOneAndReplace(new ObjectId(_schedule.Id), _schedule);

            var schedule = _repository.FindOneById<Schedule>(_schedule.Id);
            
            schedule.ShouldBe<Schedule>();
            Assert.IsNotNull(schedule);
            Assert.AreEqual(schedule.Destination, _schedule.Destination);
            Assert.AreEqual(schedule.Number, _schedule.Number);
        }

        [TestCleanup]
        public void TearDown()
        {
            _repository.DeleteMany<Schedule>(x => true);
        }
    }
}