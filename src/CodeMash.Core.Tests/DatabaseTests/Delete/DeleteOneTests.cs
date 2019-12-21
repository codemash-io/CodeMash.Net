using System.Linq;
using System.Threading.Tasks;
using CodeMash.Client;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public partial class DeleteOneTests : DatabaseTestBase
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
        }
        
        [TestMethod]
        public void Can_delete_one()
        {
            var insertedRecord = InsertRecord();

            var deleteResult = Repository.DeleteOne(insertedRecord.Id);
            var deletedRecord = Repository.FindOne(x => x.Id == insertedRecord.Id);
            
            Assert.IsTrue(deleteResult.IsAcknowledged);
            Assert.AreEqual(deleteResult.DeletedCount, 1);
            Assert.IsNull(deletedRecord.Result);
        }
        
        [TestMethod]
        public async Task Can_delete_one_async()
        {
            var insertedRecord = InsertRecord();

            var deleteResult = await Repository.DeleteOneAsync(insertedRecord.Id);

            var deletedRecord = await Repository.FindOneAsync(x => x.Id == insertedRecord.Id);
            
            Assert.IsTrue(deleteResult.IsAcknowledged);
            Assert.AreEqual(deleteResult.DeletedCount, 1);
            Assert.IsNull(deletedRecord.Result);
        }
        
        [TestMethod]
        public async Task Cannot_delete_record_when_record_does_not_exists()
        {
            var deleteResult = Repository.DeleteOneAsync(x => x.Id == ObjectId.GenerateNewId().ToString());
            
            Assert.AreEqual(deleteResult.Result.IsAcknowledged, false);
            Assert.AreEqual(deleteResult.Result.DeletedCount, 0);
        }
    }
}