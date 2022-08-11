using CodeMash.Interfaces.Client;
using CodeMash.Interfaces.ServerEvents;

namespace CodeMash.ServerEvents.Services
{
    public partial class CodeMashServerEventsService : IServerEventsService
    {
        public ICodeMashClient Client { get; }

        public CodeMashServerEventsService(ICodeMashClient client)
        {
            Client = client;
        }
    }
}