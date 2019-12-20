using CodeMash.Interfaces.Client;
using CodeMash.Interfaces.Database.Terms;

namespace CodeMash.Repository
{
    public partial class CodeMashTermsService : ITermService
    {
        public ICodeMashClient Client { get; }
        
        public CodeMashTermsService(ICodeMashClient client)
        {
            Client = client;
        }
    }
}