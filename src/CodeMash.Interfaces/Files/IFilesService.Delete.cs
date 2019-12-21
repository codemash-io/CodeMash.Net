using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Interfaces.Files
{
    public partial interface IFilesService
    {
        DeleteFileResponse DeleteFile(DeleteFileRequest request);
        
        Task<DeleteFileResponse> DeleteFileAsync(DeleteFileRequest request);
    }
}