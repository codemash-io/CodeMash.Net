using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts.Api;

namespace CodeMash.Interfaces.Membership
{
    public partial interface IMembershipService
    {
        VerifyUserResponse VerifyUser(VerifyUserRequest request);
        
        Task<VerifyUserResponse> VerifyUserAsync(VerifyUserRequest request);
        
        ValidateInvitationTokenResponse CheckUserInvitationToken(ValidateInvitationTokenRequest request);
        
        Task<ValidateInvitationTokenResponse> CheckUserInvitationTokenAsync(ValidateInvitationTokenRequest request);
        
        VerifyUserInvitationResponse VerifyUserInvitation(VerifyUserInvitationRequest request);
        
        Task<VerifyUserInvitationResponse> VerifyUserInvitationAsync(VerifyUserInvitationRequest request);
    }
}