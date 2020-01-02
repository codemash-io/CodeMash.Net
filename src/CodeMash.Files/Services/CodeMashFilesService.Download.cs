using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Project.Services
{
    public partial class CodeMashFilesService
    {
        public GetFileStreamResponse GetFileStream(GetFileStreamRequest request)
        {
            return Client.Get<GetFileStreamResponse>(request);
        }

        public async Task<GetFileStreamResponse> GetFileStreamAsync(GetFileStreamRequest request)
        {
            return await Client.GetAsync<GetFileStreamResponse>(request);
        }

        public GetFileUrlResponse GetFileUrl(GetFileUrlRequest request)
        {
            return Client.Get<GetFileUrlResponse>(request);
        }

        public async Task<GetFileUrlResponse> GetFileUrlAsync(GetFileUrlRequest request)
        {
            return await Client.GetAsync<GetFileUrlResponse>(request);
        }
    }
}