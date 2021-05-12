using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class LogsTests : LogsTestBase
    {
        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
        }

        [TestCleanup]
        public override void TearDown()
        {
            base.TearDown();
        }

        [TestMethod]
        public void Can_create_log()
        {
            var response = Service.CreateLog(new CreateLogRequest
            {
                Message = "Test log",
            });
            
            Assert.IsNotNull(response.Result);
        }
        
        [TestMethod]
        public async Task Can_create_log_async()
        {
            var response = await Service.CreateLogAsync(new CreateLogRequest
            {
                Message = "Test log"
            });
            
            Assert.IsNotNull(response.Result);
        }
    }
}