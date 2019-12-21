using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Notifications.Push.Services
{
    public partial class CodeMashPushService
    {
        public UpdateDeviceMetaResponse UpdateDeviceMeta(UpdateDeviceMetaRequest request)
        {
            return Client.Put<UpdateDeviceMetaResponse>(request);
        }

        public async Task<UpdateDeviceMetaResponse> UpdateDeviceMetaAsync(UpdateDeviceMetaRequest request)
        {
            return await Client.PutAsync<UpdateDeviceMetaResponse>(request);
        }

        public DeleteDeviceMetaResponse DeleteDeviceMeta(DeleteDeviceMetaRequest request)
        {
            return Client.Delete<DeleteDeviceMetaResponse>(request);
        }

        public async Task<DeleteDeviceMetaResponse> DeleteDeviceMetaAsync(DeleteDeviceMetaRequest request)
        {
            return await Client.DeleteAsync<DeleteDeviceMetaResponse>(request);
        }

        public DeleteDeviceResponse DeleteDevice(DeleteDeviceRequest request)
        {
            return Client.Delete<DeleteDeviceResponse>(request);
        }

        public async Task<DeleteDeviceResponse> DeleteDeviceAsync(DeleteDeviceRequest request)
        {
            return await Client.DeleteAsync<DeleteDeviceResponse>(request);
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

        public RegisterDeviceResponse RegisterDevice(RegisterDeviceRequest request)
        {
            return Client.Post<RegisterDeviceResponse>(request);
        }

        public async Task<RegisterDeviceResponse> RegisterDevicesAsync(RegisterDeviceRequest request)
        {
            return await Client.PostAsync<RegisterDeviceResponse>(request);
        }

        public UpdateDeviceMetaResponse UpdateDeviceTimeZone(UpdateDeviceTimeZoneRequest request)
        {
            return Client.Put<UpdateDeviceMetaResponse>(request);
        }

        public async Task<UpdateDeviceMetaResponse> UpdateDeviceTimeZoneAsync(UpdateDeviceTimeZoneRequest request)
        {
            return await Client.PutAsync<UpdateDeviceMetaResponse>(request);
        }

        public UpdateDeviceUserResponse UpdateDeviceUser(UpdateDeviceUserRequest request)
        {
            return Client.Put<UpdateDeviceUserResponse>(request);
        }

        public async Task<UpdateDeviceUserResponse> UpdateDeviceUserAsync(UpdateDeviceUserRequest request)
        {
            return await Client.PutAsync<UpdateDeviceUserResponse>(request);
        }
    }
}