using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Notifications.Push.Services
{
    public partial class CodeMashPushService
    {
        public SendPushNotificationResponse SendPushNotification(SendPushNotificationRequest request)
        {
            return Client.Post<SendPushNotificationResponse>(request);
        }

        public async Task<SendPushNotificationResponse> SendPushNotificationAsync(SendPushNotificationRequest request)
        {
            return await Client.PostAsync<SendPushNotificationResponse>(request);
        }

        public DeleteNotificationResponse DeleteNotification(DeleteNotificationRequest request)
        {
            return Client.Delete<DeleteNotificationResponse>(request);
        }

        public async Task<DeleteNotificationResponse> DeleteNotificationAsync(DeleteNotificationRequest request)
        {
            return await Client.DeleteAsync<DeleteNotificationResponse>(request);
        }

        public GetNotificationResponse GetNotification(GetNotificationRequest request)
        {
            return Client.Get<GetNotificationResponse>(request);
        }

        public async Task<GetNotificationResponse> GetNotificationAsync(GetNotificationRequest request)
        {
            return await Client.GetAsync<GetNotificationResponse>(request);
        }

        public GetNotificationsResponse GetNotifications(GetNotificationsRequest request)
        {
            return Client.Get<GetNotificationsResponse>(request);
        }

        public async Task<GetNotificationsResponse> GetNotificationsAsync(GetNotificationsRequest request)
        {
            return await Client.GetAsync<GetNotificationsResponse>(request);
        }

        public MarkNotificationAsReadResponse ReadNotification(MarkNotificationAsReadRequest request)
        {
            return Client.Patch<MarkNotificationAsReadResponse>(request);
        }

        public async Task<MarkNotificationAsReadResponse> ReadNotificationAsync(MarkNotificationAsReadRequest request)
        {
            var task = Task.Run(() => Client.Patch<MarkNotificationAsReadResponse>(request));
            
            return await task;
        }
    }
}