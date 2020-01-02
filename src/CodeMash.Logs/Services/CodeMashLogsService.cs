using System.Threading.Tasks;
using CodeMash.Interfaces.Client;
using CodeMash.Interfaces.Logs;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Logs.Services
{
    public class CodeMashLogsService : ILogsService
    {
        public ICodeMashClient Client { get; }
        
        public CodeMashLogsService(ICodeMashClient client)
        {
            Client = client;
        }

        public CreateLogResponse CreateLog(CreateLogRequest request)
        {
            return Client.Get<CreateLogResponse>(request);
        }

        public async Task<CreateLogResponse> CreateLogAsync(CreateLogRequest request)
        {
            return await Client.GetAsync<CreateLogResponse>(request);
        }
    }
}