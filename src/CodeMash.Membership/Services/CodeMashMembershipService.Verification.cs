using System.Threading.Tasks;
using CodeMash.ServiceContracts.Api;

namespace CodeMash.Membership.Services
{
    public partial class CodeMashMembershipService
    {
        public void VerifyUser(VerifyUserRequest request)
        {
            Client.Put(request);
        }

        public async Task VerifyUserAsync(VerifyUserRequest request)
        {
            await Client.PutAsync(request);
        }

        public ValidateInvitationTokenResponse CheckUserInvitationToken(ValidateInvitationTokenRequest request)
        {
            return Client.Get<ValidateInvitationTokenResponse>(request);
        }

        public async Task<ValidateInvitationTokenResponse> CheckUserInvitationTokenAsync(ValidateInvitationTokenRequest request)
        {
            return await Client.GetAsync<ValidateInvitationTokenResponse>(request);
        }

        public void VerifyUserInvitation(VerifyUserInvitationRequest request)
        {
            Client.Put(request);
        }

        public async Task VerifyUserInvitationAsync(VerifyUserInvitationRequest request)
        {
            await Client.PutAsync(request);
        }
    }
}