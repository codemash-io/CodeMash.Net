using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts.Api;

namespace CodeMash.Interfaces.Notifications.Email
{
    public partial interface IEmailService
    {
        SendEmailNotificationResponse SendEmail(SendEmailRequest sendEmailOptions);
        
        Task<SendEmailNotificationResponse> SendEmailAsync(SendEmailRequest sendEmailOptions);
        
        /*
        DeleteEmailResponse DeleteEmail(DeleteEmailRequest emailId);
        
        Task<DeleteEmailResponse> DeleteEmailAsync(DeleteEmailRequest emailId);*/
    }
}