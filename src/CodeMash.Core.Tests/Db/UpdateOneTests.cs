using System;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
using UpdateResult = Isidos.CodeMash.ServiceContracts.UpdateResult;
using ErrorMessages = CodeMash.Repository.Statics.Database.ErrorMessages;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class UpdateOneTests
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
        public void Can_update_one_integration_test()
        {
            var update = new UpdateDefinitionBuilder<Schedule>()
            .Set(x => x.Origin, "Kaunas")
            .Set(x => x.Number, 1);

            var result = _repository.UpdateOne(x => x.Id == _schedule4.Id, update, null);
            
            result.ShouldBe<UpdateResult>();
            Assert.IsTrue(result.IsAcknowledged);

            var entityFromDb = _repository.FindOne<Schedule>(x => x.Id == _schedule4.Id);
            entityFromDb.ShouldBe<Schedule>();
            entityFromDb.ShouldNotNull();
            Assert.AreEqual(entityFromDb.Number, 1);
            Assert.AreEqual(entityFromDb.Origin, "Kaunas");
        }
        
        [TestMethod]
        public void Cannot_update_one_no_filter_integration_test()
        {
            var update = new UpdateDefinitionBuilder<Schedule>()
                .Set(x => x.Origin, "Kaunas")
                .Set(x => x.Number, 1);

            Assert.ThrowsException<ArgumentNullException>(
                () => _repository.UpdateOne(filter: null, update: update, updateOptions: null),
                ErrorMessages.FilterIsNotDefined);
            
            Assert.ThrowsException<ArgumentNullException>(
                () => _repository.UpdateOne(FilterDefinition<Schedule>.Empty, update, null),
                ErrorMessages.FilterIsNotDefined);
        }
        
        [TestMethod]
        public void Cannot_update_one_no_update_definition_integration_test()
        {
            Assert.ThrowsException<ArgumentNullException>(
                () => _repository.UpdateOne<Schedule>(x => x.Id == _schedule4.Id, null, null),
                ErrorMessages.UpdateIsNotDefined);
        }
        
        [TestMethod]
        public void Cannot_update_one_id_not_found_integration_test()
        {
            var update = new UpdateDefinitionBuilder<Schedule>()
                .Set(x => x.Origin, "Kaunas")
                .Set(x => x.Number, 1);

            Assert.ThrowsException<ArgumentException>(
                () => _repository.UpdateOne(
                    x => x.Id == new ObjectId().ToString(), update, null),
                ErrorMessages.DocumentNotFound);
        }

        [TestCleanup]
        public void TearDown()
        {
            _repository.DeleteMany<Schedule>(x => true);
        }
    }
}