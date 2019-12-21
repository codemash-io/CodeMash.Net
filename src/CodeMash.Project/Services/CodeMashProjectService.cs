using System.Threading.Tasks;
using CodeMash.Interfaces.Client;
using CodeMash.Interfaces.Project;
using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Project.Services
{
    public class CodeMashProjectService : IProjectService
    {
        public ICodeMashClient Client { get; }
        
        public CodeMashProjectService(ICodeMashClient client)
        {
            Client = client;
        }

        public GetProjectResponse GetProject(GetProjectRequest request)
        {
            return Client.Get<GetProjectResponse>(request);
        }

        public async Task<GetProjectResponse> GetProjectAsync(GetProjectRequest request)
        {
            return await Client.GetAsync<GetProjectResponse>(request);
        }
    }
}