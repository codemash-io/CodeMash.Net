using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts.Api;

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
        
        UpdateUserResponse UpdateUser(UpdateUserRequest request);
        
        Task<UpdateUserResponse> UpdateUserAsync(UpdateUserRequest request);
        
        void UpdateProfile(UpdateProfileRequest request);
        
        Task UpdateProfileAsync(UpdateProfileRequest request);
        
        void UpdatePassword(UpdatePasswordRequest request);
        
        Task UpdatePasswordAsync(UpdatePasswordRequest request);
        
        DeleteUserResponse DeleteUser(DeleteUserRequest request);
        
        Task<DeleteUserResponse> DeleteUserAsync(DeleteUserRequest request);
        
        BlockUserResponse BlockUser(BlockUserRequest request);
        
        Task<BlockUserResponse> BlockUserAsync(BlockUserRequest request);
        
        UnblockUserResponse UnblockUser(UnblockUserRequest request);
        
        Task<UnblockUserResponse> UnblockUserAsync(UnblockUserRequest request);
    }
}