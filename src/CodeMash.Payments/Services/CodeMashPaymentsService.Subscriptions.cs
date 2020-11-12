using System.Threading.Tasks;
using CodeMash.Interfaces.Payments;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Payments.Services
{
    public partial class CodeMashPaymentsService
    {
        public CreateSubscriptionResponse CreateSubscription(CreateSubscriptionRequest request)
        {
            return Client.Post<CreateSubscriptionResponse>(request);
        }

        public async Task<CreateSubscriptionResponse> CreateSubscriptionAsync(CreateSubscriptionRequest request)
        {
            return await Client.PostAsync<CreateSubscriptionResponse>(request);
        }

        public CancelSubscriptionResponse CancelSubscription(CancelSubscriptionRequest request)
        {
            return Client.Delete<CancelSubscriptionResponse>(request);
        }

        public async Task<CancelSubscriptionResponse> CancelSubscriptionAsync(CancelSubscriptionRequest request)
        {
            return await Client.DeleteAsync<CancelSubscriptionResponse>(request);
        }

        public ChangeSubscriptionResponse ChangeSubscription(ChangeSubscriptionRequest request)
        {
            return Client.Put<ChangeSubscriptionResponse>(request);
        }

        public async Task<ChangeSubscriptionResponse> ChangeSubscriptionAsync(ChangeSubscriptionRequest request)
        {
            return await Client.PutAsync<ChangeSubscriptionResponse>(request);
        }

        public void UpdateSubscription(UpdateSubscriptionRequest request)
        {
            Client.Patch<object>(request);
        }

        public async Task UpdateSubscriptionAsync(UpdateSubscriptionRequest request)
        {
            await Client.PatchAsync<object>(request);
        }
    }
}