using System;
using CodeMash.Repository;
using NUnit.Framework;

namespace CodeMash.Core.Tests
{
    [TestFixture]
    public class FindOneTests 
    {        
        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("FindOne")]
        public void Can_find_one_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.json");

            var train = recipesRepository.FindOne<Schedule>(x => x.Destination == "Trakai");
            
            Assert.IsInstanceOf<Schedule>(train);
            Assert.IsNotNull(train);
            Assert.AreEqual(train.Destination, "Trakai");
        }

        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("FindOne")]
        public void Can_find_one_value_in_number_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.json");

            var train = recipesRepository.FindOne<Schedule>(x => x.Number == 1454);
            
            Assert.IsInstanceOf<Schedule>(train);
            Assert.IsNotNull(train);
            Assert.AreEqual(train.Id, "5d0782db33ab560001f28fb8");
        }

        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("FindOneOne")]
        public void Exception_find_one_with_no_filter_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.json");

            Assert.Throws<ArgumentNullException>( () => recipesRepository.FindOne<Schedule>(null) );
        }

        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("FindOne")]
        public void Can_find_one_with_id_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.json");

            // TODO : change it later. Either we need setup tests and seed with data first or Insert and get data upfront of each test execution.
            var train = recipesRepository.FindOne<Schedule>(x => x.Id == "5d07829733ab560001f28f91");
            
            Assert.IsInstanceOf<Schedule>(train);
            Assert.IsNotNull(train);
            Assert.AreEqual(train.Origin, "Kaunas");
            Assert.AreEqual(train.Destination, "Vilnius");
        }

        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("FindOne")]
        public void Can_find_one_first_with_origin_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.json");

            var train = recipesRepository.FindOne<Schedule>(x => x.Origin == "Kaunas");
            
            Assert.IsInstanceOf<Schedule>(train);
            Assert.IsNotNull(train);
            Assert.AreEqual(train.Origin, "Kaunas");
            Assert.AreEqual(train.Destination, "Vilnius");
        }
    }
}