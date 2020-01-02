using System.Threading.Tasks;
using CodeMash.Interfaces.Client;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Interfaces.Code
{
    public interface ICodeService : IClientService
    {
        ExecuteFunctionResponse ExecuteFunction(ExecuteFunctionRequest request);
        
        Task<ExecuteFunctionResponse> ExecuteFunctionAsync(ExecuteFunctionRequest request);
    }
}