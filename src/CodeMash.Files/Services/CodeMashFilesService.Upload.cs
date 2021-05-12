using System.IO;
using System.Threading.Tasks;
using CodeMash.Client;
using Isidos.CodeMash.ServiceContracts.Api;

namespace CodeMash.Project.Services
{
    public partial class CodeMashFilesService
    {
        /* Custom File Upload */
        public UploadFileResponse UploadFile(byte[] file, string fileName, UploadFileRequest request)
        {
            var fileStream = new MemoryStream(file);
            return UploadFile(fileStream, fileName, request);
        }

        public async Task<UploadFileResponse> UploadFileAsync(byte[] file, string fileName, UploadFileRequest request)
        {
            var fileStream = new MemoryStream(file);
            return await UploadFileAsync(fileStream, fileName, request);
        }

        public UploadFileResponse UploadFile(Stream file, string fileName, UploadFileRequest request)
        {
            return Client.PostFile<UploadFileResponse>(file, request, new CodeMashRequestOptions
            {
                FileName = fileName
            });
        }

        public async Task<UploadFileResponse> UploadFileAsync(Stream file, string fileName, UploadFileRequest request)
        {
            var task = Task.Run(() => Client.PostFile<UploadFileResponse>(file, request, new CodeMashRequestOptions
            {
                FileName = fileName
            }));
            
            return await task;
        }

        
        /* Record File Upload */
        public UploadRecordFileResponse UploadRecordFile(byte[] file, string fileName, UploadRecordFileRequest request)
        {
            var fileStream = new MemoryStream(file);
            return UploadRecordFile(fileStream, fileName, request);
        }

        public async Task<UploadRecordFileResponse> UploadRecordFileAsync(byte[] file, string fileName, UploadRecordFileRequest request)
        {
            var fileStream = new MemoryStream(file);
            return await UploadRecordFileAsync(fileStream, fileName, request);
        }

        public UploadRecordFileResponse UploadRecordFile(Stream file, string fileName, UploadRecordFileRequest request)
        {
            return Client.PostFile<UploadRecordFileResponse>(file, request, new CodeMashRequestOptions
            {
                FileName = fileName
            });
        }

        public async Task<UploadRecordFileResponse> UploadRecordFileAsync(Stream file, string fileName, UploadRecordFileRequest request)
        {
            var task = Task.Run(() => Client.PostFile<UploadRecordFileResponse>(file, request, new CodeMashRequestOptions
            {
                FileName = fileName
            }));
            
            return await task;
        }

        
        /* User File Upload */
        public UploadUserFileResponse UploadUserFile(byte[] file, string fileName, UploadUserFileRequest request)
        {
            var fileStream = new MemoryStream(file);
            return UploadUserFile(fileStream, fileName, request);
        }

        public async Task<UploadUserFileResponse> UploadUserFileAsync(byte[] file, string fileName, UploadUserFileRequest request)
        {
            var fileStream = new MemoryStream(file);
            return await UploadUserFileAsync(fileStream, fileName, request);
        }

        public UploadUserFileResponse UploadUserFile(Stream file, string fileName, UploadUserFileRequest request)
        {
            return Client.PostFile<UploadUserFileResponse>(file, request, new CodeMashRequestOptions
            {
                FileName = fileName
            });
        }

        public async Task<UploadUserFileResponse> UploadUserFileAsync(Stream file, string fileName, UploadUserFileRequest request)
        {
            var task = Task.Run(() => Client.PostFile<UploadUserFileResponse>(file, request, new CodeMashRequestOptions
            {
                FileName = fileName
            }));
            
            return await task;
        }
        
        
        /* Payment File Upload */
        public UploadOrderFileResponse UploadOrderFile(byte[] file, string fileName, UploadOrderFileRequest request)
        {
            var fileStream = new MemoryStream(file);
            return UploadOrderFile(fileStream, fileName, request);
        }

        public async Task<UploadOrderFileResponse> UploadOrderFileAsync(byte[] file, string fileName, UploadOrderFileRequest request)
        {
            var fileStream = new MemoryStream(file);
            return await UploadOrderFileAsync(fileStream, fileName, request);
        }

        public UploadOrderFileResponse UploadOrderFile(Stream file, string fileName, UploadOrderFileRequest request)
        {
            return Client.PostFile<UploadOrderFileResponse>(file, request, new CodeMashRequestOptions
            {
                FileName = fileName
            });
        }

        public async Task<UploadOrderFileResponse> UploadOrderFileAsync(Stream file, string fileName, UploadOrderFileRequest request)
        {
            var task = Task.Run(() => Client.PostFile<UploadOrderFileResponse>(file, request, new CodeMashRequestOptions
            {
                FileName = fileName
            }));
            
            return await task;
        }
    }
}