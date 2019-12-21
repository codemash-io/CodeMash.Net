using CodeMash.Interfaces.Client;
using CodeMash.Interfaces.Files;

namespace CodeMash.Project.Services
{
    public partial class CodeMashFilesService : IFilesService
    {
        public ICodeMashClient Client { get; }
        
        public CodeMashFilesService(ICodeMashClient client)
        {
            Client = client;
        }
    }
}