using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

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
        
        DeleteDeviceResponse DeleteDevice(DeleteDeviceRequest request);
        
        Task<DeleteDeviceResponse> DeleteDeviceAsync(DeleteDeviceRequest request);
        
        UpdateDeviceMetaResponse UpdateDeviceMeta(UpdateDeviceMetaRequest request);
        
        Task<UpdateDeviceMetaResponse> UpdateDeviceMetaAsync(UpdateDeviceMetaRequest request);
        
        UpdateDeviceTimeZoneResponse UpdateDeviceTimeZone(UpdateDeviceTimeZoneRequest request);
        
        Task<UpdateDeviceTimeZoneResponse> UpdateDeviceTimeZoneAsync(UpdateDeviceTimeZoneRequest request);
        
        UpdateDeviceUserResponse UpdateDeviceUser(UpdateDeviceUserRequest request);
        
        Task<UpdateDeviceUserResponse> UpdateDeviceUserAsync(UpdateDeviceUserRequest request);
    }
}