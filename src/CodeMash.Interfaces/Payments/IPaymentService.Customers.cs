using System.Threading.Tasks;
using CodeMash.ServiceContracts.Api;

namespace CodeMash.Interfaces.Payments
{
    public partial interface IPaymentService
    {
        CreateCustomerResponse CreateCustomer(CreateCustomerRequest request);
        
        Task<CreateCustomerResponse> CreateCustomerAsync(CreateCustomerRequest request);
        
        GetCustomerResponse GetCustomer(GetCustomerRequest request);
        
        Task<GetCustomerResponse> GetCustomerAsync(GetCustomerRequest request);
        
        GetCustomersResponse GetCustomers(GetCustomersRequest request);
        
        Task<GetCustomersResponse> GetCustomersAsync(GetCustomersRequest request);
        
        void UpdateCustomer(UpdateCustomerRequest request);
        
        Task UpdateCustomerAsync(UpdateCustomerRequest request);
        
        void DeleteCustomer(DeleteCustomerRequest request);
        
        Task DeleteCustomerAsync(DeleteCustomerRequest request);
    }
}