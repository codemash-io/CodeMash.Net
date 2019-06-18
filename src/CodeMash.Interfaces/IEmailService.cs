using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Interfaces
{
    public interface IEmailService
    {
        SendPushNotificationResponse SendMail(SendEmail email);
    }
}