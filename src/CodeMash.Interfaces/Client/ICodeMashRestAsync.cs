using System.Threading.Tasks;

namespace CodeMash.Interfaces.Client
{
    public interface ICodeMashRestAsync
    {
        Task<TResponse> GetAsync<TResponse>(object requestDto, ICodeMashRequestOptions requestOptions = null);
        
        Task<TResponse> DeleteAsync<TResponse>(object requestDto, ICodeMashRequestOptions requestOptions = null);
        
        Task<TResponse> PostAsync<TResponse>(object requestDto, ICodeMashRequestOptions requestOptions = null);
        
        Task<TResponse> PutAsync<TResponse>(object requestDto, ICodeMashRequestOptions requestOptions = null);

        Task<TResponse> PatchAsync<TResponse>(object requestDto, ICodeMashRequestOptions requestOptions = null);
    }
}