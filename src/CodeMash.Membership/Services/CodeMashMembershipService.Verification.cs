using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Membership.Services
{
    public partial class CodeMashMembershipService
    {
        public VerifyUserResponse VerifyUser(VerifyUserRequest request)
        {
            return Client.Put<VerifyUserResponse>(request);
        }

        public async Task<VerifyUserResponse> VerifyUserAsync(VerifyUserRequest request)
        {
            return await Client.PutAsync<VerifyUserResponse>(request);
        }

        public ValidateInvitationTokenResponse CheckUserInvitationToken(ValidateInvitationTokenRequest request)
        {
            return Client.Get<ValidateInvitationTokenResponse>(request);
        }

        public async Task<ValidateInvitationTokenResponse> CheckUserInvitationTokenAsync(ValidateInvitationTokenRequest request)
        {
            return await Client.GetAsync<ValidateInvitationTokenResponse>(request);
        }

        public VerifyUserInvitationResponse VerifyUserInvitation(VerifyUserInvitationRequest request)
        {
            return Client.Put<VerifyUserInvitationResponse>(request);
        }

        public async Task<VerifyUserInvitationResponse> VerifyUserInvitationAsync(VerifyUserInvitationRequest request)
        {
            return await Client.PutAsync<VerifyUserInvitationResponse>(request);
        }
    }
}