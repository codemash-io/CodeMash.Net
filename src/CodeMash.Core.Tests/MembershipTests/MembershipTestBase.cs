using System;
using System.Collections.Generic;
using CodeMash.Interfaces.Membership;
using Isidos.CodeMash.ServiceContracts.Api;

namespace CodeMash.Core.Tests
{
    public abstract class MembershipTestBase : TestBase
    {
        protected IMembershipService Service { get; set; }
        
        protected Dictionary<string, Guid> RegisteredUsers { get; set; } = new Dictionary<string, Guid>();

        protected RegisterUserRequest RegisterUserRequest;

        protected RegisterUserResponse RegisterUser(string prefix, bool autoLogin = false)
        {
            var request = new RegisterUserRequest
            {
                Email = prefix + "_sdk@test.com",
                FirstName = "SDK",
                LastName = "User",
                DisplayName = "SDK User",
                Password = "sdk",
                AutoLogin = autoLogin
            };
            RegisterUserRequest = request;

            var response = Service.RegisterUser(request);
            Guid.TryParse(response.UserId, out var parsedUserId);
            RegisteredUsers[TestContext.TestName] = parsedUserId;

            return response;
        }
    }
}