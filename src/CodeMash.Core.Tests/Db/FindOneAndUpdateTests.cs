using System;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using ServiceStack;
using ErrorMessages = CodeMash.Repository.Statics.Database.ErrorMessages;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class FindOneAndUpdateTests 
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
        public void Can_find_one_and_Update_by_id_integration_test()
        {
            var options = new FindOneAndUpdateOptions<BsonDocument>{
                ReturnDocument = ReturnDocument.After
            };

            var update = new UpdateDefinitionBuilder<Schedule>()
                .Set(x => x.Destination, "test")
                .Set(x => x.Number, -10);

            _schedule.Destination = "test";
            _schedule.Number = -10;

            var schedule = _repository.FindOneAndUpdate(_schedule.Id, update, options);
            
            schedule.ShouldBe<Schedule>();
            Assert.IsNotNull(schedule);
            Assert.AreEqual(schedule.Destination, _schedule.Destination);
            Assert.AreEqual(schedule.Number, _schedule.Number);
        }

        [TestMethod]
        public void Can_find_one_and_Update_value_in_number_integration_test()
        {
            var options = new FindOneAndUpdateOptions<BsonDocument>{
                ReturnDocument = ReturnDocument.After
            };
            
            _schedule2.Destination = "test";
            _schedule2.Origin = "test-test";

            var update = new UpdateDefinitionBuilder<Schedule>()
                .Set(x => x.Destination, "test")
                .Set(x => x.Origin, "test-test");

            var schedule = _repository.FindOneAndUpdate(x => x.Number == _schedule2.Number, update, options);
            
            schedule.ShouldBe<Schedule>();
            Assert.IsNotNull(schedule);
            Assert.AreEqual(schedule.Destination, _schedule2.Destination);
            Assert.AreEqual(schedule.Origin, _schedule2.Origin);
        }

        [TestMethod]
        public void Exception_find_one_and_Update_with_no_filter_integration_test()
        {
            var update = new UpdateDefinitionBuilder<Schedule>()
                .Set(x => x.Destination, "test")
                .Set(x => x.Origin, "test-test");

            Assert.ThrowsException<ArgumentNullException>( () => _repository.FindOneAndUpdate(null, update),
            ErrorMessages.FilterIsNotDefined );
        }

        [TestMethod]
        public void Exception_find_one_and_Update_with_no_update_integration_test()
        {
            Assert.ThrowsException<ArgumentNullException>( () => _repository.FindOneAndUpdate<Schedule>(_schedule.Id, null),
            ErrorMessages.UpdateIsNotDefined );
        }

        [TestMethod]
        public void Exception_find_one_and_Update_not_found_integration_test()
        {
            var update = new UpdateDefinitionBuilder<Schedule>()
                .Set(x => x.Destination, "test")
                .Set(x => x.Origin, "test-test");

            //CodeMash API throws BusinessException which is recieved as a ServiceStack.WebServiceException
            Assert.ThrowsException<WebServiceException>( () => _repository.FindOneAndUpdate(new ObjectId("aaaaaaaaaaaaaaaaaaaaaaaa"), update, null) );
        }

        [TestMethod]
        public void Can_find_one_and_Update_by_object_id_integration_test()
        {
            var options = new FindOneAndUpdateOptions<BsonDocument>{
                ReturnDocument = ReturnDocument.After
            };
            
            _schedule2.Destination = "test";
            _schedule2.Origin = "test-test";

            var update = new UpdateDefinitionBuilder<Schedule>()
                .Set(x => x.Destination, "test")
                .Set(x => x.Origin, "test-test");

            var schedule = _repository.FindOneAndUpdate(new ObjectId(_schedule2.Id), update, options);
            
            schedule.ShouldBe<Schedule>();
            Assert.IsNotNull(schedule);
            Assert.AreEqual(schedule.Destination, _schedule2.Destination);
            Assert.AreEqual(schedule.Origin, _schedule2.Origin);
        }

        [TestCleanup]
        public void TearDown()
        {
            _repository.DeleteMany<Schedule>(x => true);
        }
    }
}