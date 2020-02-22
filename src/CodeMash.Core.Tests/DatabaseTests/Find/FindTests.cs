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
using Newtonsoft.Json;

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
            
            insertedRecordsFromDb.Items.ShouldBe<List<SdkEntity>>();
            Assert.AreEqual(insertedRecordsFromDb.Items.Find(x => x.Id == insertedRecord1.Id).Number, insertedRecord1.Number);
            Assert.AreEqual(insertedRecordsFromDb.Items.Find(x => x.Id == insertedRecord2.Id).Number, insertedRecord2.Number);
        }
        
        [TestMethod]
        public async Task Can_find_all_async()
        {
            await Repository.DeleteManyAsync(x => true);
            
            var insertedRecord1 = InsertRecord(0);
            var insertedRecord2 = InsertRecord(1);
            var insertedRecordsFromDb = await Repository.FindAsync(x => true);
        
            insertedRecordsFromDb.Items.ShouldBe<List<SdkEntity>>();
            Assert.AreEqual(insertedRecordsFromDb.Items.Find(x => x.Id == insertedRecord1.Id).Number, insertedRecord1.Number);
            Assert.AreEqual(insertedRecordsFromDb.Items.Find(x => x.Id == insertedRecord2.Id).Number, insertedRecord2.Number);
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
            
            insertedRecordsFromDb.Items.ShouldBe<List<SdkEntity>>();
            Assert.AreEqual(insertedRecordsFromDb.Items.Count, 1);
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
            
            insertedRecordsFromDb.Items.ShouldBe<List<SdkEntity>>();
            Assert.AreEqual(insertedRecordsFromDb.Items.Count, 1);
        }
        
        [TestMethod]
        public async Task Can_find_with_references()
        {
            var client = new CodeMashClient(ApiKey, ProjectId);
            var repository = new CodeMashRepository<ReferencingEntity>(client);

            var referencedFields = new List<CollectionReferenceField>
            {
                new CollectionReferenceField
                {
                    Name = "singleref",
                },
                new CollectionReferenceField
                {
                    Name = "multiref",
                },
                new CollectionReferenceField
                {
                    Name = "singletaxref",
                },
                new CollectionReferenceField
                {
                    Name = "multitaxref",
                }
            };
            
            referencedFields[0].SetSort(Builders<ReferencedEntity>.Sort.Descending(x => x.Date));
            referencedFields[0].SetProjection(Builders<ReferencedEntity>.Projection.Include(x => x.Date).Include(x => x.Id));
            
            var response = await repository.FindAsync(findOptions: new DatabaseFindOptions
            {
                ReferencedFields = referencedFields
            });
        }
    }
}