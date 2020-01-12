using System.Linq;
using System.Threading.Tasks;
using CodeMash.Client;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public partial class ReplaceOneTests : DatabaseTestBase
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
        public void Can_replace_one()
        {
            var insertedRecord = InsertRecord();
            
            var updatedRecord = new SdkEntity().PopulateWith(insertedRecord);
            updatedRecord.Number = 13;
            updatedRecord.Notes = "replaced notes";

            var replaceResult = Repository.ReplaceOne(x => x.Id == insertedRecord.Id, updatedRecord);
            var updatedRecordFromDb = Repository.FindOne(x => x.Id == insertedRecord.Id);
            
            Assert.IsTrue(replaceResult.IsAcknowledged);
            Assert.AreEqual(replaceResult.ModifiedCount, 1);
            Assert.AreEqual(replaceResult.MatchedCount, 1);
            
            updatedRecordFromDb.ShouldBe<SdkEntity>();
            Assert.AreEqual(updatedRecordFromDb.Notes, "replaced notes");
            Assert.AreEqual(updatedRecordFromDb.Number, 13);
        }
        
        [TestMethod]
        public async Task Can_replace_one_async()
        {
            var insertedRecord = InsertRecord();
            
            var updatedRecord = new SdkEntity().PopulateWith(insertedRecord);
            updatedRecord.Number = 13;
            updatedRecord.Notes = "replaced notes";

            var replaceResult = await Repository.ReplaceOneAsync(x => x.Id == insertedRecord.Id, updatedRecord);
            var updatedRecordFromDb = await Repository.FindOneAsync(x => x.Id == insertedRecord.Id);
            
            Assert.IsTrue(replaceResult.IsAcknowledged);
            Assert.AreEqual(replaceResult.ModifiedCount, 1);
            Assert.AreEqual(replaceResult.MatchedCount, 1);
            
            updatedRecordFromDb.ShouldBe<SdkEntity>();
            Assert.AreEqual(updatedRecordFromDb.Notes, "replaced notes");
            Assert.AreEqual(updatedRecordFromDb.Number, 13);
        }
    }
}