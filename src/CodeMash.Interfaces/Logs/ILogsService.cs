using System.Threading.Tasks;
using CodeMash.Interfaces.Client;
using CodeMash.ServiceContracts.Api;

namespace CodeMash.Interfaces.Logs
{
    public interface ILogsService : IClientService
    {
        CreateLogResponse CreateLog(CreateLogRequest request);
        
        Task<CreateLogResponse> CreateLogAsync(CreateLogRequest request);
    }
}