using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using JsonConvert = Newtonsoft.Json.JsonConvert;
using System.Net.Mail;
using System.Text;
using CodeMash.Interfaces.Notifications;
using CodeMash.ServiceModel;
using Newtonsoft.Json.Linq;
using ServiceStack;

namespace CodeMash.Notifications
{
    public class EmailService : CodeMashBase, IEmailService
    {
        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="fromEmail">From email. - One email</param>
        /// <param name="toEmail">To email. - One recipient. Also you can provide emails comma separated</param>
        /// <param name="subject">The subject of email</param>
        /// <param name="body">The body of email</param>
        /// <returns>.</returns>
        public void SendMail(string fromEmail, string toEmail, string subject, string body)
        {
            if (toEmail.Contains(","))
            {
                var emails = toEmail.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                SendMail(fromEmail, emails, subject, body);
            }
            SendMail(fromEmail, new[] { toEmail }, subject, body);
        }

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="fromEmail">From email. - One email</param>
        /// <param name="toEmails">To emails. - You can provide an array of emails. The message will be sent to those recipients</param>
        /// <param name="subject">The subject of email</param>
        /// <param name="body">The body of email</param>
        /// <returns>.</returns>
        public void SendMail(string fromEmail, string[] toEmails, string subject, string body)
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
                    SendMail(request);
                });
            }
        }

        /// <summary>
        /// Sends the mail.
        /// </summary>
        /// <param name="fromEmail">From email. - One email</param>
        /// <param name="toEmail">To email. - One recipient. Also you can provide emails comma separated</param>
        /// <param name="subject">The subject of email</param>
        /// <param name="body">The body of email</param>
        /// <param name="attachments">The attachments.</param>
        /// <returns>.</returns>
        public void SendMail(string fromEmail, string toEmail, string subject, string body,  string[] attachments)
        {
            if (toEmail.Contains(","))
            {
                var emails = toEmail.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                SendMail(fromEmail, emails, subject, body, attachments);
            }
            SendMail(fromEmail, new[] { toEmail }, subject, body, attachments);
        }

        public void SendMail(string fromEmail, string[] toEmails, string subject, string body, string[] attachments)
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
                    SendMail(request, mailAttachments);
                });
            }
        }
        public void SendMail(string fromEmail, string toEmail, string subject, string templateName, JObject model)
        {
            if (toEmail.Contains(","))
            {
                var emails = toEmail.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                SendMail(fromEmail, emails, subject, templateName, model, (string[])null);

            }
            SendMail(fromEmail, new[] { toEmail }, subject, templateName, model, (string[])null);
        }


        public void SendMail(string fromEmail, string toEmail, string subject, string templateName, JObject model, string[] attachments)
        {
            if (toEmail.Contains(","))
            {
                var emails = toEmail.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                SendMail(fromEmail, emails, subject, templateName, model, attachments);
            }
            SendMail(fromEmail, new[] { toEmail }, subject, templateName, model, attachments);
        }


        public void SendMail(string fromEmail, string[] toEmails, string subject, string templateName, JObject model, string[] attachments)
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
                    SendMail(request, mailAttachments);
                });
            }
        }

        private void SendMail(SendMail message, List<Attachment> attachments = null)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message), "You didn't provide message which should be sent.");
            }

            if (string.IsNullOrEmpty(message.To))
            {
                throw new ArgumentNullException(nameof(message.To), "Please provide email of recipient. To who you want send the message ?");
            }

            if (string.IsNullOrEmpty(message.Subject))
            {
                throw new ArgumentNullException(nameof(message.Subject), "Please provide subject of the email.");
            }

            if (string.IsNullOrEmpty(message.From))
            {
                throw new ArgumentNullException(nameof(message.From), "Please provide email of sender. It's not polite send emails as anonymous ;)");
            }

            if (string.IsNullOrEmpty(message.Body) && string.IsNullOrEmpty(message.TemplateName))
            {
                throw new ArgumentNullException(nameof(message.Body), "You didn't provide mail content - body. Consider send something useful and use either body or template property");
            }

            // Check if smtp configuration exist

            var section = ConfigurationManager.GetSection("system.net");

            try
            {

                var msg = new MailMessage(message.From, message.To, message.Subject, message.Body)
                {
                    BodyEncoding = Encoding.UTF8,
                    DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure,
                    IsBodyHtml = true
                };

                var smtp = new SmtpClient();


                if (attachments != null)
                {
                    foreach (var attachment in attachments)
                    {
                        msg.Attachments.Add(attachment);
                    }
                }

                smtp.Send(msg);
            }
            catch (Exception e)
            {
                if (attachments != null)
                {
                    var mailAttachments = (from attachment in attachments
                                           where attachment.ContentStream != null
                                           select new MailAttachmentDataContract(attachment.Name, StreamExtensions.ReadFully(attachment.ContentStream))).ToList();

                    message.Attachments = mailAttachments;
                }

                Client.Post<SendMailResponse>(message);
            }
            
        }

    }
}