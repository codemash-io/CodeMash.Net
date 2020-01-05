using System;
using System.Collections.Generic;
using System.Linq;
using CodeMash.Client;
using CodeMash.Interfaces.Database.Repository;
using CodeMash.Interfaces.Membership;
using CodeMash.Interfaces.Notifications.Push;
using CodeMash.Interfaces.Project;
using CodeMash.Membership.Services;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Core.Tests
{
    public abstract class NotificationTestBase : TestBase
    {
        protected IPushService Service { get; set; }
        
        protected IMembershipService MembershipService { get; set; }
        
        protected Dictionary<string, Guid> RegisteredUsers { get; set; } = new Dictionary<string, Guid>();

        protected RegisterUserResponse RegisterUser(string prefix, int index = 0)
        {
            var request = new RegisterUserRequest
            {
                Email = prefix + index + "_sdk@test.com",
                FirstName = "SDK",
                LastName = "User",
                DisplayName = "SDK User",
                Password = "sdk",
            };

            var response = MembershipService.RegisterUser(request);
            Guid.TryParse(response.UserId, out var parsedUserId);
            RegisteredUsers[TestContext.TestName + index] = parsedUserId;

            return response;
        }

        public override void SetUp()
        {
            base.SetUp();
            MembershipService = new CodeMashMembershipService(new CodeMashClient(ApiKey, ProjectId));
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