using System.IO;
using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Interfaces.Files
{
    public partial interface IFilesService
    {
        /* Custom Upload */
        UploadFileResponse UploadFile(byte[] file, string fileName, UploadFileRequest request);
        
        Task<UploadFileResponse> UploadFileAsync(byte[] file, string fileName, UploadFileRequest request);
        
        UploadFileResponse UploadFile(Stream file, string fileName, UploadFileRequest request);
        
        Task<UploadFileResponse> UploadFileAsync(Stream file, string fileName, UploadFileRequest request);
        
        
        /* Record Upload */
        UploadRecordFileResponse UploadRecordFile(byte[] file, string fileName, UploadRecordFileRequest request);
        
        Task<UploadRecordFileResponse> UploadRecordFileAsync(byte[] file, string fileName, UploadRecordFileRequest request);
        
        UploadRecordFileResponse UploadRecordFile(Stream file, string fileName, UploadRecordFileRequest request);
        
        Task<UploadRecordFileResponse> UploadRecordFileAsync(Stream file, string fileName, UploadRecordFileRequest request);
        
        
        /* User Upload */
        UploadUserFileResponse UploadUserFile(byte[] file, string fileName, UploadUserFileRequest request);
        
        Task<UploadUserFileResponse> UploadUserFileAsync(byte[] file, string fileName, UploadUserFileRequest request);
        
        UploadUserFileResponse UploadUserFile(Stream file, string fileName, UploadUserFileRequest request);
        
        Task<UploadUserFileResponse> UploadUserFileAsync(Stream file, string fileName, UploadUserFileRequest request);
        
        
        /* Payments Upload */
        UploadOrderFileResponse UploadOrderFile(byte[] file, string fileName, UploadOrderFileRequest request);
        
        Task<UploadOrderFileResponse> UploadOrderFileAsync(byte[] file, string fileName, UploadOrderFileRequest request);
        
        UploadOrderFileResponse UploadOrderFile(Stream file, string fileName, UploadOrderFileRequest request);
        
        Task<UploadOrderFileResponse> UploadOrderFileAsync(Stream file, string fileName, UploadOrderFileRequest request);
    }
}