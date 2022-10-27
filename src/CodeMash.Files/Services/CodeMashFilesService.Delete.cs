using System.Threading.Tasks;
using CodeMash.ServiceContracts.Api;
using ServiceStack;

namespace CodeMash.Project.Services
{
    public partial class CodeMashFilesService
    {
        public IReturnVoid DeleteFile(DeleteFileRequest request)
        {
            return Client.Delete<IReturnVoid>(request);
        }

        public async Task<IReturnVoid> DeleteFileAsync(DeleteFileRequest request)
        {
            return await Client.DeleteAsync<IReturnVoid>(request);
        }
    }
}