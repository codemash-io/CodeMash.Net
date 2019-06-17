using System;
using System.Collections.Generic;
using CodeMash.Repository;
using NUnit.Framework;

namespace CodeMash.Core.Tests
{
    [TestFixture]
    public class FindTests
    {
        // TODO : add setup which runs before each test
        // TODO : add all possible fields (Selections, Taxonomies, Files, Translatable fields)
        // TODO : play with projections
        // TODO : play with paging and sorting
        // TODO : play with cultures and translatable fields. 
        // 
        
        
        
        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("Find")]
        public void Can_find_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.json");

            var trains = recipesRepository.Find<Schedule>(x => true);
            
            Assert.IsInstanceOf<List<Schedule>>(trains);
            Assert.IsNotNull(trains);
            Assert.AreEqual(4, trains.Count);
        }

        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("Find")]
        public void Can_find_value_in_origin_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.json");

            var trains = recipesRepository.Find<Schedule>(x => x.Origin == "Kaunas");
            
            Assert.IsInstanceOf<List<Schedule>>(trains);
            Assert.IsNotNull(trains);
            Assert.AreEqual(2, trains.Count);
        }

        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("Find")]
        public void Can_find_value_in_origin_and_destination_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.json");

            var trains = recipesRepository.Find<Schedule>(x => x.Origin == "Kaunas" || x.Destination == "Kaunas");
            
            Assert.IsInstanceOf<List<Schedule>>(trains);
            Assert.IsNotNull(trains);
            Assert.AreEqual(3, trains.Count);
        }

        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("Find")]
        public void Can_find_with_no_filter_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.json");

            var trains = recipesRepository.Find<Schedule>(null);
            
            Assert.IsInstanceOf<List<Schedule>>(trains);
            Assert.IsNotNull(trains);
            Assert.AreEqual(4, trains.Count);
        }

        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("Find")]
        public void Can_find_with_id_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.json");

            var trains = recipesRepository.Find<Schedule>(x => x.Id == "5d07829733ab560001f28f91");
            
            Assert.IsInstanceOf<List<Schedule>>(trains);
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
            var recipesRepository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.json");

            var trains = recipesRepository.Find<Schedule>(x => true, null, null, 2, null);
            
            Assert.IsInstanceOf<List<Schedule>>(trains);
            Assert.IsNotNull(trains);
            Assert.AreEqual(2, trains.Count);
        }

        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("Find")]
        public void Can_find_with_limit_2_and_skip_1_page_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.json");

            var trains = recipesRepository.Find<Schedule>(x => true, null, 1, 2, null);
            
            Assert.IsInstanceOf<List<Schedule>>(trains);
            Assert.IsNotNull(trains);
            Assert.AreEqual(2, trains.Count);
            Assert.True(Convert.ToInt32(trains[0].Number) > 200);
            Assert.True(Convert.ToInt32(trains[1].Number) > 200);
        }
    }
}