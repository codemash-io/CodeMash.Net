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
    public class FileDownloadTests : FileTestBase
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
        public void Can_download_file_url()
        {
            var uploadedFile = UploadFile();
            var response = Service.GetFileUrl(new GetFileRequest
            {
                FileId = uploadedFile.Id
            });
            
            Assert.IsNotNull(response.Result);
        }
        
        [TestMethod]
        public async Task Can_download_file_url_async()
        {
            var uploadedFile = UploadFile();
            var response = await Service.GetFileUrlAsync(new GetFileRequest
            {
                FileId = uploadedFile.Id
            });
            
            Assert.IsNotNull(response.Result);
        }
        
        [TestMethod]
        public void Can_download_file_stream()
        {
            var uploadedFile = UploadFile();
            var response = Service.GetFileStream(new GetFileRequest
            {
                FileId = uploadedFile.Id
            });
            
            var reader = new StreamReader(response);
            var text = reader.ReadToEnd();

            Assert.IsTrue(text.Length == uploadedFile.Size);
        }
        
        [TestMethod]
        public async Task Can_download_file_stream_async()
        {
            var uploadedFile = UploadFile();
            var response = await Service.GetFileStreamAsync(new GetFileRequest
            {
                FileId = uploadedFile.Id
            });
            
            var reader = new StreamReader(response);
            var text = reader.ReadToEnd();

            Assert.IsTrue(text.Length == uploadedFile.Size);
        }
        
        [TestMethod]
        public void Can_download_file_bytes()
        {
            var uploadedFile = UploadFile();
            var response = Service.GetFileBytes(new GetFileRequest
            {
                FileId = uploadedFile.Id
            });
            
            var text = System.Text.Encoding.UTF8.GetString(response);

            Assert.IsTrue(text.Length == uploadedFile.Size);
        }
        
        [TestMethod]
        public async Task Can_download_file_bytes_async()
        {
            var uploadedFile = UploadFile();
            var response = await Service.GetFileBytesAsync(new GetFileRequest
            {
                FileId = uploadedFile.Id
            });
            
            var text = System.Text.Encoding.UTF8.GetString(response);

            Assert.IsTrue(text.Length == uploadedFile.Size);
        }
    }
}