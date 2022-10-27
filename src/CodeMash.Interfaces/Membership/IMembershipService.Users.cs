using System.Threading.Tasks;
using CodeMash.ServiceContracts.Api;

namespace CodeMash.Interfaces.Membership
{
    public partial interface IMembershipService
    {
        GetUserResponse GetUser(GetUserRequest request);
        
        Task<GetUserResponse> GetUserAsync(GetUserRequest request);
        
        GetProfileResponse GetProfile(GetProfileRequest request);
        
        Task<GetProfileResponse> GetProfileAsync(GetProfileRequest request);
        
        GetUsersResponse GetUsersList(GetUsersRequest request);
        
        Task<GetUsersResponse> GetUsersListAsync(GetUsersRequest request);
        
        void UpdateUser(UpdateUserRequest request);
        
        Task UpdateUserAsync(UpdateUserRequest request);
        
        void UpdateProfile(UpdateProfileRequest request);
        
        Task UpdateProfileAsync(UpdateProfileRequest request);
        
        void UpdatePassword(UpdatePasswordRequest request);
        
        Task UpdatePasswordAsync(UpdatePasswordRequest request);
        
        void DeleteUser(DeleteUserRequest request);
        
        Task DeleteUserAsync(DeleteUserRequest request);
        
        void BlockUser(BlockUserRequest request);
         
        Task BlockUserAsync(BlockUserRequest request);
        
        void UnblockUser(UnblockUserRequest request);
        
        Task UnblockUserAsync(UnblockUserRequest request);
    }
}