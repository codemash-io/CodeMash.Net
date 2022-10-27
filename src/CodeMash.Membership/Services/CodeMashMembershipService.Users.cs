using System.Threading.Tasks;
using CodeMash.ServiceContracts.Api;

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

        public void UpdateUser(UpdateUserRequest request)
        {
            Client.Put(request);
        }

        public async Task UpdateUserAsync(UpdateUserRequest request)
        {
            await Client.PutAsync(request);
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
        
        public void DeleteUser(DeleteUserRequest request)
        {
            Client.Delete(request);
        }

        public async Task DeleteUserAsync(DeleteUserRequest request)
        {
            await Client.DeleteAsync(request);
        }

        public void BlockUser(BlockUserRequest request)
        {
            Client.Put(request);
        }

        public async Task BlockUserAsync(BlockUserRequest request)
        {
            await Client.PutAsync(request);
        }

        public void UnblockUser(UnblockUserRequest request)
        {
            Client.Put(request);
        }

        public async Task UnblockUserAsync(UnblockUserRequest request)
        {
            await Client.PutAsync(request);
        }
    }
}