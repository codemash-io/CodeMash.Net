using System.Collections.Generic;
using CodeMash.Models;
using CodeMash.Repository;
using Isidos.CodeMash.Utils;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class DatabaseUtilsReferencingTests
    {
        [TestInitialize]
        public void SetUp()
        {
        }
        
        [TestCleanup]
        public void TearDown()
        {
        }
        
        // --- Deserialized
        [TestMethod]
        public void Can_resolve_referenced_non_nested_entities()
        {
            var timeStamp1 = 1579719600000;
            var timeStamp2 = 1579719601000;
            var jsonSdkObject = new JObject(
                new JProperty("_id", "1"),
                new JProperty("text", "t1"),
                new JProperty("singleref", new JObject(
                    new JProperty("date", timeStamp1)    
                )),
                new JProperty("multiref", new JArray
                {
                    new JObject(new JProperty("date", timeStamp2))
                })
            );
            
            var deserialized = JsonConverterHelper.DeserializeEntity<ReferencingEntity>(jsonSdkObject.ToString(), null);
            
            Assert.AreEqual(deserialized.Text, "t1");
            Assert.AreEqual(deserialized.SingleRef.Date, DateTimeHelpers.DateTimeFromUnixTimestamp(timeStamp1));
            Assert.AreEqual(deserialized.MultiRef[0].Date, DateTimeHelpers.DateTimeFromUnixTimestamp(timeStamp2));
        }
        
        [TestMethod]
        public void Can_resolve_reference_id_when_not_referencing()
        {
            var jsonSdkObject = new JObject(
                new JProperty("_id", "1"),
                new JProperty("text", "t1"),
                new JProperty("singleref", "refId"),
                new JProperty("multiref", new JArray
                {
                    new JValue("mRefId")
                })
            );
            
            var deserialized = JsonConverterHelper.DeserializeEntity<ReferencingEntity>(jsonSdkObject.ToString(), null);
            
            Assert.AreEqual(deserialized.Id, "1");
            Assert.AreEqual(deserialized.SingleRef.Id, "refId");
            Assert.AreEqual(deserialized.MultiRef[0].Id, "mRefId");
        }
        
        [TestMethod]
        public void Can_resolve_referenced_non_nested_entities_with_nested_fields()
        {
            var timeStamp1 = 1579719600000;
            var timeStamp2 = 1579719601000;
            var jsonSdkObject = new JObject(
                new JProperty("_id", "1"),
                new JProperty("text", "t1"),
                new JProperty("singleref", new JObject(
                    new JProperty("date", timeStamp1),
                    new JProperty("nested", new JArray(
                        new JObject(new JProperty("number", 1), new JProperty("date", timeStamp1)),
                        new JObject(new JProperty("number", 2), new JProperty("date", timeStamp2))
                    ))
                )),
                new JProperty("multiref", new JArray
                {
                    new JObject(new JProperty("date", timeStamp2), new JProperty("nested", new JArray(
                        new JObject(new JProperty("number", 1), new JProperty("date", timeStamp1)),
                        new JObject(new JProperty("number", 2), new JProperty("date", timeStamp2))
                    )))
                })
            );
            
            var deserialized = JsonConverterHelper.DeserializeEntity<ReferencingEntity>(jsonSdkObject.ToString(), null);
            
            Assert.AreEqual(deserialized.Text, "t1");
            Assert.AreEqual(deserialized.SingleRef.Date, DateTimeHelpers.DateTimeFromUnixTimestamp(timeStamp1));
            Assert.AreEqual(deserialized.SingleRef.Nested[0].Number, 1);
            Assert.AreEqual(deserialized.SingleRef.Nested[0].Date, DateTimeHelpers.DateTimeFromUnixTimestamp(timeStamp1));
            Assert.AreEqual(deserialized.SingleRef.Nested[1].Number, 2);
            Assert.AreEqual(deserialized.SingleRef.Nested[1].Date, DateTimeHelpers.DateTimeFromUnixTimestamp(timeStamp2));
            Assert.AreEqual(deserialized.MultiRef[0].Date, DateTimeHelpers.DateTimeFromUnixTimestamp(timeStamp2));
            Assert.AreEqual(deserialized.MultiRef[0].Nested[0].Number, 1);
            Assert.AreEqual(deserialized.MultiRef[0].Nested[1].Number, 2);
            Assert.AreEqual(deserialized.MultiRef[0].Nested[0].Date, DateTimeHelpers.DateTimeFromUnixTimestamp(timeStamp1));
            Assert.AreEqual(deserialized.MultiRef[0].Nested[1].Date, DateTimeHelpers.DateTimeFromUnixTimestamp(timeStamp2));
        }
        
        [TestMethod]
        public void Can_resolve_referenced_nested_entities()
        {
            var timeStamp1 = 1579719600000;
            var timeStamp2 = 1579719601000;
            var jsonSdkObject = new JObject(
                new JProperty("_id", "1"),
                new JProperty("nested", new JArray(
                    new JObject(
                        new JProperty("singleref", 
                            new JObject(new JProperty("date", timeStamp1))
                        ),
                        new JProperty("multiref", 
                            new JArray(
                                new JObject(new JProperty("date", timeStamp2))    
                            )
                        ))
                ))
            );
            
            var deserialized = JsonConverterHelper.DeserializeEntity<ReferencingFromNestedEntity>(jsonSdkObject.ToString(), null);
            
            Assert.AreEqual(deserialized.Nested[0].NestedSingleRef.Date, DateTimeHelpers.DateTimeFromUnixTimestamp(timeStamp1));
            Assert.AreEqual(deserialized.Nested[0].NestedMultiRef[0].Date, DateTimeHelpers.DateTimeFromUnixTimestamp(timeStamp2));
        }
        
        [TestMethod]
        public void Can_resolve_reference_id_in_nested_when_not_referencing()
        {
            var jsonSdkObject = new JObject(
                new JProperty("_id", "1"),
                new JProperty("nested", new JArray(
                    new JObject(
                        new JProperty("singleref", "refId"),
                        new JProperty("multiref", 
                            new JArray(
                                new JValue("mRefId")
                            )
                        )
                    ))
                )
            );
            
            var deserialized = JsonConverterHelper.DeserializeEntity<ReferencingFromNestedEntity>(jsonSdkObject.ToString(), null);
            
            Assert.AreEqual(deserialized.Id, "1");
            Assert.AreEqual(deserialized.Nested[0].NestedSingleRef.Id, "refId");
            Assert.AreEqual(deserialized.Nested[0].NestedMultiRef[0].Id, "mRefId");
        }
        
        // --- Serialized
        [TestMethod]
        public void Can_serialize_referenced_non_nested_entities()
        {
            var timeStamp1 = 1579719600000;
            var entity = new ReferencingEntity
            {
                Id = "1",
                Date = DateTimeHelpers.DateTimeFromUnixTimestamp(timeStamp1),
                SingleRef = new ReferencedEntity { Id = "2" },
                MultiRef = new List<ReferencedEntity>
                {
                    new ReferencedEntity { Id = "3" },
                    new ReferencedEntity { Id = "4" },
                }
            };
            
            var serialized = JsonConverterHelper.SerializeEntity(entity);
            
            var deserialized = JsonConverterHelper.DeserializeEntity<ReferencingEntity>(serialized, null);
            var jObject = JObject.Parse(serialized);

            Assert.AreEqual(deserialized.Id, entity.Id);
            Assert.AreEqual(deserialized.Date, entity.Date);
            
            Assert.AreEqual(deserialized.SingleRef.Id, entity.SingleRef.Id);
            Assert.AreEqual(jObject["singleref"], "2");
            
            Assert.AreEqual(deserialized.MultiRef[0].Id, deserialized.MultiRef[0].Id);
            Assert.AreEqual(deserialized.MultiRef[1].Id, deserialized.MultiRef[1].Id);
            Assert.AreEqual(jObject["multiref"][0], "3");
            Assert.AreEqual(jObject["multiref"][1], "4");
        }
        
        [TestMethod]
        public void Can_serialize_referenced_nested_entities()
        {
            var entity = new ReferencingFromNestedEntity
            {
                Id = "1",
                Nested = new List<NestedWithReference>
                {
                    new NestedWithReference
                    {
                        NestedSingleRef = new ReferencingEntity
                        {
                            Id = "2",
                        },
                        NestedMultiRef = new List<ReferencingEntity>
                        {
                            new ReferencingEntity
                            {
                                Id = "3",
                            },
                            new ReferencingEntity
                            {
                                Id = "4",
                            }
                        }
                    },
                    new NestedWithReference
                    {
                        NestedSingleRef = new ReferencingEntity
                        {
                            Id = "5",
                        },
                    }
                }
            };
            
            var serialized = JsonConverterHelper.SerializeEntity(entity);
            
            var deserialized = JsonConverterHelper.DeserializeEntity<ReferencingFromNestedEntity>(serialized, null);
            var jObject = JObject.Parse(serialized);

            Assert.AreEqual(deserialized.Id, entity.Id);
            Assert.AreEqual(deserialized.Nested[0].NestedSingleRef.Id, deserialized.Nested[0].NestedSingleRef.Id);
            Assert.AreEqual(deserialized.Nested[1].NestedSingleRef.Id, deserialized.Nested[1].NestedSingleRef.Id);
            Assert.AreEqual(jObject["nested"][0]["singleref"], "2");
            Assert.AreEqual(jObject["nested"][1]["singleref"], "5");
            
            Assert.AreEqual(deserialized.Nested[0].NestedMultiRef[0].Id, deserialized.Nested[0].NestedMultiRef[0].Id);
            Assert.AreEqual(deserialized.Nested[0].NestedMultiRef[1].Id, deserialized.Nested[0].NestedMultiRef[1].Id);
            Assert.AreEqual(jObject["nested"][0]["multiref"][0], "3");
            Assert.AreEqual(jObject["nested"][0]["multiref"][1], "4");
        }
        
        [TestMethod]
        public void Can_serialize_taxonomy_entities()
        {
            var entity = new ReferencingEntity
            {
                Id = "1",
                SingleTaxRef = new TermEntity<ReferencedTermMeta> { Id = "2" },
                MultiTaxRef = new List<TermEntity<ReferencedTermMeta>>
                {
                    new TermEntity<ReferencedTermMeta> { Id = "3" },
                    new TermEntity<ReferencedTermMeta> { Id = "4" },
                }
            };
            
            var serialized = JsonConverterHelper.SerializeEntity(entity);
            
            var deserialized = JsonConverterHelper.DeserializeEntity<ReferencingEntity>(serialized, null);
            var jObject = JObject.Parse(serialized);

            Assert.AreEqual(deserialized.Id, entity.Id);
            Assert.AreEqual(deserialized.SingleTaxRef.Id, entity.SingleTaxRef.Id);
            Assert.AreEqual(jObject["singletaxref"], "2");
            
            Assert.AreEqual(deserialized.MultiTaxRef[0].Id, deserialized.MultiTaxRef[0].Id);
            Assert.AreEqual(deserialized.MultiTaxRef[1].Id, deserialized.MultiTaxRef[1].Id);
            Assert.AreEqual(jObject["multitaxref"][0], "3");
            Assert.AreEqual(jObject["multitaxref"][1], "4");
        }
        
        [TestMethod]
        public void Can_serialize_file_entities()
        {
            var entity = new ReferencingEntity
            {
                Id = "1",
                Files = new List<FileEntity>
                {
                    new FileEntity
                    {
                        Id = "2",
                        FileName = "test.tx"
                    }
                }
            };
            
            var serialized = JsonConverterHelper.SerializeEntity(entity);
            
            var deserialized = JsonConverterHelper.DeserializeEntity<ReferencingEntity>(serialized, null);
            var jObject = JObject.Parse(serialized);

            Assert.AreEqual(deserialized.Files[0].Id, entity.Files[0].Id);
            Assert.AreEqual(jObject["files"][0], "2");
        }
    }
}