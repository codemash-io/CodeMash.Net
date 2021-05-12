using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts.Api;

namespace CodeMash.Interfaces.Payments
{
    public partial interface IPaymentService
    {
        GetPaymentMethodSetupResponse GetPaymentMethodSetup(GetPaymentMethodSetupRequest request);
        
        Task<GetPaymentMethodSetupResponse> GetPaymentMethodSetupAsync(GetPaymentMethodSetupRequest request);
        
        AttachPaymentMethodResponse AttachPaymentMethod(AttachPaymentMethodRequest request);
        
        Task<AttachPaymentMethodResponse> AttachPaymentMethodAsync(AttachPaymentMethodRequest request);
        
        void DetachPaymentMethod(DetachPaymentMethodRequest request);
        
        Task DetachPaymentMethodAsync(DetachPaymentMethodRequest request);
        
        void UpdatePaymentMethod(UpdatePaymentMethodRequest request);
        
        Task UpdatePaymentMethodAsync(UpdatePaymentMethodRequest request);
    }
}