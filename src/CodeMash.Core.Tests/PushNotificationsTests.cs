using System;
using System.Collections.Generic;
using CodeMash.Common;
using CodeMash.Interfaces;
using CodeMash.Notifications.Push;
using Isidos.CodeMash.ServiceContracts;
using NSubstitute;
using NUnit.Framework;

namespace CodeMash.Core.Tests
{
    [TestFixture]
    public class PushNotificationsTests
    {
        [Test]
        [Category("Notifications.Push")]
        public void Can_create_notification_device()
        {
            var deviceId = Guid.NewGuid();
            
            var mock = Substitute.For<ICodeMashSettings>();
            
            mock.Client.Post<CreateNotificationDeviceResponse>(Arg.Any<CreateNotificationDevice>())
                .Returns(info => new CreateNotificationDeviceResponse {Result = deviceId});
            
            var pushNotificationsService = new PushNotificationsService
            {
                CodeMashSettings = mock
            };
            
            var response = pushNotificationsService.CreateNotificationDevice(new CreateNotificationDevice());
            
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

            var request = new CreateNotificationDevice
            {
                TimeZone = "timzone"
            };

            var response = pushNotificationsService.CreateNotificationDevice(request);

            Assert.IsInstanceOf<Guid>(response.Result);

        }
        
        
        [Test]
        [Category("Notifications.Push")]
        public void Can_save_push_notification()
        {
            var mock = Substitute.For<ICodeMashSettings>();
            
            mock.Client.Post<CreateNotificationResponse>(Arg.Any<CreateNotification>())
                .Returns(info => new CreateNotificationResponse {Result = "NotificationId"});
            
            var pushNotificationsService = new PushNotificationsService
            {
                CodeMashSettings = mock
            };
            
            var result = pushNotificationsService.CreateNotification(new CreateNotification());
            
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
            
            var createDeviceResponse = pushNotificationsService.CreateNotificationDevice(new CreateNotificationDevice());

            var request = new CreateNotification
            {
                Title = "title",
                Body = "body",
                Devices = new List<Guid> { createDeviceResponse.Result },
                Data = "{ \"customData\" : \"ok\"}",
            };
            
            var response = pushNotificationsService.CreateNotification(request);
            
            Assert.IsNotNull(response);
            Assert.IsNotNull(response.Result);
        }
        
    }
}