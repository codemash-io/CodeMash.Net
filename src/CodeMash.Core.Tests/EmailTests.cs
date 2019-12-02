using System;
using CodeMash.Common;
using CodeMash.Interfaces;
using CodeMash.Notifications.Email;
using Isidos.CodeMash.ServiceContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class EmailTests
    {
        
        // TODO : throw nice message when module is disabled or not established yet
        
        [TestMethod]
        public void Can_send_email()
        {
            var mock = Substitute.For<ICodeMashSettings>();
            
            var emailService = new EmailService
            {
                CodeMashSettings = mock
            };

            mock.Client.Post(Arg.Any<SendEmail>())
                .Returns(info => new SendPushNotificationResponse {Result = "asd"}); 
            
            var response = emailService.SendMail(new SendEmail
            {
                Emails = new[] {"support@isidos.lt"},
                TemplateId = "b2f36c34-fd0e-44a3-bac2-18cf5433c6d9"
            });

            response.ShouldNotNull();
            response.Result.ShouldEqual("asd");

        }
        
        [TestMethod]
        public void Cannot_set_emails_when_email_recipients_are_not_set()
        {
            var mock = Substitute.For<ICodeMashSettings>();
            
            var emailService = new EmailService
            {
                CodeMashSettings = mock
            };

            mock.Client.Post(Arg.Any<SendEmail>())
                .Returns(new SendPushNotificationResponse {Result = "asd"}); 
            
            Assert.ThrowsException<ArgumentNullException>(() => emailService.SendMail(new SendEmail
            {
                TemplateId = "b2f36c34-fd0e-44a3-bac2-18cf5433c6d9"
            }));

        }
        
        
        [TestMethod]
        public void Can_send_email_integration_test()
        {
            var emailService = new EmailService
            {
                CodeMashSettings = new CodeMashSettingsCore(null, "appsettings.Production.json")
            };

            var response = emailService.SendMail(new SendEmail
            {
                Emails = new[] {"support@isidos.lt"},
                TemplateId = "b2f36c34-fd0e-44a3-bac2-18cf5433c6d9"
            });

            response.ShouldNotNull();
            response.Result.ShouldBe<string>();
        }
    }
}