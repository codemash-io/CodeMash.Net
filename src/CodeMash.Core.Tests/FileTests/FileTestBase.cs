using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using CodeMash.Client;
using CodeMash.Code.Services;
using CodeMash.Interfaces.Code;
using CodeMash.Interfaces.Database.Repository;
using CodeMash.Interfaces.Files;
using CodeMash.Interfaces.Logs;
using CodeMash.Interfaces.Membership;
using CodeMash.Interfaces.Notifications.Email;
using CodeMash.Interfaces.Notifications.Push;
using CodeMash.Interfaces.Project;
using CodeMash.Logs.Services;
using CodeMash.Membership.Services;
using CodeMash.Notifications.Email.Services;
using CodeMash.Notifications.Push.Services;
using CodeMash.Project.Services;
using Isidos.CodeMash.ServiceContracts;
using File = Isidos.CodeMash.ServiceContracts.File;

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