using System;
using System.Collections.Generic;
using CodeMash.Interfaces;
using CodeMash.Common;
using CodeMash.Notifications.Push;
using Isidos.CodeMash.ServiceContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class PushNotificationsTests
    {
        
        // TODO : rise exception if module is disabled
        // TODO : throw nice message when module is disabled or not established yet
        
        /*
        [TestMethod]
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
        
        [TestMethod]
        public void Can_register_notification_device_integration()
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

            response.Result.ShouldBe<Guid>();

        }
        
        
        [TestMethod]
        public void Can_send_push_notification()
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
        
        [TestMethod]
        public void Can_send_push_notification_integration()
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
        */
    }
}