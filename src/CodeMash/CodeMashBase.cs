using System.Net;
using ServiceStack;

namespace CodeMash
{
    public class CodeMashBase
    {
        public static IServiceClient Client
        {
            get
            {
                var jsonClient = new JsonServiceClient(Configuration.Address)
                {
                     Credentials = new NetworkCredential(Configuration.ApiKey, "")
                };

                if (!string.IsNullOrEmpty(Configuration.ApplicationId))
                {
                    jsonClient.Headers.Add("X-CM-ApplicationId", Configuration.ApplicationId);
                }

                //jsonClient.Headers.Add("Authorization", $"Bearer: {Configuration.ApiKey}");

                return jsonClient.WithCache();
            }
        }

        
    }
}