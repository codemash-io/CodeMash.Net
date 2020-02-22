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
using AggregateOptions = CodeMash.Repository.AggregateOptions;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class AggregateTests : DatabaseTestBase
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
        public void Can_aggregate()
        {
            var aggregateId = Guid.Parse("466384cf-61a2-40dd-96fd-609a39a3fcd9");
            var result = Repository.Aggregate<SdkEntity>(aggregateId, new AggregateOptions { Tokens = new Dictionary<string, string> { { "nr", "1" } } });
        }
        
        [TestMethod]
        public async Task Can_aggregate_async()
        {
            var aggregateId = Guid.Parse("466384cf-61a2-40dd-96fd-609a39a3fcd9");
            var result = await Repository.AggregateAsync<SdkEntity>(aggregateId, new AggregateOptions { Tokens = new Dictionary<string, string> { { "nr", "1" } } });
        }
    }
}