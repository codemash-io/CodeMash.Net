using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Interfaces.Payments
{
    public partial interface IPaymentService
    {
        CreatePayseraTransactionResponse CreatePayseraTransaction(CreatePayseraTransactionRequest request);
        
        Task<CreatePayseraTransactionResponse> CreatePayseraTransactionAsync(CreatePayseraTransactionRequest request);
        
        CreateStripeTransactionResponse CreateStripeTransaction(CreateStripeTransactionRequest request);
        
        Task<CreateStripeTransactionResponse> CreateStripeTransactionAsync(CreateStripeTransactionRequest request);
        
        CheckStripePaymentStatusResponse CheckStripePaymentStatus(CheckStripePaymentStatusRequest request);
        
        Task<CheckStripePaymentStatusResponse> CheckStripePaymentStatusAsync(CheckStripePaymentStatusRequest request);
    }
}