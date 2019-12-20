using System.Linq;
using System.Threading.Tasks;
using CodeMash.Client;
using CodeMash.Interfaces.Client;
using CodeMash.Membership.Services;
using Isidos.CodeMash.Data;
using Isidos.CodeMash.ServiceContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class MembershipAuthenticationTests : MembershipTestBase
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
        public void Can_auto_authenticate_on_register()
        {
            RegisterUser("Can_authenticate_with_credentials", true);
            var resp = Service.Client.Response;
            
            Assert.IsTrue(resp.Headers?.ContainsKey("Set-Cookie") ?? false);
        }
        
        [TestMethod]
        public void Can_register_without_auto_authenticate()
        {
            RegisterUser("Can_authenticate_with_credentials", false);
            var resp = Service.Client.Response;
            
            Assert.IsFalse(resp.Headers?.ContainsKey("Set-Cookie") ?? false);
        }
        
        [TestMethod]
        public void Can_authenticate_with_credentials()
        {
            var registerResponse = RegisterUser("Can_authenticate_with_credentials", false);
            var authResponse = Service.AuthenticateCredentials(RegisterUserRequest.Email, RegisterUserRequest.Password);
            var resp = Service.Client.Response;

            AssertCanAuthenticateWithCredentials(authResponse, registerResponse, resp);
        }
        
        [TestMethod]
        public async Task Can_authenticate_with_credentials_async()
        {
            var registerResponse = RegisterUser("Can_authenticate_with_credentials_async", false);
            var authResponse = Service.AuthenticateCredentials(RegisterUserRequest.Email, RegisterUserRequest.Password);
            var resp = Service.Client.Response;

            AssertCanAuthenticateWithCredentials(authResponse, registerResponse, resp);
        }
        
        private void AssertCanAuthenticateWithCredentials(AuthResponse authResponse, RegisterUserResponse registerResponse, ICodeMashResponse resp)
        {
            Assert.IsNotNull(authResponse.SessionId);
            Assert.AreEqual((object) authResponse.BearerToken, registerResponse.ApiKey);
            Assert.IsTrue((resp.Headers?.ContainsKey("Set-Cookie") ?? false) && resp.Headers["Set-Cookie"].Contains(authResponse.SessionId));
        }
    }
}