using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Payments.Services
{
    public partial class CodeMashPaymentsService
    {
        public CreateCustomerResponse CreateCustomer(CreateCustomerRequest request)
        {
            return Client.Post<CreateCustomerResponse>(request);
        }

        public async Task<CreateCustomerResponse> CreateCustomerAsync(CreateCustomerRequest request)
        {
            return await Client.PostAsync<CreateCustomerResponse>(request);
        }

        public GetCustomerResponse GetCustomer(GetCustomerRequest request)
        {
            return Client.Get<GetCustomerResponse>(request);
        }

        public async Task<GetCustomerResponse> GetCustomerAsync(GetCustomerRequest request)
        {
            return await Client.GetAsync<GetCustomerResponse>(request);
        }

        public GetCustomersResponse GetCustomers(GetCustomersRequest request)
        {
            return Client.Get<GetCustomersResponse>(request);
        }

        public async Task<GetCustomersResponse> GetCustomersAsync(GetCustomersRequest request)
        {
            return await Client.GetAsync<GetCustomersResponse>(request);
        }

        public void UpdateCustomer(UpdateCustomerRequest request)
        {
            Client.Patch<object>(request);
        }

        public async Task UpdateCustomerAsync(UpdateCustomerRequest request)
        {
            await Client.PatchAsync<object>(request);
        }

        public void DeleteCustomer(DeleteCustomerRequest request)
        {
            Client.Delete<object>(request);
        }

        public async Task DeleteCustomerAsync(DeleteCustomerRequest request)
        {
            await Client.DeleteAsync<object>(request);
        }
    }
}