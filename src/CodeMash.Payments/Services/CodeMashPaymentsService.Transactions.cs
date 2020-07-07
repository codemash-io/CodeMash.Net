using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Payments.Services
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