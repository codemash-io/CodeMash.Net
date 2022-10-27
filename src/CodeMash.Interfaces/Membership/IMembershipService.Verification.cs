using System.Threading.Tasks;
using CodeMash.ServiceContracts.Api;

namespace CodeMash.Interfaces.Membership
{
    public partial interface IMembershipService
    {
        void VerifyUser(VerifyUserRequest request);
        
        Task VerifyUserAsync(VerifyUserRequest request);
        
        ValidateInvitationTokenResponse CheckUserInvitationToken(ValidateInvitationTokenRequest request);
        
        Task<ValidateInvitationTokenResponse> CheckUserInvitationTokenAsync(ValidateInvitationTokenRequest request);
        
        void VerifyUserInvitation(VerifyUserInvitationRequest request);
        
        Task VerifyUserInvitationAsync(VerifyUserInvitationRequest request);
    }
}