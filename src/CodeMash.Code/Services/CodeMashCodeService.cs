using CodeMash.Interfaces.Client;
using CodeMash.Interfaces.Code;

namespace CodeMash.Code.Services
{
    public partial class CodeMashCodeService : ICodeService
    {
        public ICodeMashClient Client { get; }
        
        public CodeMashCodeService(ICodeMashClient client)
        {
            Client = client;
        }
    }
}