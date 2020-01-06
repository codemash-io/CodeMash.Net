using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CodeMash.Client;
using CodeMash.Notifications.Push.Services;
using Isidos.CodeMash.ServiceContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class EmailEmailsTests : EmailTestBase
    {
        private Guid _templateId;
        private string _emailRecipient;
        
        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();

            _emailRecipient = Config["EmailRecipient"];
            _templateId = Guid.Parse(Config["EmailTemplateId"]);
        }

        [TestCleanup]
        public override void TearDown()
        {
            base.TearDown();
        }

        [TestMethod]
        public void Can_send_email_by_email_address()
        {
            var response = Service.SendEmail(new SendEmailRequest
            {
                TemplateId = _templateId,
                Emails = new List<string> { _emailRecipient }
            });
            
            Assert.IsTrue(response.Result);
        }
        
        [TestMethod]
        public async Task Can_send_email_by_email_address_async()
        {
            var response = await Service.SendEmailAsync(new SendEmailRequest
            {
                TemplateId = _templateId,
                Emails = new List<string> { _emailRecipient }
            });
            
            Assert.IsTrue(response.Result);
        }
        
        [TestMethod]
        public void Can_send_email_by_user()
        {
            var userResponse = RegisterUser(_emailRecipient);
            var response = Service.SendEmail(new SendEmailRequest
            {
                TemplateId = _templateId,
                Users = new List<Guid> { Guid.Parse(userResponse) }
            });
            
            Assert.IsTrue(response.Result);
        }
        
        [TestMethod]
        public async Task Can_send_email_by_user_async()
        {
            var userResponse = RegisterUser(_emailRecipient);
            var response = await Service.SendEmailAsync(new SendEmailRequest
            {
                TemplateId = _templateId,
                Users = new List<Guid> { Guid.Parse(userResponse) }
            });
            
            Assert.IsTrue(response.Result);
        }
    }
}