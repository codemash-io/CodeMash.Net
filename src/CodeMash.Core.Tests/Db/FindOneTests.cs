using System;
using System.Collections.Generic;
using CodeMash.Repository;
using NUnit.Framework;

namespace CodeMash.Core.Tests
{
    [TestFixture]
    public class FindOneTests 
    {        
        // TODO : add all possible fields (Selections, Taxonomies, Files, Translatable fields)
        // TODO : play with projections
        // TODO : play with paging and sorting
        // TODO : play with cultures and translatable fields. 

        private Schedule _schedule, _schedule2, _schedule3, _schedule4;
        private IRepository<Schedule> _repository;
        
        [SetUp]
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
        
        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("FindOne")]
        public void Can_find_one_integration_test()
        {
            var schedule = _repository.FindOne<Schedule>(x => x.Origin == "Trakai");
            
            Assert.IsInstanceOf<Schedule>(schedule);
            Assert.IsNotNull(schedule);
            Assert.AreEqual(schedule, _schedule4);
        }

        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("FindOne")]
        public void Can_find_one_value_in_number_integration_test()
        {
            var schedule = _repository.FindOne<Schedule>(x => x.Number == _schedule.Number);
            
            Assert.IsInstanceOf<Schedule>(schedule);
            Assert.IsNotNull(schedule);
            Assert.AreEqual(schedule, _schedule);
        }

        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("FindOneOne")]
        public void Exception_find_one_with_no_filter_integration_test()
        {
            Assert.Throws<ArgumentNullException>( () => _repository.FindOne<Schedule>(null) );
        }

        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("FindOne")]
        public void Can_find_one_with_id_integration_test()
        {
            var schedule = _repository.FindOne<Schedule>(x => x.Id == _schedule2.Id);
            
            Assert.IsInstanceOf<Schedule>(schedule);
            Assert.IsNotNull(schedule);
            Assert.AreEqual(_schedule2, schedule);
        }

        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("FindOne")]
        public void Can_find_one_first_with_destination_integration_test()
        {
            var schedule = _repository.FindOne<Schedule>(x => x.Destination == "Kaunas");
            
            Assert.IsInstanceOf<Schedule>(schedule);
            Assert.IsNotNull(schedule);
            Assert.AreEqual(_schedule2, schedule);
        }
    }
}