using Isidos.CodeMash.ServiceContracts;

namespace CodeMash.Interfaces
{
    public interface IEmailService
    {
        SendEmailResponse SendMail(SendEmail email);
    }
}