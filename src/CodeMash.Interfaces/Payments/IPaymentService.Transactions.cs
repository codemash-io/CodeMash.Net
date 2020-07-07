using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Interfaces.Membership
{
    public partial interface IPaymentService
    {
        CreatePayseraTransactionResponse CreatePayseraTransaction(CreatePayseraTransactionRequest request);
        
        Task<CreatePayseraTransactionResponse> CreatePayseraTransactionAsync(CreatePayseraTransactionRequest request);
    }
}