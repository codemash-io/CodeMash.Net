using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Code.Services
{
    public partial class CodeMashCodeService
    {
        public ExecuteFunctionResponse ExecuteFunction(ExecuteFunctionRequest request)
        {
            return Client.Post<ExecuteFunctionResponse>(request);
        }

        public async Task<ExecuteFunctionResponse> ExecuteFunctionAsync(ExecuteFunctionRequest request)
        {
            return await Client.PostAsync<ExecuteFunctionResponse>(request);
        }
    }
}