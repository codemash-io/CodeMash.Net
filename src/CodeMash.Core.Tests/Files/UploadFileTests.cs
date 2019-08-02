using System;
using CodeMash.Repository;
using Isidos.CodeMash.ServiceContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServiceStack.Host;
using ServiceStack.Web;
using ErrorMessages = CodeMash.Repository.Statics.Database.ErrorMessages;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class UploadFileTests
    {
        private IRepository<File> Repository { get; set; }

        private File _file;
        
        [TestInitialize]
        public void SetUp()
        {
            Repository = CodeMashRepositoryFactory.Create<File>("appsettings.Production.primary.json");
        }

        [TestMethod]
        public void Can_upload_file_integration_test()
        {
            var uploaded = Repository.UploadFileWithRequest("C:\\Users\\Present Connection\\Desktop\\appsettings.Production - Copy.json", "");

            Assert.IsTrue(uploaded);
        }

        [TestMethod]
        public void Exception_insert_one_entity_null_integration_test()
        {

            Assert.ThrowsException<ArgumentNullException>(() => Repository.InsertOne<Schedule>(null), ErrorMessages.EntityIsNotDefined);
        }
    }
}