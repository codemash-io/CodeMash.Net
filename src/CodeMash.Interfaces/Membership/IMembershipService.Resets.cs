using System.Threading.Tasks;
using CodeMash.ServiceContracts.Api;

namespace CodeMash.Interfaces.Membership
{
    public partial interface IMembershipService
    {
        CreatePasswordResetResponse CreatePasswordReset(CreatePasswordResetRequest request);
        
        Task<CreatePasswordResetResponse> CreatePasswordResetAsync(CreatePasswordResetRequest request);
        
        ValidatePasswordTokenResponse CheckPasswordResetToken(ValidatePasswordTokenRequest request);
        
        Task<ValidatePasswordTokenResponse> CheckPasswordResetTokenAsync(ValidatePasswordTokenRequest request);
        
        void ResetPassword(ResetPasswordRequest request);
        
        Task ResetPasswordAsync(ResetPasswordRequest request);
    }
}