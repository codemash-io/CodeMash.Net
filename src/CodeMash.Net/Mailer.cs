using System;
using System.Collections.Generic;
using System.Linq;
using CodeMash.Net.DataContracts;
using JsonConvert = Newtonsoft.Json.JsonConvert;
using System.Net.Mail;
using Newtonsoft.Json.Linq;
using ServiceStack;

namespace CodeMash.Net
{
    public class Mailer : CodeMashBase
    {
        public static IDatabaseRepository repo = MongoRepositoryFactory.Create();
        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="toEmail">To email. - One recipient. Also you can provide emails comma separated</param>
        /// <param name="subject">The subject of email</param>
        /// <param name="body">The body of email</param>
        /// <param name="fromEmail">From email. - One email</param>
        /// <returns>.</returns>
        public static void SendMail(string toEmail, string subject, string body, string fromEmail)
        {
            if (toEmail.Contains(","))
            {
                var emails = toEmail.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                SendMail(emails, subject, body, fromEmail);
            }
            SendMail(new[] { toEmail }, subject, body, fromEmail);
        }

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="toEmails">To emails. - You can provide an array of emails. The message will be sent to those recipients</param>
        /// <param name="subject">The subject of email</param>
        /// <param name="body">The body of email</param>
        /// <param name="fromEmail">From email. - One email</param>
        /// <returns>.</returns>
        public static void SendMail(string[] toEmails, string subject, string body, string fromEmail)
        {
            if (toEmails != null && toEmails.Any())
            {
                toEmails.ToList().ForEach(email =>
                {
                    var request = new SendMail
                    {
                        To = email,
                        Subject = subject,
                        Body = body,
                        From = fromEmail
                    };
                    repo.SendMail(request);
                });
            }
        }

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="toEmail">To email. - One recipient. Also you can provide emails comma separated</param>
        /// <param name="subject">The subject of email</param>
        /// <param name="body">The body of email</param>
        /// <param name="fromEmail">From email. - One email</param>
        /// <param name="attachments">The attachments.</param>
        /// <returns>.</returns>

        public static void SendMail(string toEmail, string subject, string body, string fromEmail, string[] attachments)
        {
            if (toEmail.Contains(","))
            {
                var emails = toEmail.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                SendMail(emails, subject, body, fromEmail, attachments);
            }
            SendMail(new[] { toEmail }, subject, body, fromEmail, attachments);
        }

        public static void SendMail(string[] toEmails, string subject, string body, string fromEmail, string[] attachments)
        {
            List<Attachment> mailAttachments = null;

            if (attachments != null)
            {
                mailAttachments = (from attachment in attachments where !string.IsNullOrEmpty(attachment) select new Attachment(attachment)).ToList();
            }

            if (toEmails != null && toEmails.Any())
            {
                toEmails.ToList().ForEach(email =>
                {
                    var request = new SendMail
                    {
                        To = email,
                        Subject = subject,
                        Body = body,
                        From = fromEmail,
                    };
                    repo.SendMail(request, mailAttachments);
                });
            }
        }
        public static void SendMail(string toEmail, string subject, string templateName, JObject model, string fromEmail)
        {
            if (toEmail.Contains(","))
            {
                var emails = toEmail.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                SendMail(emails, subject, templateName, model, fromEmail, (string[])null);

            }
            SendMail(new[] { toEmail }, subject, templateName, model, fromEmail, (string[])null);
        }


        public static void SendMail(string toEmail, string subject, string templateName, JObject model, string fromEmail, string[] attachments)
        {
            if (toEmail.Contains(","))
            {
                var emails = toEmail.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                SendMail(emails, subject, templateName, model, fromEmail, attachments);
            }
            SendMail(new[] { toEmail }, subject, templateName, model, fromEmail, attachments);
        }


        public static void SendMail(string[] toEmails, string subject, string templateName, JObject model, string fromEmail, string[] attachments)
        {
            // dynamic input from inbound JSON
            dynamic json = model;

            string jsonAsString = JsonConvert.SerializeObject(json);

            List<Attachment> mailAttachments = null;

            if (attachments != null)
            {
                mailAttachments = (from attachment in attachments where !string.IsNullOrEmpty(attachment) select new Attachment(attachment)).ToList();
            }

            if (toEmails != null && toEmails.Any())
            {
                toEmails.ToList().ForEach(email =>
                {
                    var request = new SendMail
                    {
                        To = email,
                        Subject = subject,
                        TemplateName = templateName,
                        ModelInJsonAsString = jsonAsString,
                        From = fromEmail,
                    };
                    repo.SendMail(request, mailAttachments);
                });
            }
        }

        

    }
}