using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

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

        public ResetPasswordResponse ResetPassword(ResetPasswordRequest request)
        {
            return Client.Post<ResetPasswordResponse>(request);
        }

        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            return await Client.PostAsync<ResetPasswordResponse>(request);
        }
    }
}