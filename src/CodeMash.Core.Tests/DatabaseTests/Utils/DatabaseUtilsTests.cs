using CodeMash.Repository;
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
    }
}