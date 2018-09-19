using System.Net;
using ServiceStack;

namespace CodeMash.Utils
{
    public static class CodeMashService
    {
        public static IServiceClient Client
        {
            get
            {
                var jsonClient = new JsonServiceClient(Configuration.Address)
                {
                     Credentials = new NetworkCredential(Configuration.ApiKey, "")
                };

                if (!string.IsNullOrEmpty(Configuration.ProjectId))
                {
                    jsonClient.Headers.Add("X-CM-ProjectId", Configuration.ProjectId);
                }
                
                if (!string.IsNullOrEmpty(Configuration.ApiKey))
                {
                    jsonClient.Headers.Add("Authorization", $"Bearer: {Configuration.ApiKey}");
                }
                
                return jsonClient.WithCache();
            }
        }

        
    }
}