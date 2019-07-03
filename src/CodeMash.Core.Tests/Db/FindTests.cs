using System;
using System.Collections.Generic;
using System.Linq;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ErrorMessages = CodeMash.Repository.Statics.Database.ErrorMessages;

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
        private IRepository<Schedule> _Prepository; //Primary
        private IRepository<Schedule> _Srepository; //Secondary
        
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
            
            _Prepository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.primary.json");
            _Srepository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.secondary.json");

            _schedule = _Prepository.InsertOne(_schedule);
            _schedule2 = _Prepository.InsertOne(_schedule2);
            _schedule3 = _Prepository.InsertOne(_schedule3);
            _schedule4 = _Prepository.InsertOne(_schedule4);
        }

        [TestMethod]
        public void Can_find_integration_test()
        {
            var schedules = _Prepository.Find<Schedule>(x => true);
            
            schedules.ShouldBe<List<Schedule>>();
            Assert.IsNotNull(schedules);
        }

        [TestMethod]
        public void Can_find_value_in_origin_integration_test()
        {
            var schedules = _Prepository.Find<Schedule>(x => x.Origin == _schedule2.Origin);
            
            schedules.ShouldBe<List<Schedule>>();
            Assert.IsNotNull(schedules);
            Assert.AreEqual(_schedule2, schedules.First());
        }

        [TestMethod]
        public void Can_find_value_in_origin_and_destination_integration_test()
        {
            var schedules = _Prepository.Find<Schedule>(x => x.Origin == "Kaunas" || x.Destination == "Kaunas");
            
            schedules.ShouldBe<List<Schedule>>();
            Assert.IsNotNull(schedules);
            Assert.AreEqual(3, schedules.Count);
        }

        [TestMethod]
        public void Can_find_with_no_filter_integration_test()
        {
            var schedules = _Prepository.Find<Schedule>(null);
            
            schedules.ShouldBe<List<Schedule>>();
            Assert.IsNotNull(schedules);
        }

        [TestMethod]
        public void Exception_find_not_found_integration_test()
        {
            Assert.ThrowsException<InvalidOperationException>( 
                () => _Prepository.Find<Schedule>(x => x.Destination == "gyvenimeTokioDestinationNeduosiu"), 
                ErrorMessages.DocumentNotFound );
        }

        [TestMethod]
        public void Can_find_with_id_integration_test()
        {
            var schedules = _Prepository.Find<Schedule>(x => x.Id == _schedule.Id);
            
            schedules.ShouldBe<List<Schedule>>();
            Assert.IsNotNull(schedules);
            Assert.AreEqual(schedules.First(), _schedule);
        }

        [TestMethod]
        public void Can_find_with_limit_integration_test()
        {
            var schedules = _Prepository.Find<Schedule>(x => true, null, null, 2, null);
            
            schedules.ShouldBe<List<Schedule>>();
        }

        [TestMethod]
        public void Can_find_with_limit_2_and_skip_1_page_integration_test()
        {
            var schedules = _Prepository.Find<Schedule>(x => true, null, 1, 2, null);
            
            schedules.ShouldBe<List<Schedule>>();
            Assert.IsNotNull(schedules);
            Assert.AreEqual(2, schedules.Count);
            Assert.AreEqual(schedules[0], _schedule3);
            Assert.AreEqual(schedules[1], _schedule4);
        }
        
        [TestCleanup]
        public void TearDown()
        {
            _Prepository.DeleteMany<Schedule>(x => true);
        }
    }
}