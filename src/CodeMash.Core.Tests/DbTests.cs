using System;
using System.Collections.Generic;
using CodeMash.Interfaces;
using CodeMash.Repository;
using MongoDB.Bson.Serialization.Attributes;
using NUnit.Framework;

namespace CodeMash.Core.Tests
{
    [TestFixture]
    public class DbTests
    {
        [CollectionName("trains")]
        public class Train : Entity, IEntity
        {
            [BsonElement("number")]
            public string Number { get; set; }
            [BsonElement("origin")]
            public string Origin { get; set; }
            [BsonElement("destination")]
            public string Destination { get; set; }
        }

        // 15 Kaunas Vilnius
        // 140 Vilnius Kaunas
        // 410 Kaunas Klaipeda
        // 1454 Vilnius Trakai
        
        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("Find")]
        public void Can_find_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Train>("appsettings.Production.json");

            var trains = recipesRepository.Find<Train>(x => true);
            
            Assert.IsInstanceOf<List<Train>>(trains);
            Assert.IsNotNull(trains);
            Assert.AreEqual(4, trains.Count);
        }

        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("Find")]
        public void Can_find_value_in_origin_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Train>("appsettings.Production.json");

            var trains = recipesRepository.Find<Train>(x => x.Origin == "Kaunas");
            
            Assert.IsInstanceOf<List<Train>>(trains);
            Assert.IsNotNull(trains);
            Assert.AreEqual(2, trains.Count);
        }

        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("Find")]
        public void Can_find_value_in_origin_and_destination_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Train>("appsettings.Production.json");

            var trains = recipesRepository.Find<Train>(x => x.Origin == "Kaunas" || x.Destination == "Kaunas");
            
            Assert.IsInstanceOf<List<Train>>(trains);
            Assert.IsNotNull(trains);
            Assert.AreEqual(3, trains.Count);
        }

        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("Find")]
        public void Can_find_with_no_filter_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Train>("appsettings.Production.json");

            var trains = recipesRepository.Find<Train>(null);
            
            Assert.IsInstanceOf<List<Train>>(trains);
            Assert.IsNotNull(trains);
            Assert.AreEqual(4, trains.Count);
        }

        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("Find")]
        public void Can_find_with_id_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Train>("appsettings.Production.json");

            var trains = recipesRepository.Find<Train>(x => x.Id == "5d0236ba23ef8d00013fb409");
            
            Assert.IsInstanceOf<List<Train>>(trains);
            Assert.IsNotNull(trains);
            Assert.AreEqual(trains[0].Origin, "Kaunas");
            Assert.AreEqual(trains[0].Destination, "Vilnius");
        }

        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("Find")]
        public void Can_find_with_limit_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Train>("appsettings.Production.json");

            var trains = recipesRepository.Find<Train>(x => true, null, null, 2, null);
            
            Assert.IsInstanceOf<List<Train>>(trains);
            Assert.IsNotNull(trains);
            Assert.AreEqual(2, trains.Count);
        }

        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("Find")]
        public void Can_find_with_limit_2_and_skip_1_page_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Train>("appsettings.Production.json");

            var trains = recipesRepository.Find<Train>(x => true, null, 1, 2, null);
            
            Assert.IsInstanceOf<List<Train>>(trains);
            Assert.IsNotNull(trains);
            Assert.AreEqual(2, trains.Count);
            Assert.True(Convert.ToInt32(trains[0].Number) > 200);
            Assert.True(Convert.ToInt32(trains[1].Number) > 200);
        }
    }
}