using System;
using System.Collections.Generic;
using CodeMash.Interfaces;
using CodeMash.Common;
using CodeMash.Notifications.Push;
using Isidos.CodeMash.ServiceContracts;
using NSubstitute;
using NUnit.Framework;

namespace CodeMash.Core.Tests
{
    [TestFixture]
    public class PushNotificationsTests
    {
        
        // TODO : rise exception if module is disabled
        
        
        [Test]
        [Category("Notifications.Push")]
        public void Can_create_notification_device()
        {
            var deviceId = Guid.NewGuid();
            
            var mock = Substitute.For<ICodeMashSettings>();
            
            mock.Client.Post(Arg.Any<RegisterDevice>())
                .Returns(info => new RegisterDeviceResponse {Result = deviceId});
            
            var pushNotificationsService = new PushNotificationsService
            {
                CodeMashSettings = mock
            };
            
            var response = pushNotificationsService.RegisterDevice(new RegisterDevice());
            
            Assert.AreEqual(deviceId, response.Result);

        }
        
        [Test]
        [Category("Notifications.Push")]
        [Category("Integration")]
        public void Can_create_notification_device_integration()
        {
            var pushNotificationsService = new PushNotificationsService
            {
                CodeMashSettings = new CodeMashSettingsCore(null, "appsettings.Production.json")
            };

            var request = new RegisterDevice
            {
                TimeZone = "timzone"
            };

            var response = pushNotificationsService.RegisterDevice(request);

            Assert.IsInstanceOf<Guid>(response.Result);

        }
        
        
        [Test]
        [Category("Notifications.Push")]
        public void Can_save_push_notification()
        {
            var mock = Substitute.For<ICodeMashSettings>();
            
            mock.Client.Post(Arg.Any<SendPushNotification>())
                .Returns(info => new SendPushNotificationResponse { Result = "NotificationId" });
            
            var pushNotificationsService = new PushNotificationsService
            {
                CodeMashSettings = mock
            };
            
            var result = pushNotificationsService.Send(new SendPushNotification());
            
            Assert.AreEqual("NotificationId", result.Result);

        }
        
        [Test]
        [Category("Notifications.Push")]
        [Category("Integration")]
        public void Can_save_push_notification_integration()
        {
            var pushNotificationsService = new PushNotificationsService
            {
                CodeMashSettings = new CodeMashSettingsCore(null, "appsettings.Production.json")
            };
            
            var createDeviceResponse = pushNotificationsService.RegisterDevice(new RegisterDevice());

            var request = new SendPushNotification
            {
                TemplateName = "Template",
                Devices = new List<Guid> { createDeviceResponse.Result },
                Tokens = new Dictionary<string, string> {{"customData", "ok"}},
                IsNonPushable = true
            };
            
            var response = pushNotificationsService.Send(request);
            
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
        }
        
    }
}