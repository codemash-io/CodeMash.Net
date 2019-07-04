using System;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using ServiceStack;
using ErrorMessages = CodeMash.Repository.Statics.Database.ErrorMessages;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class FindOneByIdTests 
    {
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
        public void Can_find_one_by_id_integration_test()
        {
            var schedule = _Prepository.FindOneById<Schedule>(_schedule4.Id);
            
            schedule.ShouldBe<Schedule>();
            Assert.IsNotNull(schedule);
            Assert.AreEqual(schedule, _schedule4);
        }


        [TestMethod]
        public void Exception_find_one_with_no_filter_integration_test()
        {
            Assert.ThrowsException<ArgumentNullException>( 
                () => _Prepository.FindOneById<Schedule>(null), ErrorMessages.IdIsNotDefined );

            Assert.ThrowsException<ArgumentNullException>( 
                () => _Prepository.FindOneById<Schedule>(ObjectId.Empty), ErrorMessages.IdIsNotDefined );
        }

        [TestMethod]
        public void Exception_find_one_with_not_found_integration_test()
        {
            Assert.ThrowsException<InvalidOperationException>( 
                () => _Prepository.FindOneById<Schedule>("aaaaaaaaaaaaaaaaaaaaaaaa"), 
                ErrorMessages.DocumentNotFound );
        }

        [TestMethod]
        public void Cant_find_one_by_id_without_access_integration_test()
        {
            Assert.ThrowsException<WebServiceException>( 
                () => _Srepository.FindOneById<Schedule>(_schedule.Id),
                ErrorMessages.AccessNotGranted );
        }

        [TestCleanup]
        public void TearDown()
        {
            _Prepository.DeleteMany<Schedule>(x => true);
        }
    }
}