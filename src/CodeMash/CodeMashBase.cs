using System.Net;
using ServiceStack;

namespace CodeMash.Core
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
                return jsonClient.WithCache();
            }
        }

        
    }
}