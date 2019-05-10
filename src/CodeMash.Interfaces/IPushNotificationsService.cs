using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Interfaces
{
    public interface IPushNotificationsService
    {
        CreateNotificationResponse CreateNotification(CreateNotification request);

        CreateNotificationDeviceResponse CreateNotificationDevice(CreateNotificationDevice request);

        GetNotificationsResponse GetNotifications(GetNotifications request);
        GetNotificationResponse GetNotification(GetNotification request);

    }
}