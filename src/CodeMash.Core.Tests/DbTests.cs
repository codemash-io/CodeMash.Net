using System.Collections.Generic;
using CodeMash.Interfaces;
using CodeMash.Repository;
using MongoDB.Bson.Serialization.Attributes;
using NSubstitute;
using NUnit.Framework;

namespace CodeMash.Core.Tests
{
    [TestFixture]
    public class DbTests
    {
        [CollectionName("recipes")]
        public class Recipe : Entity, IEntity
        {
            [BsonElement("title")]
            public Translatable Name { get; set; }
        }
        
        [Test]
        [Category("Db")]
        [Category("Integration")]
        public void Can_find_integration_test()
        {

            var recipesRepository = CodeMashRepositoryFactory.Create<Recipe>("appsettings.Production.json");

            var recipes = recipesRepository.Find<Recipe>(x => true);
            

            Assert.IsInstanceOf<List<Recipe>>(recipes);
            Assert.IsNotNull(recipes);

        }
    }
}