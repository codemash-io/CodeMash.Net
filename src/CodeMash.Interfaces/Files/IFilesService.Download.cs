using System.IO;
using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Interfaces.Files
{
    public partial interface IFilesService
    {
        Stream GetFileStream(GetFileRequest request);
        
        Task<Stream> GetFileStreamAsync(GetFileRequest request);
        
        byte[] GetFileBytes(GetFileRequest request);
        
        Task<byte[]> GetFileBytesAsync(GetFileRequest request);
        
        GetFileResponse GetFileUrl(GetFileRequest request);
        
        Task<GetFileResponse> GetFileUrlAsync(GetFileRequest request);
    }
}