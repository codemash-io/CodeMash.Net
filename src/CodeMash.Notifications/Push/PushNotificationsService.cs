using System;
using CodeMash.Interfaces;
using Isidos.CodeMash.ServiceContracts;
using Isidos.CodeMash.Utils;

namespace CodeMash.Notifications.Push
{
    public class PushNotificationsService : IPushNotificationsService
    {
        private void AssertItHasSettings()
        {
            if (CodeMashSettings == null)
            {
                throw new ArgumentNullException(nameof(CodeMashSettings), "CodeMash settings is not set");
            }  
        }
        
        public ICodeMashSettings CodeMashSettings { get; set; }

        public CreateNotificationDeviceResponse CreateNotificationDevice(CreateNotificationDevice request)
        {
            AssertItHasSettings();

            var response =
                CodeMashSettings.Client.Post<CreateNotificationDeviceResponse>(request);

            return response;
        }

        public GetNotificationsResponse GetNotifications(GetNotifications request)
        {
            AssertItHasSettings();

            return CodeMashSettings.Client.Post(request);
        }
        
        public GetNotificationResponse GetNotification(GetNotification request)
        {
            AssertItHasSettings();

            return CodeMashSettings.Client.Post(request);
        }

        public CreateNotificationResponse CreateNotification(CreateNotification request)
        {
            AssertItHasSettings();

            var response =
                CodeMashSettings.Client.Post<CreateNotificationResponse>(request);

            return response;
        }

    }
}