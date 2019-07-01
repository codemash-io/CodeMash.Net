using System;
using System.Collections.Generic;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class DistinctTests 
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
        public void Can_distinct_different_values_integration_test()
        {
            var schedule = _repository.Distinct("origin", x => true, null);
            
            schedule.ShouldBe<List<string>>();
            Assert.IsNotNull(schedule);
            Assert.AreEqual(schedule.Count, 4);
        }

        [TestMethod]
        public void Can_distinct_repeating_values_integration_test()
        {
            var schedule = _repository.Distinct("destination", x => true, null);
            
            schedule.ShouldBe<List<string>>();
            Assert.IsNotNull(schedule);
            Assert.AreEqual(schedule.Count, 2);
        }

        [TestMethod]
        public void Can_distinct_different_values_with_filter_integration_test()
        {
            var schedule = _repository.Distinct("number", x => x.Number < 1500, null);
            
            schedule.ShouldBe<List<string>>();
            Assert.IsNotNull(schedule);
            Assert.AreEqual(schedule.Count, 3);
        }
        
        [TestCleanup]
        public void TearDown()
        {
            _repository.DeleteMany<Schedule>(x => true);
        }
    }
}