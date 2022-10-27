using System.Threading.Tasks;
using CodeMash.ServiceContracts.Api;

namespace CodeMash.Interfaces.Membership
{
    public partial interface IMembershipService
    {
        RegisterUserV2Response RegisterUser(RegisterUserRequest registerUserData);
        
        Task<RegisterUserV2Response> RegisterUserAsync(RegisterUserRequest registerUserData);

        RegisterGuestUserResponse RegisterGuestUser(RegisterGuestUserRequest registerUserData);

        Task<RegisterGuestUserResponse> RegisterGuestUserAsync(RegisterGuestUserRequest registerUserData);
        
        InviteUserV2Response InviteUser(InviteUserRequest inviteUserData);
        
        Task<InviteUserV2Response> InviteUserAsync(InviteUserRequest inviteUserData);
    }
}