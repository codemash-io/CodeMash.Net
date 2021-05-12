using System;
using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class NotificationTemplateTests : NotificationTestBase
    {
        private Guid _templateId;
        
        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            
            _templateId = Guid.Parse(Config["PushTemplateId"]);
        }

        [TestCleanup]
        public override void TearDown()
        {
        }

        [TestMethod]
        public void Can_get_notification_template()
        {
            var response = Service.GetTemplate(new GetNotificationTemplateRequest
            {
                Id = _templateId
            });
            
            Assert.AreEqual(response.Result.Id, _templateId.ToString());
        }
        
        [TestMethod]
        public async Task Can_get_notification_template_async()
        {
            var response = await Service.GetTemplateAsync(new GetNotificationTemplateRequest
            {
                Id = _templateId
            });
            
            Assert.AreEqual(response.Result.Id, _templateId.ToString());
        }
        
        [TestMethod]
        public void Can_get_devices()
        {
            var response = Service.GetTemplates(new GetNotificationTemplatesRequest());
            
            Assert.IsTrue(response.Result.Count > 0);
            Assert.IsTrue(response.Result.Exists(x => x.Id == _templateId.ToString()));
        }
        
        [TestMethod]
        public async Task Can_get_devices_async()
        {
            var response = await Service.GetTemplatesAsync(new GetNotificationTemplatesRequest());
            
            Assert.IsTrue(response.Result.Count > 0);
            Assert.IsTrue(response.Result.Exists(x => x.Id == _templateId.ToString()));
        }
    }
}