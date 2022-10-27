using System.Threading.Tasks;
using CodeMash.ServiceContracts.Api;

namespace CodeMash.Interfaces.Membership
{
    public partial interface IMembershipService
    {
        AuthResponse AuthenticateCredentials(string userName, string password, bool permanentSession = false);
        
        Task<AuthResponse> AuthenticateCredentialsAsync(string userName, string password, bool permanentSession = false);
        
        AuthResponse Logout(string bearerToken = null);
        
        Task<AuthResponse> LogoutAsync(string bearerToken = null);
    }
}