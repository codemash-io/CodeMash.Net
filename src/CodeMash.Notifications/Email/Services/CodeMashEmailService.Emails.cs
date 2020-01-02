using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts;

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

        public DeleteEmailResponse DeleteEmail(DeleteEmailRequest request)
        {
            return Client.Delete<DeleteEmailResponse>(request);
        }

        public async Task<DeleteEmailResponse> DeleteEmailAsync(DeleteEmailRequest request)
        {
            return await Client.DeleteAsync<DeleteEmailResponse>(request);
        }
    }
}