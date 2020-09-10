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

        public CreateStripeTransactionResponse CreateStripeTransaction(CreateStripeTransactionRequest request)
        {
            return Client.Post<CreateStripeTransactionResponse>(request);
        }

        public async Task<CreateStripeTransactionResponse> CreateStripeTransactionAsync(CreateStripeTransactionRequest request)
        {
            return await Client.PostAsync<CreateStripeTransactionResponse>(request);
        }

        public CheckStripePaymentStatusResponse CheckStripePaymentStatus(CheckStripePaymentStatusRequest request)
        {
            return Client.Get<CheckStripePaymentStatusResponse>(request);
        }

        public async Task<CheckStripePaymentStatusResponse> CheckStripePaymentStatusAsync(CheckStripePaymentStatusRequest request)
        {
            return await Client.GetAsync<CheckStripePaymentStatusResponse>(request);
        }
    }
}