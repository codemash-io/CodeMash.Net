using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts.Api;

namespace CodeMash.Membership.Services
{
    public partial class CodeMashMembershipService
    {
        public RegisterUserResponse RegisterUser(RegisterUserRequest registerUserData)
        {
            return Client.Post<RegisterUserResponse>(registerUserData);
        }

        public async Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest registerUserData)
        {
            return await Client.PostAsync<RegisterUserResponse>(registerUserData);
        }
        
        public RegisterGuestUserResponse RegisterGuestUser(RegisterGuestUserRequest registerUserData)
        {
            return Client.Post<RegisterGuestUserResponse>(registerUserData);
        }

        public async Task<RegisterGuestUserResponse> RegisterGuestUserAsync(RegisterGuestUserRequest registerUserData)
        {
            return await Client.PostAsync<RegisterGuestUserResponse>(registerUserData);
        }

        public InviteUserResponse InviteUser(InviteUserRequest inviteUserData)
        {
            return Client.Post<InviteUserResponse>(inviteUserData);
        }

        public async Task<InviteUserResponse> InviteUserAsync(InviteUserRequest inviteUserData)
        {
            return await Client.PostAsync<InviteUserResponse>(inviteUserData);
        }
    }
}