using System.IO;

namespace CodeMash.Interfaces.Client
{
    public interface ICodeMashRest
    {
        TResponse Get<TResponse>(object requestDto, ICodeMashRequestOptions requestOptions = null);
        
        TResponse Delete<TResponse>(object requestDto, ICodeMashRequestOptions requestOptions = null);
        
        TResponse Post<TResponse>(object requestDto, ICodeMashRequestOptions requestOptions = null);
        
        TResponse Put<TResponse>(object requestDto, ICodeMashRequestOptions requestOptions = null);
        
        TResponse Patch<TResponse>(object requestDto, ICodeMashRequestOptions requestOptions = null);
        
        TResponse PostFile<TResponse>(Stream fileToUpload, object requestDto, ICodeMashRequestOptions requestOptions = null);
    }
}