using System;
using System.Collections.Generic;
using System.Linq;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class FindTests
    {
        // TODO : add all possible fields (Selections, Taxonomies, Files, Translatable fields)
        // TODO : play with projections
        // TODO : play with paging and sorting
        // TODO : play with cultures and translatable fields. 

        private Schedule _schedule, _schedule2, _schedule3, _schedule4;
        private IRepository<Schedule> _repository;
        
        public FindTests()
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

        
        public void Can_find_integration_test()
        {
            var schedules = _repository.Find<Schedule>(x => true);
            
            schedules.ShouldBe<List<Schedule>>();
            Assert.IsNotNull(schedules);
        }

        [TestMethod]
        public void Can_find_value_in_origin_integration_test()
        {
            var schedules = _repository.Find<Schedule>(x => x.Origin == _schedule2.Origin);
            
            schedules.ShouldBe<List<Schedule>>();
            Assert.IsNotNull(schedules);
            Assert.AreEqual(_schedule2, schedules.First());
        }

        [TestMethod]
        public void Can_find_value_in_origin_and_destination_integration_test()
        {
            var schedules = _repository.Find<Schedule>(x => x.Origin == "Vilnius" || x.Destination == "Vilnius");
            
            schedules.ShouldBe<List<Schedule>>();
            Assert.IsNotNull(schedules);
            Assert.AreEqual(2, schedules.Count);
        }

        [TestMethod]
        public void Can_find_with_no_filter_integration_test()
        {
            var schedules = _repository.Find<Schedule>(null);
            
            schedules.ShouldBe<List<Schedule>>();
            Assert.IsNotNull(schedules);
        }

        [TestMethod]
        public void Can_find_with_id_integration_test()
        {
            var schedules = _repository.Find<Schedule>(x => x.Id == _schedule.Id);
            
            schedules.ShouldBe<List<Schedule>>();
            Assert.IsNotNull(schedules);
            Assert.AreEqual(schedules.First(), _schedule);
        }

        [TestMethod]
        public void Can_find_with_limit_integration_test()
        {
            var schedules = _repository.Find<Schedule>(x => true, null, null, 2, null);
            
            schedules.ShouldBe<List<Schedule>>();
        }

        [TestMethod]
        public void Can_find_with_limit_2_and_skip_1_page_integration_test()
        {
            var schedules = _repository.Find<Schedule>(x => true, null, 1, 2, null);
            
            schedules.ShouldBe<List<Schedule>>();
            Assert.IsNotNull(schedules);
            Assert.AreEqual(2, schedules.Count);
            Assert.AreEqual(schedules[0], _schedule3);
            Assert.AreEqual(schedules[1], _schedule4);
        }
    }
}