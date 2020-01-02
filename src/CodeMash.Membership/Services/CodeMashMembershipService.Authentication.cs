using System.Threading.Tasks;
using CodeMash.Client;
using Isidos.CodeMash.Data;
using ServiceStack;

namespace CodeMash.Membership.Services
{
    public partial class CodeMashMembershipService
    {
        public AuthResponse AuthenticateCredentials(string userName, string password, bool permanentSession = false)
        {
            return Client.Post<AuthResponse>(FormAuthenticateCredentialsRequest(userName, password, permanentSession),
                new CodeMashRequestOptions
                {
                    UnauthenticatedRequest = true
                });
        }

        public async Task<AuthResponse> AuthenticateCredentialsAsync(string userName, string password, bool permanentSession = false)
        {
            return await Client.PostAsync<AuthResponse>(FormAuthenticateCredentialsRequest(userName, password, permanentSession),
                new CodeMashRequestOptions
                {
                    UnauthenticatedRequest = true
                });
        }

        public AuthResponse Logout(string bearerToken = null)
        {
            return Client.Post<AuthResponse>(new Authenticate
            {
                provider = "logout",
            }, new CodeMashRequestOptions { BearerToken = bearerToken });
        }

        public async Task<AuthResponse> LogoutAsync(string bearerToken = null)
        {
            return await Client.PostAsync<AuthResponse>(new Authenticate
            {
                provider = "logout",
            }, new CodeMashRequestOptions { BearerToken = bearerToken });
        }

        private Authenticate FormAuthenticateCredentialsRequest(string userName, string password, bool permanentSession)
        {
            return new Authenticate
            {
                provider = "credentials",
                UserName = userName,
                Password = password,
                RememberMe = permanentSession,
            };
        }
    }
}