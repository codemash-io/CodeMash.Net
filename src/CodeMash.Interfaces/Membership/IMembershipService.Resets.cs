using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Interfaces.Membership
{
    public partial interface IMembershipService
    {
        CreatePasswordResetResponse CreatePasswordReset(CreatePasswordResetRequest request);
        
        Task<CreatePasswordResetResponse> CreatePasswordResetAsync(CreatePasswordResetRequest request);
        
        ValidatePasswordTokenResponse CheckPasswordResetToken(ValidatePasswordTokenRequest request);
        
        Task<ValidatePasswordTokenResponse> CheckPasswordResetTokenAsync(ValidatePasswordTokenRequest request);
        
        ResetPasswordResponse ResetPassword(ResetPasswordRequest request);
        
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);
    }
}