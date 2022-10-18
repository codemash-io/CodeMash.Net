using CodeMash.Client;
using CodeMash.Code.Services;
using CodeMash.Interfaces.Code;

namespace CodeMash.Core.Tests
{
    public abstract class CodeTestBase : TestBase
    {
        protected ICodeService Service { get; set; }

        public override void SetUp()
        {
            base.SetUp();
            
            var client = new CodeMashClient(ApiKey, ProjectId);
            Service = new CodeMashCodeService(client);
        }

        public override void TearDown()
        {
        }
    }
}