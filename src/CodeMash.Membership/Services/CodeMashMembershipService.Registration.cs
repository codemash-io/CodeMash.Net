using System.Threading.Tasks;
using CodeMash.ServiceContracts.Api;

namespace CodeMash.Membership.Services
{
    public partial class CodeMashMembershipService
    {
        public RegisterUserV2Response RegisterUser(RegisterUserRequest registerUserData)
        {
            return Client.Post<RegisterUserV2Response>(registerUserData);
        }

        public async Task<RegisterUserV2Response> RegisterUserAsync(RegisterUserRequest registerUserData)
        {
            return await Client.PostAsync<RegisterUserV2Response>(registerUserData);
        }
        
        public RegisterGuestUserResponse RegisterGuestUser(RegisterGuestUserRequest registerUserData)
        {
            return Client.Post<RegisterGuestUserResponse>(registerUserData);
        }

        public async Task<RegisterGuestUserResponse> RegisterGuestUserAsync(RegisterGuestUserRequest registerUserData)
        {
            return await Client.PostAsync<RegisterGuestUserResponse>(registerUserData);
        }

        public InviteUserV2Response InviteUser(InviteUserRequest inviteUserData)
        {
            return Client.Post<InviteUserV2Response>(inviteUserData);
        }

        public async Task<InviteUserV2Response> InviteUserAsync(InviteUserRequest inviteUserData)
        {
            return await Client.PostAsync<InviteUserV2Response>(inviteUserData);
        }
    }
}