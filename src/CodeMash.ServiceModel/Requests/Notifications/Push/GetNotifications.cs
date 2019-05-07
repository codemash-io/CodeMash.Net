using System.Runtime.Serialization;
using CodeMash.ServiceModel.Responses.Notifications.Push;
using ServiceStack;

namespace CodeMash.ServiceModel.Requests.Notifications.Push
{
    [DataContract]
    public class GetNotifications : ListRequestBase, IReturn<GetNotificationsResponse>
    {
    }
}