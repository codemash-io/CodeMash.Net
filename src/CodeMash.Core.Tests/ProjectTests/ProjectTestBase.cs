using System.Collections.Generic;
using System.Linq;
using CodeMash.Interfaces.Database.Repository;
using CodeMash.Interfaces.Project;

namespace CodeMash.Core.Tests
{
    public abstract class ProjectTestBase : TestBase
    {
        protected IProjectService Service { get; set; }

        public override void TearDown()
        {
        }
    }
}