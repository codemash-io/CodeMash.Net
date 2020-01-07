using System;
using System.Collections.Generic;
using System.Linq;
using CodeMash.Repository;
using Isidos.CodeMash.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class DatabaseUtilsTests
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
        public void Can_deserialize_translatable_with_culture()
        {
            var jsonSdkObject = new JObject(
                new JProperty("_id", "123"),
                new JProperty("nonTranslatable", "not translatable"),
                new JProperty("translatable", "translated")
            );

            var cultureCode = "en";
            var deserialized = JsonConverterHelper.DeserializeWithLowercase<TranslatableEntity>(jsonSdkObject.ToString(), cultureCode);
            
            Assert.AreEqual(deserialized.Id, "123");
            Assert.AreEqual(deserialized.NonTranslatable, "not translatable");
            Assert.AreEqual(deserialized.Translatable[cultureCode], "translated");
        }
        
        [TestMethod]
        public void Can_deserialize_translatable_without_culture()
        {
            var jsonSdkObject = new JObject(
                new JProperty("_id", "123"),
                new JProperty("nonTranslatable", "not translatable"),
                new JProperty("translatable", new JObject(
                    new JProperty("en", "en translated"), 
                    new JProperty("it", "it translated")))
            );

            var deserialized = JsonConverterHelper.DeserializeWithLowercase<TranslatableEntity>(jsonSdkObject.ToString(), null);
            
            Assert.AreEqual(deserialized.Id, "123");
            Assert.AreEqual(deserialized.NonTranslatable, "not translatable");
            Assert.AreEqual(deserialized.Translatable["en"], "en translated");
            Assert.AreEqual(deserialized.Translatable["it"], "it translated");
        }
        
        [TestMethod]
        public void Can_deserialize_translatable_with_culture_with_nested()
        {
            var jsonSdkObject = new JObject(
                new JProperty("_id", "123"),
                new JProperty("nonTranslatable", "not translatable"),
                new JProperty("nestedTranslatable", new JArray(
                    new JObject(
                        new JProperty("_id", "1"),
                        new JProperty("nonTranslatable", "not translatable"),
                        new JProperty("translatable", "translated")
                    )
                ))
            );

            var cultureCode = "en";
            var deserialized = JsonConverterHelper.DeserializeWithLowercase<TranslatableWithNestedEntity>(jsonSdkObject.ToString(), cultureCode);
            
            Assert.AreEqual(deserialized.Id, "123");
            Assert.AreEqual(deserialized.NonTranslatable, "not translatable");
            Assert.AreEqual(deserialized.NestedTranslatable[0].Translatable[cultureCode], "translated");
        }
        
        [TestMethod]
        public void Can_deserialize_translatable_without_culture_with_nested()
        {
            var jsonSdkObject = new JObject(
                new JProperty("_id", "123"),
                new JProperty("nonTranslatable", "not translatable"),
                new JProperty("nestedTranslatable", new JArray(
                    new JObject(
                        new JProperty("_id", "1"),
                        new JProperty("nonTranslatable", "not translatable"),
                        new JProperty("translatable", new JObject(
                            new JProperty("en", "en translated"), 
                            new JProperty("it", "it translated")))
                    )
                ))
            );

            var deserialized = JsonConverterHelper.DeserializeWithLowercase<TranslatableWithNestedEntity>(jsonSdkObject.ToString(), null);
            
            Assert.AreEqual(deserialized.Id, "123");
            Assert.AreEqual(deserialized.NonTranslatable, "not translatable");
            Assert.AreEqual(deserialized.NestedTranslatable[0].Translatable["en"], "en translated");
            Assert.AreEqual(deserialized.NestedTranslatable[0].Translatable["it"], "it translated");
        }
        
        [TestMethod]
        public void Can_deserialize_date_from_time_stamp()
        {
            var timeStamp = 1579719600000;
            
            var jsonSdkObject = new JObject(
                new JProperty("_id", "123"),
                new JProperty("dateTimeField", timeStamp)
            );

            var deserialized = JsonConverterHelper.DeserializeWithLowercase<DateTimeEntity>(jsonSdkObject.ToString(), null);
            
            Assert.AreEqual(deserialized.Id, "123");
            Assert.AreEqual(DateTimeHelpers.DateTimeToUnixTimestamp(deserialized.DateTimeField), timeStamp);
        }
        
        [TestMethod]
        public void Can_deserialize_date_from_time_stamp_with_nested()
        {
            var timeStamp1 = 1579719600000;
            var timeStamp2 = 1579719400000;
            
            var jsonSdkObject = new JObject(
                new JProperty("_id", "123"),
                new JProperty("nonNested", timeStamp1),
                new JProperty("nestedDateTime", new JArray(
                    new JObject(
                        new JProperty("_id", "1"),
                        new JProperty("dateTimeField", timeStamp2)
                    )
                ))
            );

            var deserialized = JsonConverterHelper.DeserializeWithLowercase<DateTimeWithNestedEntity>(jsonSdkObject.ToString(), null);
            
            Assert.AreEqual(deserialized.Id, "123");
            Assert.AreEqual(DateTimeHelpers.DateTimeToUnixTimestamp(deserialized.NonNested), timeStamp1);
            Assert.AreEqual(DateTimeHelpers.DateTimeToUnixTimestamp(deserialized.NestedDateTime[0].DateTimeField), timeStamp2);
        }
        
        [TestMethod]
        public void Can_serialize_date_to_time_stamp()
        {
            var timeStamp = 1579719600000;
            var dateObject = new DateTimeEntity
            {
                Id = "123",
                DateTimeField = DateTimeHelpers.DateTimeFromUnixTimestamp(timeStamp)
            };
            
            var serialized = JsonConverterHelper.SerializeWithLowercase(dateObject);
            var deserialized = JsonConverterHelper.DeserializeWithLowercase<DateTimeEntity>(serialized, null);
            
            Assert.AreEqual(deserialized.Id, "123");
            Assert.AreEqual(dateObject.DateTimeField, deserialized.DateTimeField);
        }
        
        
        [TestMethod]
        public void Can_serialize_date_to_time_stamp_with_nested()
        {
            var timeStamp1 = 1579719600000;
            var timeStamp2 = 1579719400000;
            var dateObject = new DateTimeWithNestedEntity
            {
                Id = "123",
                NonNested =  DateTimeHelpers.DateTimeFromUnixTimestamp(timeStamp1),
                NestedDateTime = new List<DateTimeEntity>
                {
                    new DateTimeEntity { DateTimeField = DateTimeHelpers.DateTimeFromUnixTimestamp(timeStamp2) }
                }
            };
            
            var serialized = JsonConverterHelper.SerializeWithLowercase(dateObject);
            var deserialized = JsonConverterHelper.DeserializeWithLowercase<DateTimeWithNestedEntity>(serialized, null);
            
            Assert.AreEqual(deserialized.Id, "123");
            Assert.AreEqual(dateObject.NonNested, deserialized.NonNested);
            Assert.AreEqual(dateObject.NestedDateTime[0].DateTimeField, deserialized.NestedDateTime[0].DateTimeField);
        }
        
        [TestMethod]
        public void Can_serialize_list()
        {
            var timeStamp = 1579719600000;
            var dateObjects = new List<DateTimeEntity>
            {
                new DateTimeEntity
                {
                    Id = "123",
                    DateTimeField = DateTimeHelpers.DateTimeFromUnixTimestamp(timeStamp)
                }
            };
            
            var serialized = JsonConverterHelper.SerializeWithLowercase(dateObjects);
            var deserialized = JsonConverterHelper.DeserializeWithLowercase<List<DateTimeEntity>>(serialized, null);
            
            Assert.AreEqual(deserialized[0].Id, "123");
            Assert.AreEqual(dateObjects[0].DateTimeField, deserialized[0].DateTimeField);
        }
        
        [TestMethod]
        public void Can_serialize_list_with_nested()
        {
            var timeStamp1 = 1579719600000;
            var timeStamp2 = 1579719400000;
            var dateObjects = new List<DateTimeWithNestedEntity>
            {
                new DateTimeWithNestedEntity
                {
                    Id = "123",
                    NonNested =  DateTimeHelpers.DateTimeFromUnixTimestamp(timeStamp1),
                    NestedDateTime = new List<DateTimeEntity>
                    {
                        new DateTimeEntity { DateTimeField = DateTimeHelpers.DateTimeFromUnixTimestamp(timeStamp2) }
                    }
                },
                new DateTimeWithNestedEntity
                {
                    Id = "1234",
                    NonNested =  DateTimeHelpers.DateTimeFromUnixTimestamp(timeStamp1),
                }
            };
            
            var serialized = JsonConverterHelper.SerializeWithLowercase(dateObjects);
            var deserialized = JsonConverterHelper.DeserializeWithLowercase<List<DateTimeWithNestedEntity>>(serialized, null);
            
            Assert.AreEqual(deserialized[0].Id, "123");
            Assert.AreEqual(dateObjects[0].NonNested, deserialized[0].NonNested);
            Assert.AreEqual(dateObjects[1].NonNested, deserialized[1].NonNested);
            Assert.AreEqual(dateObjects[0].NestedDateTime[0].DateTimeField, deserialized[0].NestedDateTime[0].DateTimeField);
        }
    }
}