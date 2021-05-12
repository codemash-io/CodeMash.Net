using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts.Api;

namespace CodeMash.Project.Services
{
    public partial class CodeMashFilesService
    {
        public DeleteFileResponse DeleteFile(DeleteFileRequest request)
        {
            return Client.Delete<DeleteFileResponse>(request);
        }

        public async Task<DeleteFileResponse> DeleteFileAsync(DeleteFileRequest request)
        {
            return await Client.DeleteAsync<DeleteFileResponse>(request);
        }
    }
}