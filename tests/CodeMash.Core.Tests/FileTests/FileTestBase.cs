using System.IO;
using System.Reflection;
using CodeMash.Client;
using CodeMash.Interfaces.Files;
using CodeMash.Project.Services;
using CodeMash.ServiceContracts.Api;
using File = Isidos.CodeMash.ServiceContracts.Api.File;

namespace CodeMash.Core.Tests
{
    public abstract class FileTestBase : TestBase
    {
        protected IFilesService Service { get; set; }

        protected string uploadedFileId;

        public override void SetUp()
        {
            base.SetUp();
            
            var client = new CodeMashClient(ApiKey, ProjectId);
            Service = new CodeMashFilesService(client);
        }

        public File UploadFile()
        {
            var directory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var path = $"{directory}\\FileTests\\Files\\test.txt";
            
            var bytes = System.IO.File.ReadAllBytes(path);
            var response = Service.UploadFile(bytes, "test.txt", new UploadFileRequest
            {
                Path = "sdk"
            });

            uploadedFileId = response.Result.Id;

            return response.Result;
        }

        public override void TearDown()
        {
            if (!string.IsNullOrEmpty(uploadedFileId))
            {
                var response = Service.DeleteFile(new DeleteFileRequest
                {
                    FileId = uploadedFileId
                });
            }
        }
    }
}