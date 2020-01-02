using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Interfaces.Membership
{
    public partial interface IMembershipService
    {
        RegisterUserResponse RegisterUser(RegisterUserRequest registerUserData);
        
        Task<RegisterUserResponse> RegisterUserAsync(RegisterUserRequest registerUserData);
        
        InviteUserResponse InviteUser(InviteUserRequest inviteUserData);
        
        Task<InviteUserResponse> InviteUserAsync(InviteUserRequest inviteUserData);
    }
}