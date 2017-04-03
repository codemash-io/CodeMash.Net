using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using CodeMash.Interfaces.Notifications;

using ServiceStack;

#if NETCOREAPP1_1
using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Newtonsoft.Json.Linq;
    
#else
using System.Configuration;
    using System.Net;
    using System.Net.Configuration;
    using System.Net.Mail;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;
#endif

namespace CodeMash.Notifications
{
    public class EmailService : CodeMashBase, IEmailService
    {
#if NETCOREAPP1_1
        static public IConfigurationRoot ConfigurationRoot { get; set; }
#endif

        public EmailService()
        {
            #if NETSTANDARD1_6
            /*var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json");*/

            ConfigurationRoot = new ConfigurationBuilder().Build();
            #endif

        }

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
#if NET452
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
#else
        public void SendMail(string fromEmail, string toEmail, string subject, string body, string[] attachments)
        {
            throw new NotImplementedException();
        }

        public void SendMail(string fromEmail, string[] toEmails, string subject, string body, string[] attachments)
        {
            throw new NotImplementedException();
        }

        public void SendMail(string fromEmail, string toEmail, string subject, string templateName, JObject model)
        {
            throw new NotImplementedException();
        }

        public void SendMail(string fromEmail, string toEmail, string subject, string templateName, JObject model,
            string[] attachments)
        {
            throw new NotImplementedException();
        }

        public void SendMail(string fromEmail, string[] toEmails, string subject, string templateName, JObject model,
            string[] attachments)
        {
            throw new NotImplementedException();
        }

        private void SendMail(SendMail message, string[] attachments = null)
#endif
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

#if NET452

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

#else

            // add support of attachments

            // http://stackoverflow.com/questions/37853903/can-i-send-files-via-email-using-mailkit


            var email = new MimeMessage ();
            email.From.Add (new MailboxAddress (message.From));
            email.To.Add (new MailboxAddress (message.To));
            email.Subject = message.Subject;

            email.Body = new TextPart ("html") {
                Text = message.Body
            };

            using (var client = new SmtpClient ()) {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s,c,h,e) => true;

//                <add key="Smtp.deliveryMethod" value="Network" />
//                <add key="Smtp.enableSsl" value="true" />
//                <add key="Smtp.host" value="smtp.sendgrid.net" />
//                <add key="Smtp.port" value="587" />
//                <add key="Smtp.userName" value="" />
//                <add key="Smtp.password" value="" />

                client.Connect (ConfigurationRoot["Smtp.host"], int.Parse(ConfigurationRoot["Smtp.port"]), bool.Parse(ConfigurationRoot["Smtp.enableSsl"]));

                // Note: since we don't have an OAuth2 token, disable
                // the XOAUTH2 authentication mechanism.
                client.AuthenticationMechanisms.Remove ("XOAUTH2");

                // Note: only needed if the SMTP server requires authentication
                client.Authenticate (ConfigurationRoot["Smtp.userName"], ConfigurationRoot["Smtp.password"]);

                client.Send (email);
                client.Disconnect (true);
            }


#endif
            
        }

    }
}