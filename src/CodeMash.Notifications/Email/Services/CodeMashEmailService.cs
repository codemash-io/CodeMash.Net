using CodeMash.Interfaces.Client;
using CodeMash.Interfaces.Notifications.Email;

namespace CodeMash.Notifications.Email.Services
{
    public partial class CodeMashEmailService : IEmailService
    {
        public ICodeMashClient Client { get; }
        
        public CodeMashEmailService(ICodeMashClient client)
        {
            Client = client;
        }
    }
}