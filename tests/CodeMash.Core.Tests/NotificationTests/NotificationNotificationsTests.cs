using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class NotificationNotificationsTests : NotificationTestBase
    {
        private Guid _templateId;
        
        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            
            _templateId = Guid.Parse(Config["PushTemplateId"]);
        }

        [TestCleanup]
        public override void TearDown()
        {
            base.TearDown();
        }

        private void SendStaticNotification(string userId, string deviceId)
        {
            var sendResponse = Service.SendPushNotification(new SendPushNotificationRequest
            {
                TemplateId = _templateId,
                Users = new List<Guid> { Guid.Parse(userId) },
                Devices = new List<Guid> { Guid.Parse(deviceId) },
                IsNonPushable = true
            });
        }

        [TestMethod]
        public void Can_send_notification_by_devices()
        {
            var userResponse = RegisterUser("Can_send_notification_by_devices");
            var deviceIdResponse = RegisterDevice(userResponse, true);
            
            var response = Service.SendPushNotification(new SendPushNotificationRequest
            {
                TemplateId = _templateId,
                Devices = new List<Guid> { Guid.Parse(deviceIdResponse) }
            });
            
            Assert.IsTrue(response.Result);
        }
        
        [TestMethod]
        public async Task Can_send_notification_by_devices_async()
        {
            var userResponse = RegisterUser("Can_send_notification_by_devices_async");
            var deviceIdResponse = await RegisterDeviceAsync(userResponse, true);
            
            var response = await Service.SendPushNotificationAsync(new SendPushNotificationRequest
            {
                TemplateId = _templateId,
                Devices = new List<Guid> { Guid.Parse(deviceIdResponse) }
            });
            
            Assert.IsTrue(response.Result);
        }
        
        [TestMethod]
        public void Can_send_notification_by_users()
        {
            var userResponse = RegisterUser("Can_send_notification_by_users");
            var deviceIdResponse = RegisterDevice(userResponse, true);
            
            var response = Service.SendPushNotification(new SendPushNotificationRequest
            {
                TemplateId = _templateId,
                Users = new List<Guid> { Guid.Parse(userResponse) }
            });
            
            Assert.IsTrue(response.Result);
        }
        
        [TestMethod]
        public async Task Can_send_notification_by_users_async()
        {
            var userResponse = RegisterUser("Can_send_notification_by_users_async");
            var deviceIdResponse = await RegisterDeviceAsync(userResponse, true);
            
            var response = await Service.SendPushNotificationAsync(new SendPushNotificationRequest
            {
                TemplateId = _templateId,
                Users = new List<Guid> { Guid.Parse(userResponse) }
            });
            
            Assert.IsTrue(response.Result);
        }
        
        [TestMethod]
        public void Can_get_notifications_by_device()
        {
            var userResponse = RegisterUser("Can_get_notifications_by_device");
            var deviceIdResponse = RegisterDevice(userResponse, true);
            SendStaticNotification(userResponse, deviceIdResponse);
            
            Thread.Sleep(5000);
            
            var response = Service.GetNotifications(new GetNotificationsRequest
            {
                DeviceId = Guid.Parse(deviceIdResponse)
            });
            
            Assert.IsNotNull(response.Result);
            Assert.IsTrue(response.Result.Count > 0);
        }
        
        [TestMethod]
        public async Task Can_get_notifications_by_device_async()
        {
            var userResponse = RegisterUser("Can_get_notifications_by_device_async");
            var deviceIdResponse = await RegisterDeviceAsync(userResponse, true);
            SendStaticNotification(userResponse, deviceIdResponse);
            
            Thread.Sleep(5000);
            
            var response = await Service.GetNotificationsAsync(new GetNotificationsRequest
            {
                DeviceId = Guid.Parse(deviceIdResponse)
            });
            
            Assert.IsNotNull(response.Result);
            Assert.IsTrue(response.Result.Count > 0);
        }
        
        [TestMethod]
        public void Can_get_notifications_by_user()
        {
            var userResponse = RegisterUser("Can_get_notifications_by_user");
            var deviceIdResponse = RegisterDevice(userResponse, true);
            SendStaticNotification(userResponse, deviceIdResponse);
            
            Thread.Sleep(5000);
            
            var response = Service.GetNotifications(new GetNotificationsRequest
            {
                UserId = Guid.Parse(userResponse)
            });
            
            Assert.IsNotNull(response.Result);
            Assert.IsTrue(response.Result.Count > 0);
        }
        
        [TestMethod]
        public async Task Can_get_notifications_by_user_async()
        {
            var userResponse = RegisterUser("Can_get_notifications_by_user_async");
            var deviceIdResponse = await RegisterDeviceAsync(userResponse, true);
            SendStaticNotification(userResponse, deviceIdResponse);
            
            Thread.Sleep(5000);
            
            var response = await Service.GetNotificationsAsync(new GetNotificationsRequest
            {
                UserId = Guid.Parse(userResponse)
            });
            
            Assert.IsNotNull(response.Result);
            Assert.IsTrue(response.Result.Count > 0);
        }
        
        [TestMethod]
        public void Can_get_notification()
        {
            var userResponse = RegisterUser("Can_get_notification");
            var deviceIdResponse = RegisterDevice(userResponse, true);
            SendStaticNotification(userResponse, deviceIdResponse);
            
            Thread.Sleep(5000);
            
            var notificationsResponse = Service.GetNotifications(new GetNotificationsRequest
            {
                UserId = Guid.Parse(userResponse)
            });
            
            var response = Service.GetNotification(new GetNotificationRequest
            {
                Id = notificationsResponse.Result[0].Id
            });
            
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(response.Result.Id, notificationsResponse.Result[0].Id);
        }
        
        [TestMethod]
        public async Task Can_get_notification_async()
        {
            var userResponse = RegisterUser("Can_get_notification_async");
            var deviceIdResponse = await RegisterDeviceAsync(userResponse, true);
            SendStaticNotification(userResponse, deviceIdResponse);
            
            Thread.Sleep(5000);
            
            var notificationsResponse = await Service.GetNotificationsAsync(new GetNotificationsRequest
            {
                UserId = Guid.Parse(userResponse)
            });
            
            var response = await Service.GetNotificationAsync(new GetNotificationRequest
            {
                Id = notificationsResponse.Result[0].Id
            });
            
            Assert.IsNotNull(response.Result);
            Assert.AreEqual(response.Result.Id, notificationsResponse.Result[0].Id);
        }
        
        [TestMethod]
        public void Can_delete_notification()
        {
            var userResponse = RegisterUser("Can_delete_notification");
            var deviceIdResponse = RegisterDevice(userResponse, true);
            SendStaticNotification(userResponse, deviceIdResponse);
            
            Thread.Sleep(5000);
            
            var notificationsResponse = Service.GetNotifications(new GetNotificationsRequest
            {
                UserId = Guid.Parse(userResponse)
            });
            
            var response = Service.DeleteNotification(new DeleteNotificationRequest
            {
                Id = notificationsResponse.Result[0].Id
            });
            
            Assert.IsTrue(response.Result);
        }
        
        [TestMethod]
        public async Task Can_delete_notification_async()
        {
            var userResponse = RegisterUser("Can_delete_notification_async");
            var deviceIdResponse = await RegisterDeviceAsync(userResponse, true);
            SendStaticNotification(userResponse, deviceIdResponse);
            
            Thread.Sleep(5000);
            
            var notificationsResponse = await Service.GetNotificationsAsync(new GetNotificationsRequest
            {
                UserId = Guid.Parse(userResponse)
            });
            
            var response = await Service.DeleteNotificationAsync(new DeleteNotificationRequest
            {
                Id = notificationsResponse.Result[0].Id
            });
            
            Assert.IsTrue(response.Result);
        }
        
        [TestMethod]
        public void Can_mark_notification_as_read()
        {
            var userResponse = RegisterUser("Can_mark_notification_as_read");
            var deviceIdResponse = RegisterDevice(userResponse, true);
            SendStaticNotification(userResponse, deviceIdResponse);
            
            Thread.Sleep(5000);
            
            var notificationsResponse = Service.GetNotifications(new GetNotificationsRequest
            {
                UserId = Guid.Parse(userResponse)
            });
            
            var response = Service.ReadNotification(new MarkNotificationAsReadRequest
            {
                NotificationId = notificationsResponse.Result[0].Id
            });
            
            Assert.IsTrue(response.Result);
        }
        
        [TestMethod]
        public async Task Can_mark_notification_as_read_async()
        {
            var userResponse = RegisterUser("Can_mark_notification_as_read_async");
            var deviceIdResponse = await RegisterDeviceAsync(userResponse, true);
            SendStaticNotification(userResponse, deviceIdResponse);
            
            Thread.Sleep(5000);
            
            var notificationsResponse = await Service.GetNotificationsAsync(new GetNotificationsRequest
            {
                UserId = Guid.Parse(userResponse)
            });
            
            var response = await Service.ReadNotificationAsync(new MarkNotificationAsReadRequest
            {
                NotificationId = notificationsResponse.Result[0].Id
            });
            
            Assert.IsTrue(response.Result);
        }
    }
}