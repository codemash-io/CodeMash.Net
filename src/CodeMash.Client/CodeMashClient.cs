using System;
using System.IO;
using System.Threading.Tasks;
using CodeMash.Exceptions;
using CodeMash.Interfaces.Client;
using CodeMash.Models.Requests;
using ServiceStack;

namespace CodeMash.Client
{
    public class CodeMashClient : ICodeMashClient
    {
        public ICodeMashSettings Settings { get; }
        
        public ICodeMashRequest Request { get; private set; }

        public ICodeMashResponse Response { get; private set; }
        
        private IServiceClient Gateway
        {
            get
            {
                AssertSettingsAreSet();
                var client = new JsonServiceClient(Settings.ApiBaseUrl)
                {
                    ResponseFilter = (res) =>
                        {
                            Response = new CodeMashResponse
                            {
                                ResponseUri = res.ResponseUri.ToString(),
                                Method = res.Method,
                                Headers = res.Headers.ToDictionary()
                            };
                        },
                    RequestFilter = (req) =>
                    {
                        Request = new CodeMashRequest
                        {
                            RequestUri = req.RequestUri.ToString(),
                            Host = req.Host,
                            Method = req.Method,
                            Headers = req.Headers.ToDictionary()
                        };
                    }
                };
                
                if (Settings.ProjectId != Guid.Empty)
                {
                    client.Headers.Add("X-CM-ProjectId", Settings.ProjectId.ToString());
                }

                if (!string.IsNullOrEmpty(Settings.SecretKey))
                {
                    client.BearerToken = Settings.SecretKey;
                }
                  
                return client.WithCache();
            }
        }
        
        public CodeMashClient(string settingsFileName = "appsettings.json")
        {
            Settings = new CodeMashSettings(null, settingsFileName);
        }
        
        public CodeMashClient(string apiBaseUrl, string apiKey, Guid projectId, string cultureCode = null)
        {
            Settings = new CodeMashSettings(apiBaseUrl, apiKey, projectId)
            {
                CultureCode = cultureCode
            };
        }
        
        public CodeMashClient(ICodeMashSettings settings)
        {
            Settings = settings ?? throw new ArgumentNullException(nameof(settings), "CodeMash settings undefined.");
        }
        
        private void AssertSettingsAreSet()
        {
            if (Settings == null)
            {
                throw new ArgumentNullException(nameof(CodeMashSettings), "CodeMash settings are not set");
            }  
        }
        
        private void AssertRequestIsFormed(object requestDto)
        {
            if (requestDto == null)
            {
                throw new ArgumentNullException(nameof(requestDto), "Request object is not set");
            }

            if (requestDto is RequestBase requestBaseDto)
            {
                if (string.IsNullOrEmpty(requestBaseDto.CultureCode))
                    requestBaseDto.CultureCode = Settings?.CultureCode;

                if (string.IsNullOrEmpty(requestBaseDto.Version))
                    requestBaseDto.Version = Settings?.Version;
            }
        }
        
        public async Task<TResponse> GetAsync<TResponse>(object requestDto, ICodeMashRequestOptions requestOptions = null)
        {
            return await CallAsync(async () => await FormGateway(requestDto, requestOptions).GetAsync<TResponse>(requestDto));
        }
        
        public async Task<TResponse> DeleteAsync<TResponse>(object requestDto, ICodeMashRequestOptions requestOptions = null)
        {
            return await CallAsync(async () => await FormGateway(requestDto, requestOptions).DeleteAsync<TResponse>(requestDto));
        }

        public async Task<TResponse> PostAsync<TResponse>(object requestDto, ICodeMashRequestOptions requestOptions = null)
        {
            return await CallAsync(async () => await FormGateway(requestDto, requestOptions).PostAsync<TResponse>(requestDto));
        }

        public async Task<TResponse> PutAsync<TResponse>(object requestDto, ICodeMashRequestOptions requestOptions = null)
        {
            return await CallAsync(async () => await FormGateway(requestDto, requestOptions).PutAsync<TResponse>(requestDto));
        }
        
        public async Task<TResponse> PatchAsync<TResponse>(object requestDto, ICodeMashRequestOptions requestOptions = null)
        {
            var task = Task.Run(() => Patch<TResponse>(requestDto, requestOptions));
            
            return await task;
        }

        public TResponse Get<TResponse>(object requestDto, ICodeMashRequestOptions requestOptions = null)
        {
            return Call(() => FormGateway(requestDto, requestOptions).Get<TResponse>(requestDto));
        }

        public TResponse Delete<TResponse>(object requestDto, ICodeMashRequestOptions requestOptions = null)
        {
            return Call(() => FormGateway(requestDto, requestOptions).Delete<TResponse>(requestDto));
        }

        public TResponse Post<TResponse>(object requestDto, ICodeMashRequestOptions requestOptions = null)
        {
            return Call(() => FormGateway(requestDto, requestOptions).Post<TResponse>(requestDto));
        }

        public TResponse Put<TResponse>(object requestDto, ICodeMashRequestOptions requestOptions = null)
        {
            return Call(() => FormGateway(requestDto, requestOptions).Put<TResponse>(requestDto));
        }

        public TResponse Patch<TResponse>(object requestDto, ICodeMashRequestOptions requestOptions = null)
        {
            return Call(() => FormGateway(requestDto, requestOptions).Patch<TResponse>(requestDto));
        }

        public TResponse PostFile<TResponse>(Stream fileToUpload, object requestDto, ICodeMashRequestOptions requestOptions = null)
        {
            return Call(() => FormGateway(requestDto, requestOptions).PostFileWithRequest<TResponse>(fileToUpload, requestOptions?.FileName, requestDto));
        }

        private IServiceClient FormGateway(object requestDto, ICodeMashRequestOptions requestOptions)
        {
            AssertRequestIsFormed(requestDto);

            var gateWay = Gateway;

            if (requestOptions != null)
            {
                if (!string.IsNullOrEmpty(requestOptions.BearerToken))
                {
                    gateWay.BearerToken = requestOptions.BearerToken;
                }

                if (requestOptions.UnauthenticatedRequest)
                {
                    gateWay.BearerToken = null;
                }
            }

            return gateWay;
        }

        private async Task<TResponse> CallAsync<TResponse>(Func<Task<TResponse>> callFunc)
        {
            try
            {
                return await callFunc();
            }
            catch (WebServiceException e)
            {
                var businessException = ProcessWebServiceException(e);
                throw businessException;
            }
        }
        
        private TResponse Call<TResponse>(Func<TResponse> callFunc)
        {
            try
            {
                return callFunc();
            }
            catch (WebServiceException e)
            {
                var businessException = ProcessWebServiceException(e);
                throw businessException;
            }
        }

        private BusinessException ProcessWebServiceException(WebServiceException e)
        {
            var responseStatus = e.ResponseStatus;
            if (responseStatus != null)
            {
                var businessException = new BusinessException(responseStatus.Message)
                {
                    ErrorCode = responseStatus.ErrorCode,
                    StatusCode = e.StatusCode,
                    Errors = responseStatus?.Errors?.ConvertAll(x => new ValidationError
                    {
                        ErrorCode = x.ErrorCode,
                        Message = x.Message,
                        FieldName = x.FieldName
                    })
                };

                return businessException;
            }
            
            return new BusinessException(e.Message)
            {
                ErrorCode = e.ErrorCode,
                StatusCode = e.StatusCode
            };
        }
        
    }
}