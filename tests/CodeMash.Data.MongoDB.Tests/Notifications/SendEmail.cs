using CodeMash.Interfaces.Notifications;
using NUnit.Framework;

namespace CodeMash.Data.MongoDB.Tests
{
    [TestFixture]
    public class SendEmail : TestBase
    {
        public IEmailService EmailService { get; set; }
        protected override void Initialize()
        {
            base.Initialize();
            EmailService = Resolve<IEmailService>();
        }


        [Test]
        [Category("Notifications")]
        public void Can_send_email_using_local_smtp_settings()
        {
            EmailService.SendMail("recipient@email.com", "domantas@isidos.lt", "CodeMash - it just works", $"Hi Brad, this is not a spam, this is CODEMASH!");
            EmailService.ShouldNotBeNull();
        }
    }
}
