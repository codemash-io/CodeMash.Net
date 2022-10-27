using System.Threading.Tasks;
using CodeMash.ServiceContracts.Api;
using ServiceStack;

namespace CodeMash.Interfaces.Files
{
    public partial interface IFilesService
    {
        IReturnVoid DeleteFile(DeleteFileRequest request);
        
        Task<IReturnVoid> DeleteFileAsync(DeleteFileRequest request);
    }
}