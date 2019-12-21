using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Interfaces.Notifications.Push
{
    public partial interface IPushService
    {
        UpdateDeviceMetaResponse UpdateDeviceMeta(UpdateDeviceMetaRequest request);
        
        Task<UpdateDeviceMetaResponse> UpdateDeviceMetaAsync(UpdateDeviceMetaRequest request);
        
        DeleteDeviceMetaResponse DeleteDeviceMeta(DeleteDeviceMetaRequest request);
        
        Task<DeleteDeviceMetaResponse> DeleteDeviceMetaAsync(DeleteDeviceMetaRequest request);
        
        DeleteDeviceResponse DeleteDevice(DeleteDeviceRequest request);
        
        Task<DeleteDeviceResponse> DeleteDeviceAsync(DeleteDeviceRequest request);
        
        GetDeviceResponse GetDevice(GetDeviceRequest request);
        
        Task<GetDeviceResponse> GetDeviceAsync(GetDeviceRequest request);
        
        GetDevicesResponse GetDevices(GetDevicesRequest request);
        
        Task<GetDevicesResponse> GetDevicesAsync(GetDevicesRequest request);
        
        RegisterDeviceResponse RegisterDevice(RegisterDeviceRequest request);
        
        Task<RegisterDeviceResponse> RegisterDevicesAsync(RegisterDeviceRequest request);
        
        UpdateDeviceMetaResponse UpdateDeviceTimeZone(UpdateDeviceTimeZoneRequest request);
        
        Task<UpdateDeviceMetaResponse> UpdateDeviceTimeZoneAsync(UpdateDeviceTimeZoneRequest request);
        
        UpdateDeviceUserResponse UpdateDeviceUser(UpdateDeviceUserRequest request);
        
        Task<UpdateDeviceUserResponse> UpdateDeviceUserAsync(UpdateDeviceUserRequest request);
    }
}