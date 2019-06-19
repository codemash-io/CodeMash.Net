using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Interfaces
{
    public interface IPushNotificationsService
    {
        SendPushNotificationResponse Send(SendPushNotification request);
        RegisterDeviceResponse RegisterDevice(RegisterDevice request);

        GetNotificationsResponse GetAll(GetNotifications request);
        GetNotificationResponse Get(GetNotification request);

    }
}