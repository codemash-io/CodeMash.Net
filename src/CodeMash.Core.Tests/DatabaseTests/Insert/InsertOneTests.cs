using System;
using System.Linq;
using System.Threading.Tasks;
using CodeMash.Client;
using CodeMash.Models.Exceptions;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class InsertOneTests : DatabaseTestBase
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
        public void Can_insert_one()
        {
            var insertedRecord = InsertRecord();
            
            insertedRecord.ShouldBe<SdkEntity>();
            Assert.IsNotNull(insertedRecord);
            Assert.IsNotNull(insertedRecord.Id);
            Assert.AreEqual(insertedRecord.Notes, entity.Notes);
        }
        
        [TestMethod]
        public async Task Can_insert_one_async()
        {
            entity = new SdkEntity
            {
                Notes = "Notes",
                Number = 54,
            };
            
            var response = await Repository.InsertOneAsync(entity);
            InsertedRecords[TestContext.TestName] = response.Id;
            var insertedRecord = response;
            
            insertedRecord.ShouldBe<SdkEntity>();
            Assert.IsNotNull(insertedRecord);
            Assert.IsNotNull(insertedRecord.Id);
            Assert.AreEqual(insertedRecord.Notes, entity.Notes);
        }
        
        [TestMethod]
        public async Task Cannot_insert_one_when_collection_not_created()
        {
            Exception expectedException = null;
            
            try
            {
                var client = new CodeMashClient(ApiKey, ProjectId);
                var repository = new CodeMashRepository<NotExistingEntity>(client);
                
                var insertedRecord = await repository.InsertOneAsync(new NotExistingEntity());
            }
            catch (BusinessException e)
            {
                Assert.IsTrue(e.Identifier == "CollectionNotFound");
                expectedException = e;
            }
            
            Assert.IsNotNull(expectedException);
        }
    }
}