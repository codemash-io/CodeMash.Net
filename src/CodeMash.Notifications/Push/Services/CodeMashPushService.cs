using CodeMash.Interfaces.Client;
using CodeMash.Interfaces.Notifications.Push;

namespace CodeMash.Notifications.Push.Services
{
    public partial class CodeMashPushService : IPushService
    {
        public ICodeMashClient Client { get; }
        
        public CodeMashPushService(ICodeMashClient client)
        {
            Client = client;
        }
    }
}