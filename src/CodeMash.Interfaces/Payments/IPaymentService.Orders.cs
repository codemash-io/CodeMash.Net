using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts.Api;

namespace CodeMash.Interfaces.Payments
{
    public partial interface IPaymentService
    {
        GetOrderResponse GetOrder(GetOrderRequest request);
        
        Task<GetOrderResponse> GetOrderAsync(GetOrderRequest request);
        
        GetOrdersResponse GetOrders(GetOrdersRequest request);
        
        Task<GetOrdersResponse> GetOrdersAsync(GetOrdersRequest request);
        
        CreateOrderResponse CreateOrder(CreateOrderRequest request);
        
        Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest request);
    }
}