using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeMash.Client;
using CodeMash.Models;
using CodeMash.Models.Exceptions;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class FindOneTests : DatabaseTestBase
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
        public void Can_find_one_by_id()
        {
            var insertedRecord = InsertRecord();
            var insertedRecordFromDb = Repository.FindOneById(insertedRecord.Id);
            
            insertedRecordFromDb.ShouldBe<SdkEntity>();
            Assert.AreEqual(insertedRecordFromDb.Notes, insertedRecord.Notes);
            Assert.AreEqual(insertedRecordFromDb.Number, insertedRecord.Number);
        }
        
        [TestMethod]
        public async Task Can_find_one_by_id_async()
        {
            var insertedRecord = InsertRecord();
            var insertedRecordFromDb = await Repository.FindOneByIdAsync(insertedRecord.Id);
            
            insertedRecordFromDb.ShouldBe<SdkEntity>();
            Assert.AreEqual(insertedRecordFromDb.Notes, insertedRecord.Notes);
            Assert.AreEqual(insertedRecordFromDb.Number, insertedRecord.Number);
        }
        
        [TestMethod]
        public void Can_find_one_by_object_id()
        {
            var insertedRecord = InsertRecord();
            var insertedRecordFromDb = Repository.FindOneById(ObjectId.Parse(insertedRecord.Id));
            
            insertedRecordFromDb.ShouldBe<SdkEntity>();
            Assert.AreEqual(insertedRecordFromDb.Notes, insertedRecord.Notes);
            Assert.AreEqual(insertedRecordFromDb.Number, insertedRecord.Number);
        }
        
        [TestMethod]
        public async Task Can_find_one_by_object_id_async()
        {
            var insertedRecord = InsertRecord();
            var insertedRecordFromDb = await Repository.FindOneByIdAsync(ObjectId.Parse(insertedRecord.Id));
            
            insertedRecordFromDb.ShouldBe<SdkEntity>();
            Assert.AreEqual(insertedRecordFromDb.Notes, insertedRecord.Notes);
            Assert.AreEqual(insertedRecordFromDb.Number, insertedRecord.Number);
        }
        
        [TestMethod]
        public void Can_find_one_by_expression()
        {
            var insertedRecord = InsertRecord();
            var insertedRecordFromDb = Repository.FindOne(x => x.Id == insertedRecord.Id);
            
            insertedRecordFromDb.ShouldBe<SdkEntity>();
            Assert.AreEqual(insertedRecordFromDb.Notes, insertedRecord.Notes);
            Assert.AreEqual(insertedRecordFromDb.Number, insertedRecord.Number);
        }
        
        [TestMethod]
        public async Task Can_find_one_by_expression_async()
        {
            var insertedRecord = InsertRecord();
            var insertedRecordFromDb = await Repository.FindOneAsync(x => x.Id == insertedRecord.Id);
            
            insertedRecordFromDb.ShouldBe<SdkEntity>();
            Assert.AreEqual(insertedRecordFromDb.Notes, insertedRecord.Notes);
            Assert.AreEqual(insertedRecordFromDb.Number, insertedRecord.Number);
        }
        
        [TestMethod]
        public void Can_find_one_by_definition()
        {
            var insertedRecord = InsertRecord();
            var insertedRecordFromDb = Repository.FindOne(Builders<SdkEntity>.Filter.Eq(x => x.Id, insertedRecord.Id));
            
            insertedRecordFromDb.ShouldBe<SdkEntity>();
            Assert.AreEqual(insertedRecordFromDb.Notes, insertedRecord.Notes);
            Assert.AreEqual(insertedRecordFromDb.Number, insertedRecord.Number);
        }
        
        [TestMethod]
        public async Task Can_find_one_by_definition_async()
        {
            var insertedRecord = InsertRecord();
            var insertedRecordFromDb = await Repository.FindOneAsync(Builders<SdkEntity>.Filter.Eq(x => x.Id, insertedRecord.Id));
            
            insertedRecordFromDb.ShouldBe<SdkEntity>();
            Assert.AreEqual(insertedRecordFromDb.Notes, insertedRecord.Notes);
            Assert.AreEqual(insertedRecordFromDb.Number, insertedRecord.Number);
        }
        
        [TestMethod]
        public async Task Cannot_find_one_when_collection_not_created()
        {
            Exception expectedException = null;
            
            try
            {
                var client = new CodeMashClient(ApiKey, ProjectId);
                var repository = new CodeMashRepository<NotExistingEntity>(client);
                
                await repository.FindOneByIdAsync(ObjectId.GenerateNewId().ToString());
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