using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Notifications.Push.Services
{
    public partial class CodeMashPushService
    {
        public DeleteDeviceTokenResponse DeleteToken(DeleteDeviceTokenRequest request)
        {
            return Client.Get<DeleteDeviceTokenResponse>(request);
        }

        public async Task<DeleteDeviceTokenResponse> DeleteTokenAsync(DeleteDeviceTokenRequest request)
        {
            return await Client.GetAsync<DeleteDeviceTokenResponse>(request);
        }

        public RegisterDeviceExpoTokenResponse RegisterExpoToken(RegisterDeviceExpoTokenRequest request)
        {
            return Client.Get<RegisterDeviceExpoTokenResponse>(request);
        }

        public async Task<RegisterDeviceExpoTokenResponse> RegisterExpoTokenAsync(RegisterDeviceExpoTokenRequest request)
        {
            return await Client.GetAsync<RegisterDeviceExpoTokenResponse>(request);
        }
    }
}