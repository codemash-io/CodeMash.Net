using Newtonsoft.Json.Linq;

namespace CodeMash.Interfaces.Notifications
{
    public interface IEmailService
    {
        void SendMail(string toEmail, string subject, string body, string fromEmail);
        void SendMail(string[] toEmails, string subject, string body, string fromEmail);

        void SendMail(string toEmail, string subject, string body, string fromEmail, string[] attachments);

        void SendMail(string[] toEmails, string subject, string body, string fromEmail, string[] attachments);

        void SendMail(string toEmail, string subject, string templateName, JObject model, string fromEmail);

        void SendMail(string toEmail, string subject, string templateName, JObject model, string fromEmail,
            string[] attachments);

        void SendMail(string[] toEmails, string subject, string templateName, JObject model, string fromEmail,
            string[] attachments);
    }
}