using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts.Api;

namespace CodeMash.Interfaces.ServerEvents
{
    public partial interface IServerEventsService
    {
        void SendMessage(SendSseMessageRequest request);
        
        Task SendMessageAsync(SendSseMessageRequest request);
        
        void ReadMessages(ReadSseMessagesRequest request);
        
        Task ReadMessagesAsync(ReadSseMessagesRequest request);
        
        GetSseMessagesResponse GetMessages(GetSseMessagesRequest request);
        
        Task<GetSseMessagesResponse> GetMessagesAsync(GetSseMessagesRequest request);
    }
}