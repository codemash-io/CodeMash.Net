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

        public RegisterDeviceResponse RegisterDevice(RegisterDevice request)
        {
            AssertItHasSettings();

            var response =
                CodeMashSettings.Client.Post(request);

            return response;
        }

        public GetNotificationsResponse GetAll(GetNotifications request)
        {
            AssertItHasSettings();

            return CodeMashSettings.Client.Post(request);
        }
        
        public GetNotificationResponse Get(GetNotification request)
        {
            AssertItHasSettings();

            return CodeMashSettings.Client.Post(request);
        }

        public GetNotificationTemplatesResponse GetTemplates(GetNotificationTemplates request)
        {
            AssertItHasSettings();

            return CodeMashSettings.Client.Get(request);
        }

        public GetNotificationTemplateResponse GetTemplate(GetNotificationTemplate request)
        {
            AssertItHasSettings();

            return CodeMashSettings.Client.Get(request);
        }

        public SendPushNotificationResponse Send(SendPushNotification request)
        {
            AssertItHasSettings();

            var response =
                CodeMashSettings.Client.Post(request);

            return response;
        }

    }
}