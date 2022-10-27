using System.Threading.Tasks;
using CodeMash.ServiceContracts.Api;

namespace CodeMash.Notifications.Email.Services
{
    public partial class CodeMashEmailService
    {
        public SendEmailNotificationV2Response SendEmail(SendEmailRequest request)
        {
            return Client.Post<SendEmailNotificationV2Response>(request);
        }

        public async Task<SendEmailNotificationV2Response> SendEmailAsync(SendEmailRequest request)
        {
            return await Client.PostAsync<SendEmailNotificationV2Response>(request);
        }
    }
}