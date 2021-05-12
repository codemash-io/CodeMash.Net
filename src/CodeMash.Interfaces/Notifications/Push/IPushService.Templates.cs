using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts.Api;

namespace CodeMash.Interfaces.Notifications.Push
{
    public partial interface IPushService
    {
        GetNotificationTemplateResponse GetTemplate(GetNotificationTemplateRequest request);
        
        Task<GetNotificationTemplateResponse> GetTemplateAsync(GetNotificationTemplateRequest request);
        
        GetNotificationTemplatesResponse GetTemplates(GetNotificationTemplatesRequest request);
        
        Task<GetNotificationTemplatesResponse> GetTemplatesAsync(GetNotificationTemplatesRequest request);
    }
}