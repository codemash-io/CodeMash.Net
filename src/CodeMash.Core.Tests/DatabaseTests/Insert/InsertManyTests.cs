using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeMash.Client;
using CodeMash.Models.Exceptions;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class InsertManyTests : DatabaseTestBase
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
        public void Can_insert_many()
        {
            var entity1 = new SdkEntity { Notes = "1", Number = 11 };
            var entity2 = new SdkEntity { Notes = "2", Number = 22 };
            
            var insertedIds = Repository.InsertMany(new List<SdkEntity> { entity1, entity2 });
            
            Assert.AreEqual(insertedIds.Count, 2);
            Assert.AreEqual(insertedIds[0], entity1.Id);
            Assert.AreEqual(insertedIds[1], entity2.Id);
            
            Repository.DeleteOne(x => x.Id == entity1.Id);
            Repository.DeleteOne(x => x.Id == entity2.Id);
        }
    }
}