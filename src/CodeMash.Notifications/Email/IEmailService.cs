using Newtonsoft.Json.Linq;

namespace CodeMash.Notifications.Email
{
    public interface IEmailService
    {
        void SendMail(string fromEmail, string toEmail, string subject, string body);
        void SendMail(string fromEmail, string[] toEmails, string subject, string body);

        void SendMail(string fromEmail, string toEmail, string subject, string body, string[] attachments);

        void SendMail(string fromEmail, string[] toEmails, string subject, string body, string[] attachments);

        void SendMail(string fromEmail, string toEmail, string subject, string templateName, JObject model);

        void SendMail(string fromEmail, string toEmail, string subject, string templateName, JObject model, 
            string[] attachments);

        void SendMail(string fromEmail, string[] toEmails, string subject, string templateName, JObject model, 
            string[] attachments);
    }
}