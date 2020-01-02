using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeMash.Client;
using CodeMash.Models.Exceptions;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class FindTests : DatabaseTestBase
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
            base.TearDown();
        }
        
        [TestMethod]
        public void Can_find_all()
        {
            Repository.DeleteMany(x => true);
            
            var insertedRecord1 = InsertRecord(0);
            var insertedRecord2 = InsertRecord(1);
            var insertedRecordsFromDb = Repository.Find(x => true);
            
            insertedRecordsFromDb.Result.ShouldBe<List<SdkEntity>>();
            Assert.AreEqual(insertedRecordsFromDb.Result.Find(x => x.Id == insertedRecord1.Id).Number, insertedRecord1.Number);
            Assert.AreEqual(insertedRecordsFromDb.Result.Find(x => x.Id == insertedRecord2.Id).Number, insertedRecord2.Number);
        }
        
        [TestMethod]
        public async Task Can_find_all_async()
        {
            await Repository.DeleteManyAsync(x => true);
            
            var insertedRecord1 = InsertRecord(0);
            var insertedRecord2 = InsertRecord(1);
            var insertedRecordsFromDb = await Repository.FindAsync(x => true);
        
            insertedRecordsFromDb.Result.ShouldBe<List<SdkEntity>>();
            Assert.AreEqual(insertedRecordsFromDb.Result.Find(x => x.Id == insertedRecord1.Id).Number, insertedRecord1.Number);
            Assert.AreEqual(insertedRecordsFromDb.Result.Find(x => x.Id == insertedRecord2.Id).Number, insertedRecord2.Number);
        }
        
        [TestMethod]
        public void Can_find_with_pagination()
        {
            Repository.DeleteMany(x => true);
            
            var insertedRecord1 = InsertRecord(0);
            var insertedRecord2 = InsertRecord(1);
            var insertedRecordsFromDb = Repository.Find(x => true, new DatabaseFindOptions
            {
                PageNumber = 0,
                PageSize = 1
            });
            
            insertedRecordsFromDb.Result.ShouldBe<List<SdkEntity>>();
            Assert.AreEqual(insertedRecordsFromDb.Result.Count, 1);
        }
        
        [TestMethod]
        public async Task Can_find_with_pagination_async()
        {
            Repository.DeleteMany(x => true);
            
            var insertedRecord1 = InsertRecord(0);
            var insertedRecord2 = InsertRecord(1);
            var insertedRecordsFromDb = await Repository.FindAsync(x => true, new DatabaseFindOptions
            {
                PageNumber = 0,
                PageSize = 1
            });
            
            insertedRecordsFromDb.Result.ShouldBe<List<SdkEntity>>();
            Assert.AreEqual(insertedRecordsFromDb.Result.Count, 1);
        }
    }
}