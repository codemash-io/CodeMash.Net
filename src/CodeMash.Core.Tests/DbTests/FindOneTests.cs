using System;
using System.Collections.Generic;
using CodeMash.Repository;
using NUnit.Framework;

namespace CodeMash.Core.Tests
{
    [TestFixture]
    public class FindOneTests : DbTestBase
    {        
        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("FindOne")]
        public void Can_find_one_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Train>("appsettings.Production.json");

            var train = recipesRepository.FindOne<Train>(x => x.Destination == "Trakai");
            
            Assert.IsInstanceOf<Train>(train);
            Assert.IsNotNull(train);
            Assert.AreEqual(train.Destination, "Trakai");
        }

        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("FindOne")]
        public void Can_find_one_value_in_number_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Train>("appsettings.Production.json");

            var train = recipesRepository.FindOne<Train>(x => x.Number == "1454");
            
            Assert.IsInstanceOf<Train>(train);
            Assert.IsNotNull(train);
            Assert.AreEqual(train.Id, "5d0236f623ef8d00013fb462");
        }

        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("FindOneOne")]
        public void Exception_find_one_with_no_filter_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Train>("appsettings.Production.json");

            Assert.Throws<ArgumentNullException>( () => recipesRepository.FindOne<Train>(null) );
        }

        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("FindOne")]
        public void Can_find_one_with_id_integration_test()
        {
            var recipesRepository = CodeMashRepositoryFactory.Create<Train>("appsettings.Production.json");

            var train = recipesRepository.FindOne<Train>(x => x.Id == "5d0236ba23ef8d00013fb409");
            
            Assert.IsInstanceOf<Train>(train);
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
            var recipesRepository = CodeMashRepositoryFactory.Create<Train>("appsettings.Production.json");

            var train = recipesRepository.FindOne<Train>(x => x.Origin == "Kaunas");
            
            Assert.IsInstanceOf<Train>(train);
            Assert.IsNotNull(train);
            Assert.AreEqual(train.Origin, "Kaunas");
            Assert.AreEqual(train.Destination, "Vilnius");
        }
    }
}