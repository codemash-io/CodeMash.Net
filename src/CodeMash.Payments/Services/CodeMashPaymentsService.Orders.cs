using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Payments.Services
{
    public partial class CodeMashPaymentsService
    {
        public GetOrderResponse GetOrder(GetOrderRequest request)
        {
            return Client.Get<GetOrderResponse>(request);
        }

        public async Task<GetOrderResponse> GetOrderAsync(GetOrderRequest request)
        {
            return await Client.GetAsync<GetOrderResponse>(request);
        }

        public GetOrdersResponse GetOrders(GetOrdersRequest request)
        {
            return Client.Get<GetOrdersResponse>(request);
        }

        public async Task<GetOrdersResponse> GetOrdersAsync(GetOrdersRequest request)
        {
            return await Client.GetAsync<GetOrdersResponse>(request);
        }

        public CreateOrderResponse CreateOrder(CreateOrderRequest request)
        {
            return Client.Post<CreateOrderResponse>(request);
        }

        public async Task<CreateOrderResponse> CreateOrderAsync(CreateOrderRequest request)
        {
            return await Client.PostAsync<CreateOrderResponse>(request);
        }
    }
}