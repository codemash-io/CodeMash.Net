using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts.Api;

namespace CodeMash.ServerEvents.Services
{
    public partial class CodeMashServerEventsService
    {
        public void SendMessage(SendSseMessageRequest request)
        {
            Client.Post<SendSseMessageRequest>(request);
        }

        public async Task SendMessageAsync(SendSseMessageRequest request)
        {
            await Client.PostAsync<RegisterUserResponse>(request);
        }
        
        public void ReadMessages(ReadSseMessagesRequest request)
        {
            Client.Post<ReadSseMessagesRequest>(request);
        }

        public async Task ReadMessagesAsync(ReadSseMessagesRequest request)
        {
            await Client.PostAsync<ReadSseMessagesRequest>(request);
        }
        
        public GetSseMessagesResponse GetMessages(GetSseMessagesRequest request)
        {
            return Client.Get<GetSseMessagesResponse>(request);
        }

        public async Task<GetSseMessagesResponse> GetMessagesAsync(GetSseMessagesRequest request)
        {
            return await Client.GetAsync<GetSseMessagesResponse>(request);
        }
    }
}