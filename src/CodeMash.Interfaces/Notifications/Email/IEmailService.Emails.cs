using System.Threading.Tasks;
using CodeMash.ServiceContracts.Api;

namespace CodeMash.Interfaces.Notifications.Email
{
    public partial interface IEmailService
    {
        SendEmailNotificationV2Response SendEmail(SendEmailRequest sendEmailOptions);
        
        Task<SendEmailNotificationV2Response> SendEmailAsync(SendEmailRequest sendEmailOptions);
        
        /*
        DeleteEmailResponse DeleteEmail(DeleteEmailRequest emailId);
        
        Task<DeleteEmailResponse> DeleteEmailAsync(DeleteEmailRequest emailId);*/
    }
}