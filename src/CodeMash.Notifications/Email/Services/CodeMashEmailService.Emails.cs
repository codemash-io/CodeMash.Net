using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts.Api;

namespace CodeMash.Notifications.Email.Services
{
    public partial class CodeMashEmailService
    {
        public SendEmailNotificationResponse SendEmail(SendEmailRequest request)
        {
            return Client.Post<SendEmailNotificationResponse>(request);
        }

        public async Task<SendEmailNotificationResponse> SendEmailAsync(SendEmailRequest request)
        {
            return await Client.PostAsync<SendEmailNotificationResponse>(request);
        }
    }
}