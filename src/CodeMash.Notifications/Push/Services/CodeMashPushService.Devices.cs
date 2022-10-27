using System.Threading.Tasks;
using CodeMash.ServiceContracts.Api;
using ServiceStack;

namespace CodeMash.Notifications.Push.Services
{
    public partial class CodeMashPushService
    {
        public CreateDeviceResponse RegisterDevice(CreateDeviceRequest request)
        {
            return Client.Post<CreateDeviceResponse>(request);
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

        public void DeleteDevice(DeleteDeviceRequest request)
        {
            Client.Delete(request);
        }

        public async Task DeleteDeviceAsync(DeleteDeviceRequest request)
        {
            await Client.DeleteAsync(request);
        }

        public async Task<CreateDeviceResponse> RegisterDeviceAsync(CreateDeviceRequest request)
        {
            return await Client.PostAsync<CreateDeviceResponse>(request);
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