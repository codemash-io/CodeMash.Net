using System.Collections.Generic;
using System.Linq;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class CountTests
    {
        private Schedule _schedule, _schedule2, _schedule3, _schedule4;
        private List<Schedule> _schedules;
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

            _schedules = new List<Schedule>{
                _schedule,
                _schedule2,
                _schedule3,
                _schedule4
            };

            _schedules.ForEach(x => _repository.InsertOne(x));
        }

        [TestMethod]
        public void Can_count_integration_test()
        {
            var schedules = _repository.Count(x => true);
            
            schedules.ShouldBe<long>();
            Assert.IsNotNull(schedules);
            Assert.AreEqual(schedules, _schedules.Count);
        }

        [TestMethod]
        public void Can_count_value_in_destination_integration_test()
        {
            var schedules = _repository.Count(x => x.Destination == _schedule.Destination);
            
            schedules.ShouldBe<long>();
            Assert.IsNotNull(schedules);
            Assert.AreEqual(schedules, _schedules.Where(x => x.Destination == _schedule.Destination).Count());
        }

        [TestMethod]
        public void Can_count_value_in_origin_and_destination_integration_test()
        {
            var schedules = _repository.Count(x => x.Origin == "Kaunas" || x.Destination == "Kaunas");
            
            schedules.ShouldBe<long>();
            Assert.IsNotNull(schedules);
            Assert.AreEqual(schedules, _schedules.Where(x => x.Origin == "Kaunas" || x.Destination == "Kaunas").Count());
        }

        [TestMethod]
        public void Can_count_filter_found_nothing_test()
        {
            var schedules = _repository.Count(x => x.Number < 0);
            
            schedules.ShouldBe<long>();
            Assert.IsNotNull(schedules);
            Assert.AreEqual(schedules, 0);
        }

        [TestMethod]
        public void Can_count_with_no_filter_integration_test()
        {
            var schedules = _repository.Count();
            
            schedules.ShouldBe<long>();
            Assert.IsNotNull(schedules);
            Assert.AreEqual(schedules, _schedules.Count);
        }

        [TestCleanup]
        public void TearDown()
        {
            _repository.DeleteMany<Schedule>(x => true);
        }
    }
}