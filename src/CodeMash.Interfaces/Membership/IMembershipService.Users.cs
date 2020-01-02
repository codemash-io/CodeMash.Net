using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Interfaces.Membership
{
    public partial interface IMembershipService
    {
        GetUserResponse GetUser(GetUserRequest request);
        
        Task<GetUserResponse> GetUserAsync(GetUserRequest request);
        
        GetUsersResponse GetUsersList(GetUsersRequest request);
        
        Task<GetUsersResponse> GetUsersListAsync(GetUsersRequest request);
        
        UpdateUserResponse UpdateUser(UpdateUserRequest request);
        
        Task<UpdateUserResponse> UpdateUserAsync(UpdateUserRequest request);
        
        DeleteUserResponse DeleteUser(DeleteUserRequest request);
        
        Task<DeleteUserResponse> DeleteUserAsync(DeleteUserRequest request);
        
        BlockUserResponse BlockUser(BlockUserRequest request);
        
        Task<BlockUserResponse> BlockUserAsync(BlockUserRequest request);
        
        UnblockUserResponse UnblockUser(UnblockUserRequest request);
        
        Task<UnblockUserResponse> UnblockUserAsync(UnblockUserRequest request);
    }
}