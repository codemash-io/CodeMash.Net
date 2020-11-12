using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Interfaces.Payments
{
    public partial interface IPaymentService
    {
        CreateSubscriptionResponse CreateSubscription(CreateSubscriptionRequest request);
        
        Task<CreateSubscriptionResponse> CreateSubscriptionAsync(CreateSubscriptionRequest request);
        
        CancelSubscriptionResponse CancelSubscription(CancelSubscriptionRequest request);
        
        Task<CancelSubscriptionResponse> CancelSubscriptionAsync(CancelSubscriptionRequest request);
        
        ChangeSubscriptionResponse ChangeSubscription(ChangeSubscriptionRequest request);
        
        Task<ChangeSubscriptionResponse> ChangeSubscriptionAsync(ChangeSubscriptionRequest request);
        
        void UpdateSubscription(UpdateSubscriptionRequest request);
        
        Task UpdateSubscriptionAsync(UpdateSubscriptionRequest request);
    }
}