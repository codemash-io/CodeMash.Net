using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Interfaces.Notifications.Push
{
    public partial interface IPushService
    {
        DeleteDeviceTokenResponse DeleteToken(DeleteDeviceTokenRequest request);
        
        Task<DeleteDeviceTokenResponse> DeleteTokenAsync(DeleteDeviceTokenRequest request);
        
        RegisterDeviceExpoTokenResponse RegisterExpoToken(RegisterDeviceExpoTokenRequest request);
        
        Task<RegisterDeviceExpoTokenResponse> RegisterExpoTokenAsync(RegisterDeviceExpoTokenRequest request);
    }
}