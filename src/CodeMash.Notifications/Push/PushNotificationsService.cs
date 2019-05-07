using System;
using CodeMash.Interfaces;
using CodeMash.ServiceModel.Requests.Notifications.Push;
using CodeMash.ServiceModel.Responses.Notifications.Push;

namespace CodeMash.Notifications.Push
{
    public class PushNotificationsService : IPushNotificationsService
    {
        public ICodeMashSettings CodeMashSettings { get; set; }


        public CreateNotificationDeviceResponse CreateNotificationDevice(CreateNotificationDevice request)
        {
            if (CodeMashSettings == null)
            {
                throw new ArgumentNullException("CodeMashSettings", "CodeMash settings is not set");
            }

            var response =
                CodeMashSettings.Client.Post<CreateNotificationDeviceResponse>(request);

            return response;
        }

        public CreateNotificationResponse CreateNotification(CreateNotification request)
        {
            if (CodeMashSettings == null)
            {
                throw new ArgumentNullException("CodeMashSettings", "CodeMash settings is not set");
            }

            var response =
                CodeMashSettings.Client.Post<CreateNotificationResponse>(request);

            return response;
        }

    }
}