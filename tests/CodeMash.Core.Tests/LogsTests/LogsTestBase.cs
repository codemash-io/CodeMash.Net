using CodeMash.Client;
using CodeMash.Interfaces.Logs;
using CodeMash.Logs.Services;

namespace CodeMash.Core.Tests
{
    public abstract class LogsTestBase : TestBase
    {
        protected ILogsService Service { get; set; }

        public override void SetUp()
        {
            base.SetUp();
            
            var client = new CodeMashClient(ApiKey, ProjectId);
            Service = new CodeMashLogsService(client);
        }

        public override void TearDown()
        {
        }
    }
}