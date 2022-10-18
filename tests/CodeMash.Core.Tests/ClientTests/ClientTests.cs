using CodeMash.Client;
using CodeMash.Interfaces.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class ClientTests : TestBase
    {
        private ICodeMashClient Client { get; set; }
        
        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            
            Client = new CodeMashClient(ApiKey, ProjectId);
        }

        [TestCleanup]
        public override void TearDown()
        {
        }

        [TestMethod]
        public void Client_settings_set_on_creation()
        {
            Assert.IsNotNull(Client.Settings);
        }
    }
}