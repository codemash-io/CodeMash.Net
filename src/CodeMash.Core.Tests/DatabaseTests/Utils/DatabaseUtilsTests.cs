using System;
using System.Collections.Generic;
using System.Linq;
using CodeMash.Repository;
using Isidos.CodeMash.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.Driver;
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
                new JProperty("nontranslatable", "not translatable"),
                new JProperty("translatable", "translated")
            );

            var cultureCode = "en";
            var deserialized = JsonConverterHelper.DeserializeEntity<TranslatableEntity>(jsonSdkObject.ToString(), cultureCode);
            
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

            var deserialized = JsonConverterHelper.DeserializeEntity<TranslatableEntity>(jsonSdkObject.ToString(), null);
            
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
                new JProperty("nontranslatable", "not translatable"),
                new JProperty("nestedtranslatable", new JArray(
                    new JObject(
                        new JProperty("_id", "1"),
                        new JProperty("nontranslatable", "not translatable"),
                        new JProperty("translatable", "translated")
                    )
                ))
            );

            var cultureCode = "en";
            var deserialized = JsonConverterHelper.DeserializeEntity<TranslatableWithNestedEntity>(jsonSdkObject.ToString(), cultureCode);
            
            Assert.AreEqual(deserialized.Id, "123");
            Assert.AreEqual(deserialized.NonTranslatable, "not translatable");
            Assert.AreEqual(deserialized.NestedTranslatable[0].Translatable[cultureCode], "translated");
        }
        
        [TestMethod]
        public void Can_deserialize_translatable_without_culture_with_nested()
        {
            var jsonSdkObject = new JObject(
                new JProperty("_id", "123"),
                new JProperty("nontranslatable", "not translatable"),
                new JProperty("nestedtranslatable", new JArray(
                    new JObject(
                        new JProperty("_id", "1"),
                        new JProperty("nontranslatable", "not translatable"),
                        new JProperty("translatable", new JObject(
                            new JProperty("en", "en translated"), 
                            new JProperty("it", "it translated")))
                    )
                ))
            );

            var deserialized = JsonConverterHelper.DeserializeEntity<TranslatableWithNestedEntity>(jsonSdkObject.ToString(), null);
            
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
                new JProperty("datetimefield", timeStamp)
            );

            var deserialized = JsonConverterHelper.DeserializeEntity<DateTimeEntity>(jsonSdkObject.ToString(), null);
            
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
                new JProperty("nonnested", timeStamp1),
                new JProperty("nesteddatetime", new JArray(
                    new JObject(
                        new JProperty("_id", "1"),
                        new JProperty("datetimefield", timeStamp2)
                    )
                ))
            );

            var deserialized = JsonConverterHelper.DeserializeEntity<DateTimeWithNestedEntity>(jsonSdkObject.ToString(), null);
            
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
            
            var serialized = JsonConverterHelper.SerializeEntity(dateObject);
            var deserialized = JsonConverterHelper.DeserializeEntity<DateTimeEntity>(serialized, null);
            
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
                NestedDateTime = new List<DateTimeNonEntity>
                {
                    new DateTimeNonEntity { DateTimeField = DateTimeHelpers.DateTimeFromUnixTimestamp(timeStamp2) }
                }
            };
            
            var serialized = JsonConverterHelper.SerializeEntity(dateObject);
            var deserialized = JsonConverterHelper.DeserializeEntity<DateTimeWithNestedEntity>(serialized, null);
            
            Assert.AreEqual(deserialized.Id, "123");
            Assert.AreEqual(dateObject.NonNested, deserialized.NonNested);
            Assert.AreEqual(dateObject.NestedDateTime[0].DateTimeField, deserialized.NestedDateTime[0].DateTimeField);
        }
        
        [TestMethod]
        public void Can_serialize_currency_object_and_rename()
        {
            var dateObject = new CurrencyEntity
            {
                Id = "123",
                Currency = new CurrencyField
                {
                    Currency = "EUR",
                    Value = 10m
                },
                Currencies = new List<CurrencyField>
                {
                    new CurrencyField { Currency = "EUR", Value = 5m }
                }
            };
            
            var serialized = JsonConverterHelper.SerializeEntity(dateObject);
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
            
            var serialized = JsonConverterHelper.SerializeEntity(dateObjects);
            var deserialized = JsonConverterHelper.DeserializeEntity<List<DateTimeEntity>>(serialized, null);
            
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
                    NestedDateTime = new List<DateTimeNonEntity>
                    {
                        new DateTimeNonEntity { DateTimeField = DateTimeHelpers.DateTimeFromUnixTimestamp(timeStamp2) }
                    }
                },
                new DateTimeWithNestedEntity
                {
                    Id = "1234",
                    NonNested =  DateTimeHelpers.DateTimeFromUnixTimestamp(timeStamp1),
                }
            };
            
            var serialized = JsonConverterHelper.SerializeEntity(dateObjects);
            var deserialized = JsonConverterHelper.DeserializeEntity<List<DateTimeWithNestedEntity>>(serialized, null);
            
            Assert.AreEqual(deserialized[0].Id, "123");
            Assert.AreEqual(dateObjects[0].NonNested, deserialized[0].NonNested);
            Assert.AreEqual(dateObjects[1].NonNested, deserialized[1].NonNested);
            Assert.AreEqual(dateObjects[0].NestedDateTime[0].DateTimeField, deserialized[0].NestedDateTime[0].DateTimeField);
        }
        
        [TestMethod]
        public void Can_serialize_date_in_filter()
        {
            var dateFilter = Builders<DateTimeEntity>.Filter.And(
                Builders<DateTimeEntity>.Filter.Gt(x => x.DateTimeField, DateTime.Now),
                Builders<DateTimeEntity>.Filter.Lte(x => x.DateTimeField, DateTime.Now.AddDays(1))
            );
            var parsedFilter = dateFilter.FilterToJson();
            
            Assert.IsTrue(parsedFilter.Contains("NumberLong"));
            Assert.IsTrue(!parsedFilter.Contains("ISODate"));
        }
        
        [TestMethod]
        public void Can_serialize_date_in_update()
        {
            var updateFilter = Builders<DateTimeWithNestedEntity>.Update
                .Set(x => x.NonNested, DateTime.Now)
                .Set(x => x.NestedDateTime[0].DateTimeField, DateTime.Now);
            var parsedUpdate = updateFilter.UpdateToJson();
            
            Assert.IsTrue(parsedUpdate.Contains("NumberLong"));
            Assert.IsTrue(!parsedUpdate.Contains("ISODate"));
        }
        
        [TestMethod]
        public void Can_serialize_date_in_update_from_any_kind()
        {
            var unspecified = new DateTime(2017, 8, 12);
            var local = DateTime.SpecifyKind(unspecified, DateTimeKind.Local);
            var utc = DateTime.SpecifyKind(unspecified, DateTimeKind.Utc);
            
            var updateFilter = Builders<DateTimeWithNestedEntity>.Update
                .Set(x => x.NonNested, unspecified)
                .Set(x => x.NonNested2, utc)
                .Set(x => x.NestedDateTime[0].DateTimeField, local);
            var parsedUpdate = updateFilter.UpdateToJson();
            
            Assert.IsTrue(parsedUpdate.Contains("NumberLong"));
            Assert.IsTrue(!parsedUpdate.Contains("ISODate"));
        }
        
        [TestMethod]
        public void Can_deserialize_aggregate()
        {
            var timeStamp1 = 1579719600000;
            var dateTime = DateTimeHelpers.DateTimeFromUnixTimestamp(timeStamp1);
            var jsonSdkObject = new JObject(
                new JProperty("field_1", "f1"),
                new JProperty("field_2", "f2"),
                new JProperty("single", new JObject(
                        new JProperty("d", timeStamp1),
                        new JProperty("t", "t1"),
                        new JProperty("n", new JObject(
                            new JProperty("d", timeStamp1),
                            new JProperty("t", "t2"),
                            new JProperty("n", new JObject(
                                new JProperty("d", timeStamp1),
                                new JProperty("t", "t3")
                            ))
                        ))
                    )),
                new JProperty("multi", new JArray(
                    new JObject(
                        new JProperty("d", timeStamp1),
                        new JProperty("t", "t1"),
                        new JProperty("n", new JObject(
                            new JProperty("d", timeStamp1),
                            new JProperty("t", "t2"),
                            new JProperty("n", new JObject(
                                new JProperty("d", timeStamp1),
                                new JProperty("t", "t3")
                            ))
                        ))
                    ),
                    new JObject(
                        new JProperty("d", timeStamp1),
                        new JProperty("t", "t1"),
                        new JProperty("n", new JObject(
                            new JProperty("d", timeStamp1),
                            new JProperty("t", "t2"),
                            new JProperty("n", new JObject(
                                new JProperty("d", timeStamp1),
                                new JProperty("t", "t3")
                            ))
                        ))
                    )
                ))
            );
            
            var jAggregateArray = new JArray { jsonSdkObject };
            var deserialized = JsonConverterHelper.DeserializeAggregate<List<AggregateProjection>>(jAggregateArray.ToString());
            
            Assert.AreEqual(deserialized[0].Attribute1, "f1");
            Assert.AreEqual(deserialized[0].Attribute2, "f2");
            
            Assert.AreEqual(deserialized[0].Single.Text, "t1");
            Assert.AreEqual(deserialized[0].Single.Date, dateTime);
            Assert.AreEqual(deserialized[0].Single.Nest.Text, "t2");
            Assert.AreEqual(deserialized[0].Single.Nest.Date, dateTime);
            Assert.AreEqual(deserialized[0].Single.Nest.Nest.Text, "t3");
            Assert.AreEqual(deserialized[0].Single.Nest.Nest.Date, dateTime);
            
            Assert.AreEqual(deserialized[0].Multi[0].Text, "t1");
            Assert.AreEqual(deserialized[0].Multi[0].Date, dateTime);
            Assert.AreEqual(deserialized[0].Multi[0].Nest.Text, "t2");
            Assert.AreEqual(deserialized[0].Multi[0].Nest.Date, dateTime);
            Assert.AreEqual(deserialized[0].Multi[0].Nest.Nest.Text, "t3");
            Assert.AreEqual(deserialized[0].Multi[0].Nest.Nest.Date, dateTime);
            
            Assert.AreEqual(deserialized[0].Multi[1].Text, "t1");
            Assert.AreEqual(deserialized[0].Multi[1].Date, dateTime);
            Assert.AreEqual(deserialized[0].Multi[1].Nest.Text, "t2");
            Assert.AreEqual(deserialized[0].Multi[1].Nest.Date, dateTime);
            Assert.AreEqual(deserialized[0].Multi[1].Nest.Nest.Text, "t3");
            Assert.AreEqual(deserialized[0].Multi[1].Nest.Nest.Date, dateTime);
        }
    }
}