using System;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class FindOneByIdTests 
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
        public void Can_find_one_by_id_integration_test()
        {
            var schedule = _repository.FindOneById<Schedule>(_schedule4.Id);
            
            schedule.ShouldBe<Schedule>();
            Assert.IsNotNull(schedule);
            Assert.AreEqual(schedule, _schedule4);
        }

        [TestMethod]
        public void Exception_find_one_by_id_with_no_id_integration_test()
        {
            Assert.ThrowsException<ArgumentNullException>( () => _repository.FindOneById<Schedule>(null) );
        }

        [TestMethod]
        public void Exception_find_one_by_id_not_found_test()
        {
            var schedule = _repository.FindOneById<Schedule>(new ObjectId());
            
            Assert.IsNull(schedule);
        }

        [TestCleanup]
        public void TearDown()
        {
            _repository.DeleteMany<Schedule>(x => true);
        }
    }
}