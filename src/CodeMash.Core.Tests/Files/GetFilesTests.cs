using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Linq;
using File = Isidos.CodeMash.Data.File;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class GetFilesTests
    {
        private IRepository<File> Repository { get; set; }

        [TestInitialize]
        public void SetUp()
        { 
            Repository = CodeMashRepositoryFactory.Create<File>("appsettings.Production.primary.json");

            var stream1 = new StreamWriter("a.txt");
            var stream2 = new StreamWriter("b.txt");
            var stream3 = new StreamWriter("c.txt");
            for (var i = 0; i < 2048; i++)
            {
                stream1.Write("12345678123456781234567812345678");
                stream2.Write("12341234123412341234123412341234");
                stream3.Write("12345678123456788765432187654321");
            }
            stream1.Close();
            stream2.Close();
            stream3.Close();

            var fileNames = new string[]{
                "a.txt",
                "b.txt",
                "c.txt"
            };
            Repository.UploadFilesWithRequest(fileNames, "");
        }

        [TestMethod]
        public void Can_get_files_integration_test()
        {
            var files = Repository.GetFiles();

            Assert.IsNotNull(files);
            Assert.IsTrue(files.Count > 0);
        }
    }
}