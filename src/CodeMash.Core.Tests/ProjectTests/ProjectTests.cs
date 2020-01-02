using System;
using System.Linq;
using System.Threading.Tasks;
using CodeMash.Client;
using CodeMash.Membership.Services;
using CodeMash.Models.Exceptions;
using CodeMash.Project.Services;
using Isidos.CodeMash.ServiceContracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public partial class ProjectTests : ProjectTestBase
    {
        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            
            var client = new CodeMashClient(ApiKey, ProjectId);
            Service = new CodeMashProjectService(client);
        }

        [TestCleanup]
        public override void TearDown()
        {
        }
        
        [TestMethod]
        public void Can_get_project_data()
        {
            var projectData = Service.GetProject(new GetProjectRequest());
            
            Assert.IsNotNull(projectData.Result);
            Assert.AreEqual(projectData.Result.Id, ProjectId);
        }
        
        [TestMethod]
        public async Task Can_get_project_data_async()
        {
            var projectData = await Service.GetProjectAsync(new GetProjectRequest());
            
            Assert.IsNotNull(projectData.Result);
            Assert.AreEqual(projectData.Result.Id, ProjectId);
        }
    }
}