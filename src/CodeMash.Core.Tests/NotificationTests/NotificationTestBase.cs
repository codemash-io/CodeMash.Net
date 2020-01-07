using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeMash.Client;
using CodeMash.Interfaces.Database.Repository;
using CodeMash.Interfaces.Membership;
using CodeMash.Interfaces.Notifications.Push;
using CodeMash.Interfaces.Project;
using CodeMash.Membership.Services;
using CodeMash.Notifications.Push.Services;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Core.Tests
{
    public abstract class NotificationTestBase : TestBase
    {
        protected IPushService Service { get; set; }
        
        protected IMembershipService MembershipService { get; set; }
        
        protected Dictionary<string, Guid> RegisteredUsers { get; set; } = new Dictionary<string, Guid>();
        
        protected RegisterDeviceRequest _deviceRequest;
        
        protected string _expoToken;

        protected string RegisterUser(string prefix, int index = 0)
        {
            var email = prefix + index + "_sdk@test.com";

            try
            {
                var userByEmail = MembershipService.GetUser(new GetUserRequest
                {
                    Email = email
                });

                return userByEmail.Result.Id;
            }
            catch (Exception e)
            {
                // ignored
            }
            
            var request = new RegisterUserRequest
            {
                Email = email,
                FirstName = "SDK",
                LastName = "User",
                DisplayName = "SDK User",
                Password = "sdk",
            };

            var response = MembershipService.RegisterUser(request);
            Guid.TryParse(response.UserId, out var parsedUserId);
            RegisteredUsers[TestContext.TestName + index] = parsedUserId;

            return response.UserId;
        }

        protected string RegisterDevice(string userId, bool useExpo = false)
        {
            _deviceRequest.UserId = Guid.Parse(userId);

            if (useExpo)
            {
                return Service.RegisterExpoToken(new RegisterDeviceExpoTokenRequest
                {
                    UserId = Guid.Parse(userId),
                    TimeZone = _deviceRequest.TimeZone,
                    Meta = _deviceRequest.Meta,
                    Token = _expoToken
                }).Result;
            }
            
            return Service.RegisterDevice(_deviceRequest).Result;
        }
        
        protected async Task<string> RegisterDeviceAsync(string userId, bool useExpo = false)
        {
            _deviceRequest.UserId = Guid.Parse(userId);
            if (useExpo)
            {
                return (await Service.RegisterExpoTokenAsync(new RegisterDeviceExpoTokenRequest
                {
                    UserId = Guid.Parse(userId),
                    TimeZone = _deviceRequest.TimeZone,
                    Meta = _deviceRequest.Meta,
                    Token = _expoToken
                })).Result;
            }
            return (await Service.RegisterDeviceAsync(_deviceRequest)).Result;
        }

        public override void SetUp()
        {
            base.SetUp();
            _expoToken = Environment.GetEnvironmentVariable("EXPO_TOKEN");
            
            var client = new CodeMashClient(ApiKey, ProjectId);
            Service = new CodeMashPushService(client);
            MembershipService = new CodeMashMembershipService(client);
            
            
            _deviceRequest = new RegisterDeviceRequest
            {
                TimeZone = "Etc/UTC",
                Meta = new Dictionary<string, string>
                {
                    {"Os", "Android"},
                    {"Brand", "Samsung"}
                }
            };
        }

        public override void TearDown()
        {
            if (RegisteredUsers != null && RegisteredUsers.Any())
            {
                var testRecords = RegisteredUsers.Where(x => x.Key.StartsWith(TestContext.TestName)).Select(x => x.Value).ToList();
                if (testRecords.Any())
                {
                    foreach (var testRecord in testRecords)
                    {
                        MembershipService.DeleteUser(new DeleteUserRequest
                        {
                            Id = testRecord
                        });
                    }
                }
            }
        }
    }
}