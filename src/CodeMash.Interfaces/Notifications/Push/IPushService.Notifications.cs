using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Interfaces.Notifications.Push
{
    public partial interface IPushService
    {
        SendPushNotificationResponse SendPushNotification(SendPushNotificationRequest request);
        
        Task<SendPushNotificationResponse> SendPushNotificationAsync(SendPushNotificationRequest request);
        
        DeleteNotificationResponse DeleteNotification(DeleteNotificationRequest request);
        
        Task<DeleteNotificationResponse> DeleteNotificationAsync(DeleteNotificationRequest request);
        
        GetNotificationResponse GetNotification(GetNotificationRequest request);
        
        Task<GetNotificationResponse> GetNotificationAsync(GetNotificationRequest request);
        
        GetNotificationsResponse GetNotifications(GetNotificationsRequest request);
        
        Task<GetNotificationsResponse> GetNotificationsAsync(GetNotificationsRequest request);
        
        MarkNotificationAsReadResponse ReadNotification(MarkNotificationAsReadRequest request);
        
        Task<MarkNotificationAsReadResponse> ReadNotificationAsync(MarkNotificationAsReadRequest request);
        
        MarkAllNotificationsAsReadResponse ReadAllNotifications(MarkAllNotificationsAsReadRequest request);
        
        Task<MarkAllNotificationsAsReadResponse> ReadAllNotificationsAsync(MarkAllNotificationsAsReadRequest request);
    }
}