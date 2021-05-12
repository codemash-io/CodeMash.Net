using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts.Api;

namespace CodeMash.Membership.Services
{
    public partial class CodeMashMembershipService
    {
        public GetUserResponse GetUser(GetUserRequest request)
        {
            return Client.Get<GetUserResponse>(request);
        }

        public async Task<GetUserResponse> GetUserAsync(GetUserRequest request)
        {
            return await Client.GetAsync<GetUserResponse>(request);
        }
        
        public GetProfileResponse GetProfile(GetProfileRequest request)
        {
            return Client.Get<GetProfileResponse>(request);
        }

        public async Task<GetProfileResponse> GetProfileAsync(GetProfileRequest request)
        {
            return await Client.GetAsync<GetProfileResponse>(request);
        }
        
        public GetUsersResponse GetUsersList(GetUsersRequest request)
        {
            return Client.Get<GetUsersResponse>(request);
        }

        public async Task<GetUsersResponse> GetUsersListAsync(GetUsersRequest request)
        {
            return await Client.GetAsync<GetUsersResponse>(request);
        }

        public UpdateUserResponse UpdateUser(UpdateUserRequest request)
        {
            return Client.Put<UpdateUserResponse>(request);
        }

        public async Task<UpdateUserResponse> UpdateUserAsync(UpdateUserRequest request)
        {
            return await Client.PutAsync<UpdateUserResponse>(request);
        }
        
        public void UpdateProfile(UpdateProfileRequest request)
        {
            Client.Put<object>(request);
        }

        public async Task UpdateProfileAsync(UpdateProfileRequest request)
        {
            await Client.PutAsync<object>(request);
        }

        public void UpdatePassword(UpdatePasswordRequest request)
        {
            Client.Put<object>(request);
        }

        public async Task UpdatePasswordAsync(UpdatePasswordRequest request)
        {
            await Client.PutAsync<object>(request);
        }
        
        public DeleteUserResponse DeleteUser(DeleteUserRequest request)
        {
            return Client.Delete<DeleteUserResponse>(request);
        }

        public async Task<DeleteUserResponse> DeleteUserAsync(DeleteUserRequest request)
        {
            return await Client.DeleteAsync<DeleteUserResponse>(request);
        }

        public BlockUserResponse BlockUser(BlockUserRequest request)
        {
            return Client.Put<BlockUserResponse>(request);
        }

        public async Task<BlockUserResponse> BlockUserAsync(BlockUserRequest request)
        {
            return await Client.PutAsync<BlockUserResponse>(request);
        }

        public UnblockUserResponse UnblockUser(UnblockUserRequest request)
        {
            return Client.Put<UnblockUserResponse>(request);
        }

        public async Task<UnblockUserResponse> UnblockUserAsync(UnblockUserRequest request)
        {
            return await Client.PutAsync<UnblockUserResponse>(request);
        }
    }
}