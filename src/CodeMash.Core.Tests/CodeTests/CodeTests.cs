using System;
using System.Threading.Tasks;
using Isidos.CodeMash.ServiceContracts.Api;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class CodeTests : CodeTestBase
    {
        private Guid _functionId;
        
        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            _functionId = Guid.Parse(Config["FunctionId"]);
        }

        [TestCleanup]
        public override void TearDown()
        {
            base.TearDown();
        }

        [TestMethod]
        public void Can_execute_function()
        {
            var response = Service.ExecuteFunction(new ExecuteFunctionRequest
            {
                Id = _functionId
            });
            
            Assert.IsNotNull(response.Result);
        }
        
        [TestMethod]
        public async Task Can_execute_function_async()
        {
            var response = await Service.ExecuteFunctionAsync(new ExecuteFunctionRequest
            {
                Id = _functionId
            });
            
            Assert.IsNotNull(response.Result);
        }
    }
}