using System.Linq;
using System.Threading.Tasks;
using CodeMash.Client;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class UpdateOneTests : DatabaseTestBase
    {
        
        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            
            var client = new CodeMashClient(ApiKey, ProjectId);
            Repository = new CodeMashRepository<SdkEntity>(client);
        }
        
        [TestCleanup]
        public override void TearDown()
        {
            if (InsertedRecords != null && InsertedRecords.Any())
            {
                if (InsertedRecords.ContainsKey(TestContext.TestName))
                {
                    Repository.DeleteOne(InsertedRecords[TestContext.TestName]);
                }
            }
        }
        
        [TestMethod]
        public void Can_update_one()
        {
            var insertedRecord = InsertRecord();
         
            var updateDefinition = Builders<SdkEntity>.Update.Set(x => x.Number, 13);
            
            var updateResult = Repository.UpdateOne(x => x.Id == insertedRecord.Id, updateDefinition);
            var updatedRecordFromDb = Repository.FindOne(x => x.Id == insertedRecord.Id);
            
            Assert.IsTrue(updateResult.IsAcknowledged);
            Assert.AreEqual(updateResult.ModifiedCount, 1);
            Assert.AreEqual(updateResult.MatchedCount, 1);
            
            updatedRecordFromDb.ShouldBe<SdkEntity>();
            Assert.AreEqual(updatedRecordFromDb.Notes, insertedRecord.Notes);
            Assert.AreEqual(updatedRecordFromDb.Number, 13);
        }
        
        [TestMethod]
        public async Task Can_update_one_async()
        {
            var insertedRecord = InsertRecord();
         
            var updateDefinition = Builders<SdkEntity>.Update.Set(x => x.Number, 13);
            
            var updateResult = await Repository.UpdateOneAsync(x => x.Id == insertedRecord.Id, updateDefinition);
            var updatedRecordFromDb = await Repository.FindOneAsync(x => x.Id == insertedRecord.Id);
            
            Assert.IsTrue(updateResult.IsAcknowledged);
            Assert.AreEqual(updateResult.ModifiedCount, 1);
            Assert.AreEqual(updateResult.MatchedCount, 1);
            
            updatedRecordFromDb.ShouldBe<SdkEntity>();
            Assert.AreEqual(updatedRecordFromDb.Notes, insertedRecord.Notes);
            Assert.AreEqual(updatedRecordFromDb.Number, 13);
        }
    }
}