using System;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class FindOneTests 
    {        
        [TestMethod]
        public void Can_find_one_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.json");

            var train = recipesRepository.FindOne<Schedule>(x => x.Destination == "Trakai");
            
            train.ShouldBe<Schedule>();
            Assert.IsNotNull(train);
            Assert.AreEqual(train.Destination, "Trakai");
        }

        [TestMethod]
        public void Can_find_one_value_in_number_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.json");

            var train = recipesRepository.FindOne<Schedule>(x => x.Number == 1454);
            
            train.ShouldBe<Schedule>();
            Assert.IsNotNull(train);
            Assert.AreEqual(train.Id, "5d0782db33ab560001f28fb8");
        }

        [TestMethod]
        public void Exception_find_one_with_no_filter_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.json");

            Assert.ThrowsException<ArgumentNullException>( () => recipesRepository.FindOne<Schedule>(null) );
        }

        [TestMethod]
        public void Can_find_one_with_id_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.json");

            // TODO : change it later. Either we need setup tests and seed with data first or Insert and get data upfront of each test execution.
            var train = recipesRepository.FindOne<Schedule>(x => x.Id == "5d07829733ab560001f28f91");
            
            train.ShouldBe<Schedule>();
            Assert.IsNotNull(train);
            Assert.AreEqual(train.Origin, "Kaunas");
            Assert.AreEqual(train.Destination, "Vilnius");
        }

        [TestMethod]
        public void Can_find_one_first_with_origin_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.json");

            var train = recipesRepository.FindOne<Schedule>(x => x.Origin == "Kaunas");
            
            train.ShouldBe<Schedule>();
            Assert.IsNotNull(train);
            Assert.AreEqual(train.Origin, "Kaunas");
            Assert.AreEqual(train.Destination, "Vilnius");
        }
    }
}