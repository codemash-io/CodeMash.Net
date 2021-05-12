using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts.Api;

namespace CodeMash.Notifications.Push.Services
{
    public partial class CodeMashPushService
    {
        public GetNotificationTemplateResponse GetTemplate(GetNotificationTemplateRequest request)
        {
            return Client.Get<GetNotificationTemplateResponse>(request);
        }

        public async Task<GetNotificationTemplateResponse> GetTemplateAsync(GetNotificationTemplateRequest request)
        {
            return await Client.GetAsync<GetNotificationTemplateResponse>(request);
        }

        public GetNotificationTemplatesResponse GetTemplates(GetNotificationTemplatesRequest request)
        {
            return Client.Get<GetNotificationTemplatesResponse>(request);
        }

        public async Task<GetNotificationTemplatesResponse> GetTemplatesAsync(GetNotificationTemplatesRequest request)
        {
            return await Client.GetAsync<GetNotificationTemplatesResponse>(request);
        }
    }
}