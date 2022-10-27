using System.Threading.Tasks;
using CodeMash.ServiceContracts.Api;

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
        
        GetNotificationsResponse GetNotificationsCount(GetNotificationsCountRequest request);
        
        Task<GetNotificationsResponse> GetNotificationsCountAsync(GetNotificationsCountRequest request);
        
        MarkNotificationAsReadResponse ReadNotification(MarkNotificationAsReadRequest request);
        
        Task<MarkNotificationAsReadResponse> ReadNotificationAsync(MarkNotificationAsReadRequest request);
        
        MarkNotificationAsReadResponse ReadAllNotifications(MarkAllNotificationsAsReadRequest request);
        
        Task<MarkNotificationAsReadResponse> ReadAllNotificationsAsync(MarkAllNotificationsAsReadRequest request);
    }
}