using System.Threading.Tasks;
using CodeMash.ServiceContracts.Api;

namespace CodeMash.Membership.Services
{
    public partial class CodeMashMembershipService
    {
        public CreatePasswordResetResponse CreatePasswordReset(CreatePasswordResetRequest request)
        {
            return Client.Post<CreatePasswordResetResponse>(request);
        }

        public async Task<CreatePasswordResetResponse> CreatePasswordResetAsync(CreatePasswordResetRequest request)
        {
            return await Client.PostAsync<CreatePasswordResetResponse>(request);
        }

        public ValidatePasswordTokenResponse CheckPasswordResetToken(ValidatePasswordTokenRequest request)
        {
            return Client.Get<ValidatePasswordTokenResponse>(request);
        }

        public async Task<ValidatePasswordTokenResponse> CheckPasswordResetTokenAsync(ValidatePasswordTokenRequest request)
        {
            return await Client.GetAsync<ValidatePasswordTokenResponse>(request);
        }

        public void ResetPassword(ResetPasswordRequest request)
        {
            Client.Post(request);
        }

        public async Task ResetPasswordAsync(ResetPasswordRequest request)
        {
            await Client.PostAsync(request);
        }
    }
}