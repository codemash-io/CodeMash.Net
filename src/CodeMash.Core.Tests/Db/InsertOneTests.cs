using System;
using System.Collections.Generic;
using CodeMash.Repository;
using NUnit.Framework;

namespace CodeMash.Core.Tests
{
    [TestFixture]
    public class InsertOneTests
    {
        // TODO : add setup which runs before each test
        // TODO : add all possible fields (Selections, Taxonomies, Files, Translatable fields)
        // TODO : play with projections
        // TODO : play with paging and sorting
        // TODO : play with cultures and translatable fields. 

        private IRepository<Schedule> Repository { get; set; }
        
        [SetUp]
        public void SetUp()
        {
            Repository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.json");
        }

        [Test]
        [Category("Db")]
        [Category("Integration")]
        [Category("InsertOne")]
        public void Can_insert_one_integration_test()
        {
            var train = new Schedule
            {
                Destination = "Vilnius",
                Notes = "Express",
                Number = 101,
                Origin = "Kaunas"
            };

            train = Repository.InsertOne(train);
            
            Assert.IsInstanceOf<Schedule>(train);
            Assert.IsNotNull(train);
            Assert.IsNotNull(train.Id);
        }
    }
}