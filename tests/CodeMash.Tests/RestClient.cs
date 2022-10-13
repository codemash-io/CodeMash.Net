using System.Net;
using System.Net.Http.Headers;

namespace CodeMash.Tests;

public record RequestContext
{
    public CookieCollection Cookies { get; set; } = new();

    public Guid ProjectId { get; set; } = Guid.Empty;

    public string ApiKey { get; set; } = "";

    public string BaseUri { get; set; } = "";
}

public class RestClient
{
    public CodeMashConfiguration AppSettings { get; set; }

    public RequestContext Context { get; set; }
    
    public RestClient(RequestContext requestContext)
    {
        AppSettings = new CodeMashConfiguration();
        Context = requestContext;

    }

    public static HttpClient Api(RequestContext? requestContext = null)
    {
        requestContext ??= new RequestContext();

        var client = new RestClient(requestContext);
        
        client.Context.BaseUri = client.AppSettings.ApiBaseUri;

        return client.HttpClient;

    }
    public static HttpClient Hub(RequestContext? requestContext = null)
    {
        requestContext ??= new RequestContext();

        var client = new RestClient(requestContext);
        
        client.Context.BaseUri = client.AppSettings.HubBaseUri;

        return client.HttpClient;

    }

    public HttpClient HttpClient  
    { 
        get
        {
            if (Context == null || string.IsNullOrWhiteSpace(Context.BaseUri))
            {
                throw new ArgumentException("Base Uri is not set");
            }
            
            
            var baseUri = new Uri(Context.BaseUri);
            var cookiesContainer = new CookieContainer();
            var handler = new HttpClientHandler();
            handler.CookieContainer = cookiesContainer;
            
            if (Context.Cookies.Count > 0)
            {
                cookiesContainer.Add(baseUri, Context.Cookies);    
            }
            
            var client = new HttpClient(handler)
            {
                BaseAddress = baseUri,
                
                DefaultRequestHeaders =
                {
                    Accept = {new MediaTypeWithQualityHeaderValue("application/json")}
                }
            };

            if (Context.ProjectId != Guid.Empty)
            {
                client.DefaultRequestHeaders.Add("X-CM-ProjectId", Context.ProjectId.ToString());
                    
            }
            
            if (!string.IsNullOrWhiteSpace(Context.ApiKey))
            {
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", Context.ApiKey);
            }

            return client;
        }
    }
}