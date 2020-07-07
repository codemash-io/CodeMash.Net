using System.Threading.Tasks;
using CodeMash.Interfaces.Client;
using CodeMash.Interfaces.Logs;
using CodeMash.Interfaces.Membership;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Logs.Services
{
    public partial class CodeMashPaymentsService
    {
        public CreatePayseraTransactionResponse CreatePayseraTransaction(CreatePayseraTransactionRequest request)
        {
            return Client.Post<CreatePayseraTransactionResponse>(request);
        }

        public async Task<CreatePayseraTransactionResponse> CreatePayseraTransactionAsync(CreatePayseraTransactionRequest request)
        {
            return await Client.PostAsync<CreatePayseraTransactionResponse>(request);
        }
    }
}