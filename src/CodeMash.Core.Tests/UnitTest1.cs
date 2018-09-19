using System;
using CodeMash.Interfaces;
using CodeMash.Notifications.Email;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
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

            mock.Client.Post<SendMailResponse>(Arg.Any<SendMail>())
                .Returns(new SendMailResponse {Result = true}); 
            
            var result = emailService.SendMail(new [] { "support@isidos.lt" }, "Customer.WelcomeMessage", null, null);
            
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

            mock.Client.Post<SendMailResponse>(Arg.Any<SendMail>())
                .Returns(new SendMailResponse {Result = true}); 
            
            Assert.ThrowsException<ArgumentNullException>(() => emailService.SendMail(null, "Customer.WelcomeMessage", null, null));

        }
    }
}