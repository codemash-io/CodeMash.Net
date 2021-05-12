using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts.Api;

namespace CodeMash.Interfaces.Files
{
    public partial interface IFilesService
    {
        DeleteFileResponse DeleteFile(DeleteFileRequest request);
        
        Task<DeleteFileResponse> DeleteFileAsync(DeleteFileRequest request);
    }
}