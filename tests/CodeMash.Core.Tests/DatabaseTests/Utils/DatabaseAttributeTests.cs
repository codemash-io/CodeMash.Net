using System.Collections.Generic;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;

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
        
        [TestMethod]
        public void Can_serialize_and_deserialize_with_attribute_name()
        {
            var entity = new List<AttributeEntity>
            {
                new AttributeEntity
                {
                    Id = "123",
                    Attribute1 = "test",
                    Attribute2 = 2,
                    Attribute3 = 3,
                    attribute_4 = 4
                }
            };
            
            var serialized = JsonConverterHelper.SerializeEntity(entity);
            Assert.IsTrue(serialized.Contains("field_1"));
            Assert.IsTrue(serialized.Contains("field_2"));
            Assert.IsTrue(serialized.Contains("attribute3"));
            Assert.IsTrue(serialized.Contains("attribute_4"));
            
            var deserialized = JsonConverterHelper.DeserializeEntity<List<AttributeEntity>>(serialized, null);
            
            Assert.AreEqual(deserialized[0].Id, "123");
            Assert.AreEqual(entity[0].Attribute1, deserialized[0].Attribute1);
            Assert.AreEqual(entity[0].Attribute2, deserialized[0].Attribute2);
            Assert.AreEqual(entity[0].Attribute3, deserialized[0].Attribute3);
            Assert.AreEqual(entity[0].attribute_4, deserialized[0].attribute_4);
        }
    }
}