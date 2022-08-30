using System.Threading.Tasks;
using CodeMash.Client;
using Isidos.CodeMash.ServiceContracts.Api;

namespace CodeMash.ServerEvents.Services
{
    public partial class CodeMashServerEventsService
    {
        public void SendMessage(SendSseMessageRequest request)
        {
            Client.Post<object>(request, new CodeMashRequestOptions
            {
                BaseUrl = CodeMashSettings.EventsApiUrl
            });
        }

        public async Task SendMessageAsync(SendSseMessageRequest request)
        {
            await Client.PostAsync<object>(request, new CodeMashRequestOptions
            {
                BaseUrl = CodeMashSettings.EventsApiUrl
            });
        }
        
        public void ReadMessages(ReadSseMessagesRequest request)
        {
            Client.Post<object>(request, new CodeMashRequestOptions
            {
                BaseUrl = CodeMashSettings.EventsApiUrl
            });
        }

        public async Task ReadMessagesAsync(ReadSseMessagesRequest request)
        {
            await Client.PostAsync<object>(request, new CodeMashRequestOptions
            {
                BaseUrl = CodeMashSettings.EventsApiUrl
            });
        }
        
        public GetSseMessagesResponse GetMessages(GetSseMessagesRequest request)
        {
            return Client.Get<GetSseMessagesResponse>(request, new CodeMashRequestOptions
            {
                BaseUrl = CodeMashSettings.EventsApiUrl
            });
        }

        public async Task<GetSseMessagesResponse> GetMessagesAsync(GetSseMessagesRequest request)
        {
            return await Client.GetAsync<GetSseMessagesResponse>(request, new CodeMashRequestOptions
            {
                BaseUrl = CodeMashSettings.EventsApiUrl
            });
        }
    }
}