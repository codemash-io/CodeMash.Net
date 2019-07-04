using System;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using ServiceStack;
using ErrorMessages = CodeMash.Repository.Statics.Database.ErrorMessages;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class FindOneTests 
    {
        // TODO : add all possible fields (Selections, Taxonomies, Files, Translatable fields)
        // TODO : play with cultures and translatable fields. 

        private Schedule _schedule, _schedule2, _schedule3, _schedule4;
        private IRepository<Schedule> _Prepository; //Primary
        private IRepository<Schedule> _Srepository; //Secondary
        private ProjectionDefinition<Schedule> projection;
        
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
                Number = 1380,
                Origin = "Palemonas"
            };
            
            _schedule4 = new Schedule
            {
                Destination = "Vilnius",
                Notes = "Local",
                Number = 1540,
                Origin = "Trakai"
            };
            
            _Prepository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.primary.json");
            _Srepository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.secondary.json");

            _schedule = _Prepository.InsertOne(_schedule);
            _schedule2 = _Prepository.InsertOne(_schedule2);
            _schedule3 = _Prepository.InsertOne(_schedule3);
            _schedule4 = _Prepository.InsertOne(_schedule4);

            projection = new ProjectionDefinitionBuilder<Schedule>()
                .Include(x => x.Origin)
                .Include(x => x.Number)
                .Exclude(x => x.Id);
        }
        
        [TestMethod]
        public void Can_find_one_integration_test()
        {
            var schedule = _Prepository.FindOne<Schedule>(x => x.Origin == _schedule4.Origin);
            
            schedule.ShouldBe<Schedule>();
            Assert.IsNotNull(schedule);
            Assert.AreEqual(schedule, _schedule4);
        }

        [TestMethod]
        public void Can_find_one_value_in_number_integration_test()
        {
            var schedule = _Prepository.FindOne<Schedule>(x => x.Number == _schedule.Number);
            
            schedule.ShouldBe<Schedule>();
            Assert.IsNotNull(schedule);
            Assert.AreEqual(schedule, _schedule);
        }

        [TestMethod]
        public void Exception_find_one_with_no_filter_integration_test()
        {
            Assert.ThrowsException<ArgumentNullException>( 
                () => _Prepository.FindOne<Schedule>(null), 
                ErrorMessages.FilterIsNotDefined );
            
            Assert.ThrowsException<ArgumentNullException>( 
                () => _Prepository.FindOne<Schedule>(FilterDefinition<Schedule>.Empty), 
                ErrorMessages.FilterIsNotDefined );
        }

        [TestMethod]
        public void Exception_find_one_not_found_integration_test()
        {
            Assert.ThrowsException<InvalidOperationException>( 
                () => _Prepository.FindOne<Schedule>(x => x.Destination == "gyvenimeTokioDestinationNeduosiu"), 
                ErrorMessages.DocumentNotFound );
        }

        [TestMethod]
        public void Can_find_one_with_id_integration_test()
        {
            var schedule = _Prepository.FindOne<Schedule>(x => x.Id == _schedule2.Id);
            
            schedule.ShouldBe<Schedule>();
            Assert.IsNotNull(schedule);
            Assert.AreEqual(_schedule2, schedule);
        }

        [TestMethod]
        public void Can_find_one_first_with_destination_integration_test()
        {
            var schedule = _Prepository.FindOne<Schedule>(x => x.Destination == "Kaunas");
        
            schedule.ShouldBe<Schedule>();
            Assert.IsNotNull(schedule);
            Assert.AreEqual(_schedule2, schedule);
        }

        [TestMethod]
        public void Cant_find_without_access_integration_test()
        {
            Assert.ThrowsException<WebServiceException>( 
                () => _Srepository.FindOne<Schedule>(x => true),
                ErrorMessages.AccessNotGranted );
        }

        [TestMethod]
        public void Can_find_with_projection_integration_test()
        {
            var schedule = _Prepository.FindOne<Schedule, Schedule>(x => x.Id == _schedule2.Id, projection, null);

            Assert.AreEqual(schedule.Origin, _schedule2.Origin);
            Assert.AreEqual(schedule.Id, null);
            Assert.AreEqual(schedule.Destination, null);
        }

        [TestCleanup]
        public void TearDown()
        {
            _Prepository.DeleteMany<Schedule>(x => true);
        }
    }
}