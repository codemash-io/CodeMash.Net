using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeMash.Core.Tests
{
    public abstract class TestBase
    {
        public TestContext TestContext { get; set; }
        
        protected string ApiKey { get; set; }
        
        protected Guid ProjectId { get; set; }
        

        public virtual void SetUp()
        {
            ApiKey = Environment.GetEnvironmentVariable("CM_TEST_SECRET_KEY");

            var projectIdParsed = Guid.TryParse(Environment.GetEnvironmentVariable("CM_TEST_PROJECT_KEY"), out var projectId);
            if (projectIdParsed)
            {
                ProjectId = projectId;
            }

            if (string.IsNullOrEmpty(ApiKey) || ProjectId == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(TestBase), "Api Key or Project ID are not set for tests");
            }
        }

        public abstract void TearDown();
    }
}