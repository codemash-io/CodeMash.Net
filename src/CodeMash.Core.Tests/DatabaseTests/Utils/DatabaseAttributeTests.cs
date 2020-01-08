using System;
using System.Collections.Generic;
using System.Linq;
using CodeMash.Repository;
using Isidos.CodeMash.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using Newtonsoft.Json.Linq;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class DatabaseAttributeTests
    {
        [TestInitialize]
        public void SetUp()
        {
        }
        
        [TestCleanup]
        public void TearDown()
        {
        }
        
        [TestMethod]
        public void Can_serialize_filter_with_unique_names()
        {
            var filter = new ExpressionFilterDefinition<AttributeEntity>(x => x.Attribute1 == "test");
            var filterString = filter.FilterToJson();
            
            Assert.IsTrue(filterString.Contains("field_1"));
        }
        
        [TestMethod]
        public void Can_serialize_sort_with_unique_names()
        {
            var sort = Builders<AttributeEntity>.Sort.Ascending(x => x.Attribute1);
            var sortString = sort.SortToJson();
            
            Assert.IsTrue(sortString.Contains("field_1"));
        }
        
        [TestMethod]
        public void Can_serialize_projection_with_unique_names()
        {
            var projection = Builders<AttributeEntity>.Projection.Include(x => x.Attribute1);
            var projectionString = projection.ProjectionToJson();
            
            Assert.IsTrue(projectionString.Contains("field_1"));
        }
    }
}