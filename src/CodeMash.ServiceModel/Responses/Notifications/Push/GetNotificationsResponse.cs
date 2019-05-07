using System.Collections.Generic;

namespace CodeMash.ServiceModel.Responses.Notifications.Push
{
    public class GetNotificationsResponse : ResponseBase<List<NotificationDataDto>>
    {
        public int TotalCount { get; set; }
    }
    
}