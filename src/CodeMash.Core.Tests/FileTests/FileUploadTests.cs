using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using CodeMash.Client;
using CodeMash.Notifications.Push.Services;
using Isidos.CodeMash.ServiceContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using File = System.IO.File;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class FileUploadTests : FileTestBase
    {
        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
        }

        [TestCleanup]
        public override void TearDown()
        {
            base.TearDown();
        }

        [TestMethod]
        public void Can_upload_file_stream()
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = $"{directory}\\FileTests\\Files\\test.txt";

            using (var fsSource = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var response = Service.UploadFile(fsSource, "test.txt", new UploadFileRequest
                {
                    Path = "sdk"
                });

                uploadedFileId = response.Result.Id;
                Assert.IsNotNull(response.Result);
                Assert.AreEqual(response.Result.OriginalName, "test.txt");
                Assert.AreEqual(response.Result.Size, fsSource.Length);
            }
        }
        
        [TestMethod]
        public async Task Can_upload_file_stream_async()
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = $"{directory}\\FileTests\\Files\\test.txt";

            using (var fsSource = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                var response = await Service.UploadFileAsync(fsSource, "test.txt", new UploadFileRequest
                {
                    Path = "sdk"
                });
                
                uploadedFileId = response.Result.Id;
                Assert.IsNotNull(response.Result);
                Assert.AreEqual(response.Result.OriginalName, "test.txt");
                Assert.AreEqual(response.Result.Size, fsSource.Length);
            }
        }
        
        [TestMethod]
        public void Can_upload_file_bytes()
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = $"{directory}\\FileTests\\Files\\test.txt";

            var bytes = File.ReadAllBytes(path);
            var response = Service.UploadFile(bytes, "test.txt", new UploadFileRequest
            {
                Path = "sdk"
            });
                
            uploadedFileId = response.Result.Id;
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(response.Result.OriginalName, "test.txt");
            Assert.AreEqual(response.Result.Size, bytes.Length);
        }
        
        [TestMethod]
        public async Task Can_upload_file_bytes_async()
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = $"{directory}\\FileTests\\Files\\test.txt";

            var bytes = File.ReadAllBytes(path);
            var response = await Service.UploadFileAsync(bytes, "test.txt", new UploadFileRequest
            {
                Path = "sdk"
            });
                
            uploadedFileId = response.Result.Id;
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(response.Result.OriginalName, "test.txt");
            Assert.AreEqual(response.Result.Size, bytes.Length);
        }
        
        [TestMethod]
        public void Can_delete_file()
        {
            var uploadedFile = UploadFile();
            var response = Service.DeleteFile(new DeleteFileRequest
            {
                FileId = uploadedFile.Id
            });
                
            uploadedFileId = null;
            Assert.IsTrue(response.Result);
        }
        
        [TestMethod]
        public async Task Can_delete_file_async()
        {
            var uploadedFile = UploadFile();
            var response = await Service.DeleteFileAsync(new DeleteFileRequest
            {
                FileId = uploadedFile.Id
            });

            uploadedFileId = null;
            Assert.IsTrue(response.Result);
        }
    }
}