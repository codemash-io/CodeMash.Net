using System.Threading.Tasks;
using CodeMash.ServiceContracts.Api;
using ServiceStack;

namespace CodeMash.Interfaces.Notifications.Push
{
    public partial interface IPushService
    {
        CreateDeviceResponse RegisterDevice(CreateDeviceRequest request);
        
        Task<CreateDeviceResponse> RegisterDeviceAsync(CreateDeviceRequest request);
        
        GetDeviceResponse GetDevice(GetDeviceRequest request);
        
        Task<GetDeviceResponse> GetDeviceAsync(GetDeviceRequest request);
        
        GetDevicesResponse GetDevices(GetDevicesRequest request);
        
        Task<GetDevicesResponse> GetDevicesAsync(GetDevicesRequest request);
        
        void DeleteDevice(DeleteDeviceRequest request);
        
        Task DeleteDeviceAsync(DeleteDeviceRequest request);
        
        UpdateDeviceMetaResponse UpdateDeviceMeta(UpdateDeviceMetaRequest request);
        
        Task<UpdateDeviceMetaResponse> UpdateDeviceMetaAsync(UpdateDeviceMetaRequest request);
        
        UpdateDeviceTimeZoneResponse UpdateDeviceTimeZone(UpdateDeviceTimeZoneRequest request);
        
        Task<UpdateDeviceTimeZoneResponse> UpdateDeviceTimeZoneAsync(UpdateDeviceTimeZoneRequest request);
        
        UpdateDeviceUserResponse UpdateDeviceUser(UpdateDeviceUserRequest request);
        
        Task<UpdateDeviceUserResponse> UpdateDeviceUserAsync(UpdateDeviceUserRequest request);
    }
}