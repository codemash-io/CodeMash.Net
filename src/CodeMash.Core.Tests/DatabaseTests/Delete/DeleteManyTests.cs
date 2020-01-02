using System.Linq;
using System.Threading.Tasks;
using CodeMash.Client;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public partial class DeleteManyTests : DatabaseTestBase
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
        public void Can_delete_many_by_expression()
        {
            var insertedRecord1 = InsertRecord(0);
            var insertedRecord2 = InsertRecord(1);

            var deleteResult = Repository.DeleteMany(x => x.Id == insertedRecord1.Id || x.Id == insertedRecord2.Id);
            var deletedRecord1 = Repository.FindOne(x => x.Id == insertedRecord1.Id);
            var deletedRecord2 = Repository.FindOne(x => x.Id == insertedRecord2.Id);
            
            Assert.IsTrue(deleteResult.IsAcknowledged);
            Assert.AreEqual(deleteResult.DeletedCount, 2);
            Assert.IsNull(deletedRecord1.Result);
            Assert.IsNull(deletedRecord2.Result);
        }
        
        [TestMethod]
        public async Task Can_delete_many_by_expression_async()
        {
            var insertedRecord1 = InsertRecord(0);
            var insertedRecord2 = InsertRecord(1);

            var deleteResult = await Repository.DeleteManyAsync(x => x.Id == insertedRecord1.Id || x.Id == insertedRecord2.Id);
            var deletedRecord1 = await Repository.FindOneAsync(x => x.Id == insertedRecord1.Id);
            var deletedRecord2 = await Repository.FindOneAsync(x => x.Id == insertedRecord2.Id);
            
            Assert.IsTrue(deleteResult.IsAcknowledged);
            Assert.AreEqual(deleteResult.DeletedCount, 2);
            Assert.IsNull(deletedRecord1.Result);
            Assert.IsNull(deletedRecord2.Result);
        }
    }
}