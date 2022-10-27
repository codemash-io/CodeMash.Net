using System.Threading.Tasks;
using CodeMash.ServiceContracts.Api;
using CodeMash.ServiceContracts.Events.Api;

namespace CodeMash.ServerEvents.Services
{
    public partial class CodeMashServerEventsService
    {
        public void SendMessage(SendSseMessageRequest request)
        {
            Client.Post<object>(request);
        }

        public async Task SendMessageAsync(SendSseMessageRequest request)
        {
            await Client.PostAsync<object>(request);
        }
        
        public void ReadMessages(ReadSseMessagesRequest request)
        {
            Client.Post<object>(request);
        }

        public async Task ReadMessagesAsync(ReadSseMessagesRequest request)
        {
            await Client.PostAsync<object>(request);
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