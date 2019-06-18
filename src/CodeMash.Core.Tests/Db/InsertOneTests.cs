using System;
using System.Collections.Generic;
using CodeMash.Repository;
using MongoDB.Bson;
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
            var schedule = new Schedule
            {
                Destination = "Vilnius",
                Notes = "Express",
                Number = 54,
                Origin = "Kaunas"
            };

            schedule = Repository.InsertOne(schedule);

            Assert.IsInstanceOf<Schedule>(schedule);
            Assert.IsNotNull(schedule);
            Assert.IsNotNull(schedule.Id);
        }
    }
}