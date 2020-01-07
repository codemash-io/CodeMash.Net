using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;
using ServiceStack;

namespace CodeMash.Project.Services
{
    public partial class CodeMashFilesService
    {
        public Stream GetFileStream(GetFileRequest request)
        {
            var fileUrl = GetFileUrl(request);
            
            var req = WebRequest.Create(fileUrl.Result);
            var resp = req.GetResponse();
            return resp.GetResponseStream();
        }

        public async Task<Stream> GetFileStreamAsync(GetFileRequest request)
        {
            var fileUrl = await GetFileUrlAsync(request);
            
            var req = WebRequest.Create(fileUrl.Result);
            var resp = await req.GetResponseAsync();
            return resp.GetResponseStream();
        }
        
        public byte[] GetFileBytes(GetFileRequest request)
        {
            return GetFileStream(request).ToBytes();
        }

        public async Task<byte[]> GetFileBytesAsync(GetFileRequest request)
        {
            return (await GetFileStreamAsync(request)).ToBytes();
        }

        public GetFileResponse GetFileUrl(GetFileRequest request)
        {
            return Client.Get<GetFileResponse>(request);
        }

        public async Task<GetFileResponse> GetFileUrlAsync(GetFileRequest request)
        {
            return await Client.GetAsync<GetFileResponse>(request);
        }
    }
}