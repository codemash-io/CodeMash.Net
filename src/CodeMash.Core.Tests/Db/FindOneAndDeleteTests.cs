using System;
using System.Linq;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using ServiceStack;
using ErrorMessages = CodeMash.Repository.Statics.Database.ErrorMessages;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class FindOneAndDeleteTests 
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
        public void Can_find_one_and_Delete_by_id_integration_test()
        {
            var schedule = _repository.FindOneAndDelete<Schedule>(_schedule.Id);
            
            schedule.ShouldBe<Schedule>();
            Assert.IsNotNull(schedule);
            Assert.AreEqual(schedule, _schedule);

            var schedules = _repository.Find<Schedule>(x => true);
            Assert.IsFalse(schedules.Select(x => x.Id).Contains(_schedule.Id));
        }

        [TestMethod]
        public void Can_find_one_and_Delete_value_in_number_integration_test()
        {
            var schedule = _repository.FindOneAndDelete<Schedule>(x => x.Number == _schedule2.Number);
            
            schedule.ShouldBe<Schedule>();
            Assert.IsNotNull(schedule);
            Assert.AreEqual(schedule, _schedule2);

            var schedules = _repository.Find<Schedule>(x => true);
            Assert.IsFalse(schedules.Select(x => x.Id).Contains(_schedule2.Id));
        }

        [TestMethod]
        public void Exception_find_one_and_Delete_with_no_filter_integration_test()
        {
            Assert.ThrowsException<ArgumentNullException>( () => _repository.FindOneAndDelete<Schedule>(null),
            ErrorMessages.FilterIsNotDefined);
        }

        [TestMethod]
        public void Exception_find_one_and_Delete_not_found_integration_test()
        {
            //CodeMash API throws BusinessException which is recieved as a ServiceStack.WebServiceException
            Assert.ThrowsException<WebServiceException>( () => _repository.FindOneAndDelete<Schedule>(new ObjectId("aaaaaaaaaaaaaaaaaaaaaaaa")) );
        }

        [TestMethod]
        public void Can_find_one_and_Delete_by_object_id_integration_test()
        {
            var schedule = _repository.FindOneAndDelete<Schedule>(new ObjectId(_schedule2.Id));
            
            schedule.ShouldBe<Schedule>();
            Assert.IsNotNull(schedule);
            Assert.AreEqual(schedule, _schedule2);

            var schedules = _repository.Find<Schedule>(x => true);
            Assert.IsFalse(schedules.Select(x => x.Id).Contains(_schedule2.Id));
        }

        [TestCleanup]
        public void TearDown()
        {
            _repository.DeleteMany<Schedule>(x => true);
        }
    }
}