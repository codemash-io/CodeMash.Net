using System;
using System.Collections.Generic;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ErrorMessages = CodeMash.Repository.Statics.Database.ErrorMessages;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class InsertManyTests
    {
        private Schedule _schedule, _schedule2, _schedule3, _schedule4;
        private List<Schedule> schedules;
        private IRepository<Schedule> _repository;
        
        [TestInitialize]
        public void SetUp()
        {
            _repository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.json");
            
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

            schedules = new List<Schedule>
            {
                _schedule,
                _schedule2,
                _schedule3,
                _schedule4
            };
        }

        [TestMethod]
        public void Can_insert_many_integration_test()
        {            
            var inserted = _repository.InsertMany(schedules);

            inserted.ShouldBe<bool>();
            Assert.IsTrue(inserted);

            var schedulesFromDb = _repository.Find<Schedule>(x => true);
            
            schedules.ShouldBe<List<Schedule>>();
            Assert.IsNotNull(schedules);
            Assert.Equals(schedules, schedulesFromDb);
        }

        [TestMethod]
        public void Exception_insert_many_entities_null_integration_test()
        {
            Assert.ThrowsException<ArgumentNullException>( () => _repository.InsertMany<Schedule>(null), 
                ErrorMessages.EntityIsNotDefined);
        }
        
        [TestCleanup]
        public void TearDown()
        {
            _repository.DeleteMany<Schedule>(x => true);
        }
    }
}