using System;
using System.Linq;
using System.Threading.Tasks;
using CodeMash.Client;
using CodeMash.Exceptions;
using CodeMash.Membership.Services;
using CodeMash.ServiceContracts.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public partial class MembershipUsersTests : MembershipTestBase
    {
        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            
            var client = new CodeMashClient(ApiKey, ProjectId);
            Service = new CodeMashMembershipService(client);
        }

        [TestCleanup]
        public override void TearDown()
        {
            if (RegisteredUsers != null && RegisteredUsers.Any())
            {
                if (RegisteredUsers.ContainsKey(TestContext.TestName))
                {
                    Service.DeleteUser(new DeleteUserRequest
                    {
                        Id = RegisteredUsers[TestContext.TestName]
                    });
                }
            }
        }
        
        [TestMethod]
        public void Can_register_user()
        {
            var request = new RegisterUserRequest
            {
                Email = "Can_register_user_sdk@test.com",
                FirstName = "SDK",
                LastName = "User",
                DisplayName = "SDK User",
                Password = "sdk"
            };

            var response = Service.RegisterUser(request);
            var userIdParsed = Guid.TryParse(response.UserId, out var parsedUserId);
            RegisteredUsers[TestContext.TestName] = parsedUserId;

            Assert.IsTrue(userIdParsed);
            AssertUserRegister(request, response);
        }
        
        [TestMethod]
        public async Task Can_register_user_async()
        {
            var request = new RegisterUserRequest
            {
                Email = "Can_register_user_async_sdk@test.com",
                FirstName = "SDK",
                LastName = "User",
                DisplayName = "SDK User",
                Password = "sdk"
            };

            var response = await Service.RegisterUserAsync(request);
            var userIdParsed = Guid.TryParse(response.UserId, out var parsedUserId);
            RegisteredUsers[TestContext.TestName] = parsedUserId;

            Assert.IsTrue(userIdParsed);
            AssertUserRegister(request, response);
        }

        private void AssertUserRegister(RegisterUserRequest request, RegisterUserResponse response)
        {
            Assert.IsTrue(response.Result);
            Assert.AreEqual(response.UserName, request.Email);
            Assert.IsNotNull(response.ApiKey);
        }
        
        [TestMethod]
        public void Can_get_user()
        {
            RegisterUser("Can_get_user");

            var request = new GetUserRequest { Id = RegisteredUsers[TestContext.TestName] };
            var response = Service.GetUser(request);

            AssertUserGet(request, response);
        }
        
        [TestMethod]
        public async Task Can_get_user_async()
        {
            RegisterUser("Can_get_user_async");

            var request = new GetUserRequest { Id = RegisteredUsers[TestContext.TestName] };
            var response = await Service.GetUserAsync(request);
            
            AssertUserGet(request, response);
        }
        
        private void AssertUserGet(GetUserRequest request, GetUserResponse response)
        {
            Assert.AreEqual(response.Result.Id, request.Id.ToString());
            Assert.AreEqual(response.Result.Email, TestContext.TestName + "_sdk@test.com");
        }
        
        [TestMethod]
        public void Can_update_user()
        {
            RegisterUser("Can_update_user");

            var updateRequest = new UpdateUserRequest
            {
                Id = RegisteredUsers[TestContext.TestName],
                FirstName = "Updated_SDK",
                LastName = "Updated_User",
                DisplayName = "Updated_SDK User",
            };
            var updateResponse = Service.UpdateUser(updateRequest);
            
            var getRequest = new GetUserRequest { Id = RegisteredUsers[TestContext.TestName] };
            var getResponse = Service.GetUser(getRequest);
            
            AssertUserUpdate(updateRequest, updateResponse, getResponse);
        }
        
        [TestMethod]
        public async Task Can_update_user_async()
        {
            RegisterUser("Can_update_user_async");

            var updateRequest = new UpdateUserRequest
            {
                Id = RegisteredUsers[TestContext.TestName],
                FirstName = "Updated_SDK",
                LastName = "Updated_User",
                DisplayName = "Updated_SDK User",
            };
            var updateResponse = await Service.UpdateUserAsync(updateRequest);
            
            var getRequest = new GetUserRequest { Id = RegisteredUsers[TestContext.TestName] };
            var getResponse = await Service.GetUserAsync(getRequest);
            
            AssertUserUpdate(updateRequest, updateResponse, getResponse);
        }
        
        private void AssertUserUpdate(UpdateUserRequest updateRequest, UpdateUserResponse updateResponse, GetUserResponse getResponse)
        {
            Assert.IsTrue(updateResponse.Result);
            Assert.AreEqual(getResponse.Result.Id, updateRequest.Id.ToString());
            Assert.AreEqual(getResponse.Result.FirstName, "Updated_SDK");
            Assert.AreEqual(getResponse.Result.LastName, "Updated_User");
            Assert.AreEqual(getResponse.Result.DisplayName, "Updated_SDK User");
        }
        
        [TestMethod]
        public async Task Cannot_block_blocked_user()
        {
            Exception expectedException = null;
            
            var registerResponse = RegisterUser("Cannot_block_blocked_user");
            await Service.BlockUserAsync(new BlockUserRequest
            {
                Id = Guid.Parse(registerResponse.UserId)
            });

            try
            {
                await Service.BlockUserAsync(new BlockUserRequest
                {
                    Id = Guid.Parse(registerResponse.UserId)
                });
            }
            catch (BusinessException e)
            {
                expectedException = e;
                Assert.IsTrue(e.ErrorCode == "UserAlreadyBlocked");
            }
            
            Assert.IsNotNull(expectedException);
        }
    }
}