using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class UploadFileTests
    {
        private IRepository<Schedule> Repository { get; set; }
        
        [TestInitialize]
        public void SetUp()
        {

            Repository = CodeMashRepositoryFactory.Create<Schedule>("appsettings.Production.primary.json");
        }

        [TestMethod]
        public void Can_upload_file_integration_test()
        {
            var stream = new StreamWriter("aa.txt");
            for(int i = 0; i < 2048; i++){
                stream.Write("87654321876543211234567812345678");
            }
            stream.Close();
            
            var uploaded = Repository.UploadFileWithRequest("aa.txt", "");

            Assert.IsTrue(uploaded);
        }

        [TestMethod]
        public void Can_upload_files_integration_test()
        {
            var stream1 = new StreamWriter("a.txt");
            var stream2 = new StreamWriter("b.txt");
            var stream3 = new StreamWriter("c.txt");
            for (int i = 0; i < 2048; i++){
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
            var uploaded = Repository.UploadFilesWithRequest(fileNames, "");

            Assert.IsTrue(uploaded);
        }
    }
}