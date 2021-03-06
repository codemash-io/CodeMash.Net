using System.Threading.Tasks;
using CodeMash.Interfaces.Client;
using Isidos.CodeMash.ServiceContracts.Api;

namespace CodeMash.Interfaces.Project
{
    public interface IProjectService : IClientService
    {
        GetProjectResponse GetProject(GetProjectRequest request);
        
        Task<GetProjectResponse> GetProjectAsync(GetProjectRequest request);
    }
}