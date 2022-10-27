using System.Threading.Tasks;
using CodeMash.ServiceContracts.Api;

namespace CodeMash.Payments.Services
{
    public partial class CodeMashPaymentsService
    {
        public GetPaymentMethodSetupResponse GetPaymentMethodSetup(GetPaymentMethodSetupRequest request)
        {
            return Client.Get<GetPaymentMethodSetupResponse>(request);
        }

        public async Task<GetPaymentMethodSetupResponse> GetPaymentMethodSetupAsync(GetPaymentMethodSetupRequest request)
        {
            return await Client.GetAsync<GetPaymentMethodSetupResponse>(request);
        }

        public AttachPaymentMethodResponse AttachPaymentMethod(AttachPaymentMethodRequest request)
        {
            return Client.Post<AttachPaymentMethodResponse>(request);
        }

        public async Task<AttachPaymentMethodResponse> AttachPaymentMethodAsync(AttachPaymentMethodRequest request)
        {
            return await Client.PostAsync<AttachPaymentMethodResponse>(request);
        }

        public void DetachPaymentMethod(DetachPaymentMethodRequest request)
        {
            Client.Delete<AttachPaymentMethodResponse>(request);
        }

        public async Task DetachPaymentMethodAsync(DetachPaymentMethodRequest request)
        {
            await Client.DeleteAsync<AttachPaymentMethodResponse>(request);
        }

        public void UpdatePaymentMethod(UpdatePaymentMethodRequest request)
        {
            Client.Patch<object>(request);
        }

        public async Task UpdatePaymentMethodAsync(UpdatePaymentMethodRequest request)
        {
            await Client.PatchAsync<object>(request);
        }
    }
}