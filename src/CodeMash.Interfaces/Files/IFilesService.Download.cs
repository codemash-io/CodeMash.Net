using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Interfaces.Files
{
    public partial interface IFilesService
    {
        GetFileStreamResponse GetFileStream(GetFileStreamRequest request);
        
        Task<GetFileStreamResponse> GetFileStreamAsync(GetFileStreamRequest request);
        
        GetFileUrlResponse GetFileUrl(GetFileUrlRequest request);
        
        Task<GetFileUrlResponse> GetFileUrlAsync(GetFileUrlRequest request);
    }
}