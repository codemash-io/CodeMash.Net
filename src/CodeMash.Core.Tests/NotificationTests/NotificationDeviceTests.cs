using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CodeMash.Client;
using CodeMash.Notifications.Push.Services;
using Isidos.CodeMash.ServiceContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public partial class NotificationTests : NotificationTestBase
    {
        private RegisterDeviceRequest _deviceRequest;
        private string _expoToken;
        
        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            
            var client = new CodeMashClient(ApiKey, ProjectId);
            Service = new CodeMashPushService(client);

            _deviceRequest = new RegisterDeviceRequest
            {
                TimeZone = "Etc/UTC",
                Meta = new Dictionary<string, string>
                {
                    {"Os", "Android"},
                    {"Brand", "Samsung"}
                }
            };
            _expoToken = Environment.GetEnvironmentVariable("EXPO_TOKEN");
        }

        [TestCleanup]
        public override void TearDown()
        {
            base.TearDown();
        }

        private string RegisterDevice(string userId, bool useExpo = false)
        {
            _deviceRequest.UserId = Guid.Parse(userId);

            if (useExpo)
            {
                var response = Service.RegisterExpoToken(new RegisterDeviceExpoTokenRequest
                {
                    UserId = Guid.Parse(userId),
                    TimeZone = _deviceRequest.TimeZone,
                    Meta = _deviceRequest.Meta,
                    Token = _expoToken
                });
            }
            
            return Service.RegisterDevice(_deviceRequest).Result;
        }
        
        private async Task<string> RegisterDeviceAsync(string userId, bool useExpo = false)
        {
            _deviceRequest.UserId = Guid.Parse(userId);
            if (useExpo)
            {
                var response = await Service.RegisterExpoTokenAsync(new RegisterDeviceExpoTokenRequest
                {
                    UserId = Guid.Parse(userId),
                    TimeZone = _deviceRequest.TimeZone,
                    Meta = _deviceRequest.Meta,
                    Token = _expoToken
                });
            }
            
            return (await Service.RegisterDeviceAsync(_deviceRequest)).Result;
        }
        
        [TestMethod]
        public void Can_register_device()
        {
            var userResponse = RegisterUser("Can_register_device");
            var deviceIdResponse = RegisterDevice(userResponse.UserId);
            
            Assert.IsNotNull(deviceIdResponse);
            Assert.IsTrue(Guid.TryParse(deviceIdResponse, out var deviceId));
        }
        
        [TestMethod]
        public async Task Can_register_device_async()
        {
            var userResponse = RegisterUser("Can_register_device_async");
            var deviceIdResponse = await RegisterDeviceAsync(userResponse.UserId);
            
            Assert.IsNotNull(deviceIdResponse);
            Assert.IsTrue(Guid.TryParse(deviceIdResponse, out var deviceId));
        }
        
        [TestMethod]
        public void Can_register_expo_token_with_device()
        {
            var userResponse = RegisterUser("Can_register_expo_token_with_device");
            var deviceIdResponse = RegisterDevice(userResponse.UserId, true);
            
            Assert.IsNotNull(deviceIdResponse);
            Assert.IsTrue(Guid.TryParse(deviceIdResponse, out var deviceId));
        }
        
        [TestMethod]
        public async Task Can_register_expo_token_with_device_async()
        {
            var userResponse = RegisterUser("Can_register_expo_token_with_device_async");
            var deviceIdResponse = await RegisterDeviceAsync(userResponse.UserId, true);
            
            Assert.IsNotNull(deviceIdResponse);
            Assert.IsTrue(Guid.TryParse(deviceIdResponse, out var deviceId));
        }
        
        [TestMethod]
        public void Can_register_expo_token_after_device_registered()
        {
            var userResponse = RegisterUser("Can_register_expo_token_after_device_registered");
            var deviceIdResponse = RegisterDevice(userResponse.UserId);
            
            var response = Service.RegisterExpoToken(new RegisterDeviceExpoTokenRequest
            {
                UserId = Guid.Parse(userResponse.UserId),
                DeviceId = Guid.Parse(deviceIdResponse),
                Token = _expoToken
            });
            
            Assert.IsNotNull(response.Result);
            Assert.IsTrue(Guid.TryParse(response.Result, out var deviceId));
        }
        
        [TestMethod]
        public async Task Can_register_expo_token_after_device_registered_async()
        {
            var userResponse = RegisterUser("Can_register_expo_token_after_device_registered_async");
            var deviceIdResponse = await RegisterDeviceAsync(userResponse.UserId);
            
            var response = await Service.RegisterExpoTokenAsync(new RegisterDeviceExpoTokenRequest
            {
                UserId = Guid.Parse(userResponse.UserId),
                DeviceId = Guid.Parse(deviceIdResponse),
                Token = _expoToken
            });
            
            Assert.IsNotNull(response.Result);
            Assert.IsTrue(Guid.TryParse(response.Result, out var deviceId));
        }
        
        [TestMethod]
        public void Can_delete_device_token()
        {
            var userResponse = RegisterUser("Can_delete_device_token");
            var deviceIdResponse = RegisterDevice(userResponse.UserId, true);
            
            var response = Service.DeleteToken(new DeleteDeviceTokenRequest
            {
                Id = Guid.Parse(deviceIdResponse)
            });
            
            Assert.IsTrue(response.Result);
        }
        
        [TestMethod]
        public async Task Can_delete_device_token_async()
        {
            var userResponse = RegisterUser("Can_delete_device_token_async");
            var deviceIdResponse = await RegisterDeviceAsync(userResponse.UserId, true);
            
            var response = await Service.DeleteTokenAsync(new DeleteDeviceTokenRequest
            {
                Id = Guid.Parse(deviceIdResponse)
            });
            
            Assert.IsTrue(response.Result);
        }
        
        [TestMethod]
        public void Can_update_device_meta()
        {
            var userResponse = RegisterUser("Can_update_device_meta");
            var deviceIdResponse = RegisterDevice(userResponse.UserId);
            
            var response = Service.UpdateDeviceMeta(new UpdateDeviceMetaRequest
            {
                Id = Guid.Parse(deviceIdResponse),
                Meta = new Dictionary<string, string>
                {
                    { "Os", "iOs" }
                }
            });
            
            Assert.IsTrue(response.Result);
        }
        
        [TestMethod]
        public async Task Can_update_device_meta_async()
        {
            var userResponse = RegisterUser("Can_update_device_meta_async");
            var deviceIdResponse = await RegisterDeviceAsync(userResponse.UserId);
            
            var response = await Service.UpdateDeviceMetaAsync(new UpdateDeviceMetaRequest
            {
                Id = Guid.Parse(deviceIdResponse),
                Meta = new Dictionary<string, string>
                {
                    { "Os", "iOs" }
                }
            });
            
            Assert.IsTrue(response.Result);
        }
        
        [TestMethod]
        public void Can_update_device_timezone()
        {
            var userResponse = RegisterUser("Can_update_device_timezone");
            var deviceIdResponse = RegisterDevice(userResponse.UserId);
            
            var response = Service.UpdateDeviceTimeZone(new UpdateDeviceTimeZoneRequest
            {
                Id = Guid.Parse(deviceIdResponse),
                TimeZone = "America/Los_Angeles"
            });
            
            Assert.IsTrue(response.Result);
        }
        
        [TestMethod]
        public async Task Can_update_device_timezone_async()
        {
            var userResponse = RegisterUser("Can_update_device_meta_async");
            var deviceIdResponse = await RegisterDeviceAsync(userResponse.UserId);
            
            var response = await Service.UpdateDeviceTimeZoneAsync(new UpdateDeviceTimeZoneRequest
            {
                Id = Guid.Parse(deviceIdResponse),
                TimeZone = "America/Los_Angeles"
            });
            
            Assert.IsTrue(response.Result);
        }
        
        [TestMethod]
        public void Can_update_device_user()
        {
            var userResponse = RegisterUser("Can_update_device_user");
            var deviceIdResponse = RegisterDevice(userResponse.UserId);
            
            var user2Response = RegisterUser("Can_update_device_user", 1);
            var response = Service.UpdateDeviceUser(new UpdateDeviceUserRequest
            {
                Id = Guid.Parse(deviceIdResponse),
                UserId = Guid.Parse(user2Response.UserId)
            });
            
            Assert.IsTrue(response.Result);
        }
        
        [TestMethod]
        public async Task Can_update_device_user_async()
        {
            var userResponse = RegisterUser("Can_update_device_user_async");
            var deviceIdResponse = await RegisterDeviceAsync(userResponse.UserId);
            
            var user2Response = RegisterUser("Can_update_device_user_async", 1);
            var response = await Service.UpdateDeviceUserAsync(new UpdateDeviceUserRequest
            {
                Id = Guid.Parse(deviceIdResponse),
                UserId = Guid.Parse(user2Response.UserId)
            });
            
            Assert.IsTrue(response.Result);
        }
        
        [TestMethod]
        public void Can_delete_device()
        {
            var userResponse = RegisterUser("Can_update_device_timezone");
            var deviceIdResponse = RegisterDevice(userResponse.UserId);
            
            var response = Service.DeleteDevice(new DeleteDeviceRequest
            {
                Id = Guid.Parse(deviceIdResponse),
            });
            
            Assert.IsTrue(response.Result);
        }
        
        [TestMethod]
        public async Task Can_delete_device_async()
        {
            var userResponse = RegisterUser("Can_update_device_meta_async");
            var deviceIdResponse = await RegisterDeviceAsync(userResponse.UserId);
            
            var response = await Service.DeleteDeviceAsync(new DeleteDeviceRequest
            {
                Id = Guid.Parse(deviceIdResponse),
            });
            
            Assert.IsTrue(response.Result);
        }
        
        [TestMethod]
        public void Can_get_device()
        {
            var userResponse = RegisterUser("Can_get_device");
            var deviceIdResponse = RegisterDevice(userResponse.UserId);
            
            Thread.Sleep(1000);
            
            var response = Service.GetDevice(new GetDeviceRequest
            {
                Id = Guid.Parse(deviceIdResponse),
            });
            
            Assert.AreEqual(response.Result.Id, deviceIdResponse);
        }
        
        [TestMethod]
        public async Task Can_get_device_async()
        {
            var userResponse = RegisterUser("Can_get_device_async");
            var deviceIdResponse = await RegisterDeviceAsync(userResponse.UserId);

            Thread.Sleep(1000);
            
            var response = await Service.GetDeviceAsync(new GetDeviceRequest
            {
                Id = Guid.Parse(deviceIdResponse),
            });
            
            Assert.AreEqual(response.Result.Id, deviceIdResponse);
        }
    }
}