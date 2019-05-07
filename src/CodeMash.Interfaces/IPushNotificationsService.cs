using CodeMash.ServiceModel.Requests.Notifications.Push;
using CodeMash.ServiceModel.Responses.Notifications.Push;

namespace CodeMash.Interfaces
{
    public interface IPushNotificationsService
    {
        CreateNotificationResponse CreateNotification(CreateNotification request);

        CreateNotificationDeviceResponse CreateNotificationDevice(CreateNotificationDevice request);

    }
}