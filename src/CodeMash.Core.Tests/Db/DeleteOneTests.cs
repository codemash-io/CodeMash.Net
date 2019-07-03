using System;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using DeleteResult = Isidos.CodeMash.ServiceContracts.DeleteResult;
using ErrorMessages = CodeMash.Repository.Statics.Database.ErrorMessages;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class DeleteOneTests
    {
        private IRepository<Schedule> Repository { get; set; }

        private Schedule _schedule;
        
        [TestInitialize]
        public void SetUp()
        {
            Repository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.primary.json");
            
            _schedule = new Schedule
            {
                Destination = "Vilnius",
                Notes = "Express",
                Number = 54,
                Origin = "Kaunas"
            };

            _schedule = Repository.InsertOne(_schedule);
        }

        [TestMethod]
        public void Can_delete_one_integration_test()
        {
            var result = Repository.DeleteOne<Schedule>(_schedule.Id);
            
            result.ShouldBe<DeleteResult>();
            Assert.IsTrue(result.IsAcknowledged);
            Assert.IsTrue(result.DeletedCount == 1);
        }

        [TestMethod]
        public void Cannot_delete_one_no_id_integration_test()
        {
            Assert.ThrowsException<ArgumentNullException>(() => Repository.DeleteOne<Schedule>(null), 
                ErrorMessages.IdIsNotDefined);

            Assert.ThrowsException<ArgumentNullException>(() => Repository.DeleteOne<Schedule>(ObjectId.Empty), 
                ErrorMessages.IdIsNotDefined);
        }

        [TestMethod]
        public void Cannot_delete_one_not_found_integration_test()
        {
            Assert.ThrowsException<InvalidOperationException>(
                () => Repository.DeleteOne<Schedule>("aaaaaaaaaaaaaaaaaaaaaaaa"), 
                ErrorMessages.DocumentNotFound);
        }
        
        [TestCleanup]
        public void TearDown()
        {
            Repository.DeleteMany<Schedule>(x => true);
        }
    }
}