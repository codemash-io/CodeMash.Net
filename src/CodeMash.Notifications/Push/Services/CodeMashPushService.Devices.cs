using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Notifications.Push.Services
{
    public partial class CodeMashPushService
    {
        public RegisterDeviceResponse RegisterDevice(RegisterDeviceRequest request)
        {
            return Client.Post<RegisterDeviceResponse>(request);
        }
        
        public GetDeviceResponse GetDevice(GetDeviceRequest request)
        {
            return Client.Get<GetDeviceResponse>(request);
        }

        public async Task<GetDeviceResponse> GetDeviceAsync(GetDeviceRequest request)
        {
            return await Client.GetAsync<GetDeviceResponse>(request);
        }

        public GetDevicesResponse GetDevices(GetDevicesRequest request)
        {
            return Client.Get<GetDevicesResponse>(request);
        }

        public async Task<GetDevicesResponse> GetDevicesAsync(GetDevicesRequest request)
        {
            return await Client.GetAsync<GetDevicesResponse>(request);
        }

        public DeleteDeviceResponse DeleteDevice(DeleteDeviceRequest request)
        {
            return Client.Delete<DeleteDeviceResponse>(request);
        }

        public async Task<DeleteDeviceResponse> DeleteDeviceAsync(DeleteDeviceRequest request)
        {
            return await Client.DeleteAsync<DeleteDeviceResponse>(request);
        }

        public async Task<RegisterDeviceResponse> RegisterDeviceAsync(RegisterDeviceRequest request)
        {
            return await Client.PostAsync<RegisterDeviceResponse>(request);
        }
        
        public UpdateDeviceMetaResponse UpdateDeviceMeta(UpdateDeviceMetaRequest request)
        {
            return Client.Patch<UpdateDeviceMetaResponse>(request);
        }

        public async Task<UpdateDeviceMetaResponse> UpdateDeviceMetaAsync(UpdateDeviceMetaRequest request)
        {
            return await Client.PatchAsync<UpdateDeviceMetaResponse>(request);
        }

        public UpdateDeviceTimeZoneResponse UpdateDeviceTimeZone(UpdateDeviceTimeZoneRequest request)
        {
            return Client.Patch<UpdateDeviceTimeZoneResponse>(request);
        }

        public async Task<UpdateDeviceTimeZoneResponse> UpdateDeviceTimeZoneAsync(UpdateDeviceTimeZoneRequest request)
        {
            return await Client.PatchAsync<UpdateDeviceTimeZoneResponse>(request);
        }

        public UpdateDeviceUserResponse UpdateDeviceUser(UpdateDeviceUserRequest request)
        {
            return Client.Patch<UpdateDeviceUserResponse>(request);
        }

        public async Task<UpdateDeviceUserResponse> UpdateDeviceUserAsync(UpdateDeviceUserRequest request)
        {
            return await Client.PatchAsync<UpdateDeviceUserResponse>(request);
        }
    }
}