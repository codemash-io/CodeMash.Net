using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using CodeMash.Net.DataContracts;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using JsonConvert = Newtonsoft.Json.JsonConvert;
using System.Globalization;
using System.Net.Mail;
using Newtonsoft.Json.Linq;

namespace CodeMash.Net
{
    public static class Mailler
    {
        private static readonly string BaseUrl = Statics.Address;
        public static string ApiKey = Statics.ApiKey;

        public static TR Send<TR>(string url, object requestDto = null, string httpMethod = WebRequestMethods.Http.Get) where TR : class
        {
            try
            {
                if (string.IsNullOrEmpty(httpMethod))
                {
                    httpMethod = WebRequestMethods.Http.Get;
                }

                ApiKey = Statics.ApiKey;

                // TODO : specify this errors with codes and specify type of exception
                if (string.IsNullOrEmpty(ApiKey))
                {
                    throw new Exception("Please specify api key first");
                }

                if (string.IsNullOrEmpty(BaseUrl))
                {
                    throw new Exception("Please specify api address first");
                }

                if (httpMethod == "GET" && requestDto != null)
                {
                    url += "?" + requestDto.ToQueryString();
                }

                var serverUri = new Uri(BaseUrl);
                var relativeUri = new Uri(url, UriKind.Relative);
                var fullUri = new Uri(serverUri, relativeUri);

                var request = (HttpWebRequest)WebRequest.Create(fullUri);

                request.ContentType = "application/json; charset=utf-8";

                request.Accept = "application/json";
                request.Method = httpMethod;

                // ApiKey Auth 
                request.Headers.Add("Authorization", "Bearer " + ApiKey);

                if ((request.Method == WebRequestMethods.Http.Post || request.Method == WebRequestMethods.Http.Put || request.Method == "DELETE") && requestDto != null)
                {
                    var requestDtoAsJson = JsonConvert.SerializeObject(requestDto);
                    using (var streamWriter = new StreamWriter(request.GetRequestStream()))
                    {
                        streamWriter.Write(requestDtoAsJson);
                        streamWriter.Flush();
                    }
                }

                var response = (HttpWebResponse)request.GetResponse();

                using (var sr = new StreamReader(response.GetResponseStream()))
                {
                    var json = sr.ReadToEnd();
                    return BsonSerializer.Deserialize<TR>(json);
                }
            }
            catch (Exception e)
            {
                var errorMessage = e.Message;
#if DEBUG
                errorMessage += " " + e.StackTrace;
#endif
                throw new CodeMashException(errorMessage, e);
            }
        }


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
                    SendMail(request);
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
                    SendMail(request, mailAttachments);
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
                    SendMail(request, mailAttachments);
                });
            }
        }

        public static void SendMail(SendMail message, List<Attachment> attachments = null, string token = null)
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


            if (attachments != null)
            {
                var mailAttachments = (from attachment in attachments
                                       where attachment.ContentStream != null
                                       select new MailAttachmentDataContract(attachment.Name, attachment.ContentStream.ReadFully())).ToList();

                message.Attachments = mailAttachments;
            }

            Send<SendMailResponse>("mail", message, "POST");
        }

    }
}