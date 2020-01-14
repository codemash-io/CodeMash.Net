using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeMash.Client;
using CodeMash.Code.Services;
using CodeMash.Interfaces.Code;
using CodeMash.Interfaces.Database.Repository;
using CodeMash.Interfaces.Logs;
using CodeMash.Interfaces.Membership;
using CodeMash.Interfaces.Notifications.Email;
using CodeMash.Interfaces.Notifications.Push;
using CodeMash.Interfaces.Project;
using CodeMash.Logs.Services;
using CodeMash.Membership.Services;
using CodeMash.Notifications.Email.Services;
using CodeMash.Notifications.Push.Services;
using Isidos.CodeMash.ServiceContracts;

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