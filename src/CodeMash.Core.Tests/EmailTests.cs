using System;
using CodeMash.Common;
using CodeMash.Interfaces;
using CodeMash.Notifications.Email;
using Isidos.CodeMash.ServiceContracts;
using NSubstitute;
using NUnit.Framework;

namespace CodeMash.Core.Tests
{
    [TestFixture]
    public class EmailTests
    {
        [Test]
        [Category("Notifications.Email")]
        public void Can_send_email()
        {
            var mock = Substitute.For<ICodeMashSettings>();
            
            var emailService = new EmailService
            {
                CodeMashSettings = mock
            };

            mock.Client.Post<SendEmailResponse>(Arg.Any<string>(), Arg.Any<SendEmail>())
                .Returns(info => new SendEmailResponse {Result = true}); 
            
            var response = emailService.SendMail(new SendEmail
            {
                Recipients = new[] {"support@isidos.lt"},
                TemplateName = "Customer.WelcomeMessage"
            });

            Assert.IsNotNull(response);
            Assert.AreEqual(true, response.Result);

        }
        
        [Test]
        [Category("Notifications.Email")]
        public void Cannot_set_emails_when_email_recipients_are_not_set()
        {
            var mock = Substitute.For<ICodeMashSettings>();
            
            var emailService = new EmailService
            {
                CodeMashSettings = mock
            };

            mock.Client.Post<SendEmailResponse>(Arg.Any<SendEmail>())
                .Returns(new SendEmailResponse {Result = true}); 
            
            Assert.Throws<ArgumentNullException>(() => emailService.SendMail(new SendEmail
            {
                TemplateName = "Customer.WelcomeMessage"
            }));

        }
        
        
        [Test]
        [Category("Notifications.Email")]
        [Category("Integration")]
        public void Can_send_email_integration_test()
        {
            var emailService = new EmailService
            {
                CodeMashSettings = new CodeMashSettingsCore(null, "appsettings.Production.json")
            };

            var response = emailService.SendMail(new SendEmail
            {
                Recipients = new[] {"support@isidos.lt"},
                TemplateName = "Customer.WelcomeMessage"
            });

            Assert.IsNotNull(response);
            Assert.AreEqual(true, response.Result);
        }
    }
}