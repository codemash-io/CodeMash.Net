using System;
using CodeMash.Configuration.Core;
using CodeMash.Interfaces;
using CodeMash.Notifications.Email;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using ServiceStack;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class EmailTests
    {
        [TestMethod]
        public void Can_send_email()
        {
            var mock = Substitute.For<ICodeMashSettings>();
            
            var emailService = new EmailService
            {
                CodeMashSettings = mock
            };

            mock.Client.Post<SendEmailResponse>(Arg.Any<string>(), Arg.Any<SendEmail>())
                .Returns(info => new SendEmailResponse {Result = true}); 
            
            var result = emailService.SendMail(new [] { "support@isidos.lt" }, "Customer.WelcomeMessage");
            
            Assert.AreEqual(true, result);

        }
        
        [TestMethod]
        public void Cannot_set_emails_when_email_recipients_are_not_set()
        {
            var mock = Substitute.For<ICodeMashSettings>();
            
            var emailService = new EmailService
            {
                CodeMashSettings = mock
            };

            mock.Client.Post<SendEmailResponse>(Arg.Any<SendEmail>())
                .Returns(new SendEmailResponse {Result = true}); 
            
            Assert.ThrowsException<ArgumentNullException>(() => emailService.SendMail(null, "Customer.WelcomeMessage"));

        }
        
        
        [TestMethod]
        public void Can_send_email_integration_test()
        {
            var emailService = new EmailService
            {
                CodeMashSettings = new CodeMashSettingsCore(null, "appsettings.Production.json")
            };

            var result = emailService.SendMail(new[] {"support@isidos.lt"}, "Customer.WelcomeMessage");

            Assert.AreEqual(true, result);
        }
    }
}