using System;
using System.IO;
using System.Net;
using MongoDB.Bson.Serialization;
using Newtonsoft.Json;
using ServiceStack;

namespace CodeMash.Net
{
    public class CodeMashBase
    {
        protected static readonly string BaseUrl = Statics.Address;
        protected static string ApiKey = Statics.ApiKey;


        protected static IServiceClient Client
        {
            get
            {
                var jsonClient = new JsonServiceClient(BaseUrl)
                {
                    Credentials = new NetworkCredential(ApiKey, "")
                };
                return jsonClient.WithCache();
            }
        }

        /// <summary>
        /// Old fashion way to make a request when don't using ServiceStack JsonClient
        /// </summary>
        /// <typeparam name="TR"></typeparam>
        /// <param name="url"></param>
        /// <param name="requestDto"></param>
        /// <param name="httpMethod"></param>
        /// <returns></returns>
        protected static TR SendHttp<TR>(string url, object requestDto = null, string httpMethod = WebRequestMethods.Http.Get) where TR : class
        {
            try
            {
                if (string.IsNullOrEmpty(httpMethod))
                {
                    httpMethod = WebRequestMethods.Http.Get;
                }

                ApiKey = Statics.ApiKey;

                // TODO : specify this errors with codes and specify type of exception
                if (string.IsNullOrEmpty(ApiKey))
                {
                    throw new Exception("Please specify api key first");
                }

                if (string.IsNullOrEmpty(BaseUrl))
                {
                    throw new Exception("Please specify api address first");
                }

                if (httpMethod == "GET" && requestDto != null)
                {
                    url += "?" + requestDto.ToQueryString();
                }

                var serverUri = new Uri(BaseUrl);
                var relativeUri = new Uri(url, UriKind.Relative);
                var fullUri = new Uri(serverUri, relativeUri);

                var request = (HttpWebRequest)WebRequest.Create(fullUri);

                request.ContentType = "application/json; charset=utf-8";

                request.Accept = "application/json";
                request.Method = httpMethod;

                // ApiKey Auth 
                request.Headers.Add("Authorization", "Bearer " + ApiKey);

                if ((request.Method == WebRequestMethods.Http.Post || request.Method == WebRequestMethods.Http.Put || request.Method == "DELETE") && requestDto != null)
                {
                    var requestDtoAsJson = JsonConvert.SerializeObject(requestDto);
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(requestDtoAsJson);
                        streamWriter.Flush();
                    }
                }

                var response = (HttpWebResponse)request.GetResponse();

                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    var json = sr.ReadToEnd();
                    return BsonSerializer.Deserialize<TR>(json);
                }
            }
            catch (Exception e)
            {
                var errorMessage = e.Message;
#if DEBUG
                errorMessage += " " + e.StackTrace;
#endif
                throw new CodeMashException(errorMessage, e);
            }
        }
    }
}