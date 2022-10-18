using System;
using System.Collections.Generic;
using System.Linq;
using CodeMash.Client;
using CodeMash.Interfaces.Membership;
using CodeMash.Interfaces.Notifications.Email;
using CodeMash.Membership.Services;
using CodeMash.Notifications.Email.Services;
using Isidos.CodeMash.ServiceContracts.Api;

namespace CodeMash.Core.Tests
{
    public abstract class EmailTestBase : TestBase
    {
        protected IEmailService Service { get; set; }
        
        protected IMembershipService MembershipService { get; set; }
        
        protected Dictionary<string, Guid> RegisteredUsers { get; set; } = new Dictionary<string, Guid>();
        

        protected string RegisterUser(string email, int index = 0)
        {
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

        public override void SetUp()
        {
            base.SetUp();
            
            var client = new CodeMashClient(ApiKey, ProjectId);
            Service = new CodeMashEmailService(client);
            MembershipService = new CodeMashMembershipService(client);
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