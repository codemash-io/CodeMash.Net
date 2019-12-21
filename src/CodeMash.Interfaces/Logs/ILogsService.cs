using System.Threading.Tasks;
using CodeMash.Interfaces.Client;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Interfaces.Logs
{
    public interface ILogsService : IClientService
    {
        CreateLogResponse CreateLog(CreateLogRequest request);
        
        Task<CreateLogResponse> CreateLogAsync(CreateLogRequest request);
    }
}