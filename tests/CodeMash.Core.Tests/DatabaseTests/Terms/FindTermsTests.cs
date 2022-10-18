using System.Threading.Tasks;
using CodeMash.Client;
using CodeMash.Interfaces.Database.Terms;
using CodeMash.Models;
using CodeMash.Repository;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class FindTermsTests : DatabaseTestBase
    {
        protected ITermService TermService { get; set; }
        
        [TestInitialize]
        public override void SetUp()
        {
            base.SetUp();
            
            var client = new CodeMashClient(ApiKey, ProjectId);
            TermService = new CodeMashTermsService(client);
        }
        
        [TestCleanup]
        public override void TearDown()
        {
            base.TearDown();
        }

        class TermMeta
        {
            [Field("text")]
            public string Text { get; set; }
        }
        
        [TestMethod]
        public void Can_find_all_with_meta_string()
        {
            var terms = TermService.Find("sdk-tax", null);
            
            Assert.AreEqual(terms.Items[0].Name, "Name_en");
            Assert.AreEqual(terms.Items[0].Description, "Desc_en");
            Assert.AreEqual(terms.Items[0].Order, 1);
        }
        
        [TestMethod]
        public async Task Can_find_all_with_meta_string_async()
        {
            var terms = await TermService.FindAsync("sdk-tax", null);
            
            Assert.AreEqual(terms.Items[0].Name, "Name_en");
            Assert.AreEqual(terms.Items[0].Description, "Desc_en");
            Assert.AreEqual(terms.Items[0].Order, 1);
        }
        
        [TestMethod]
        public void Can_find_all_with_meta_object()
        {
            var terms = TermService.Find<TermMeta>("sdk-tax", null);
            
            Assert.AreEqual(terms.Items[0].Name, "Name_en");
            Assert.AreEqual(terms.Items[0].Description, "Desc_en");
            Assert.AreEqual(terms.Items[0].Order, 1);
            Assert.AreEqual(terms.Items[0].Meta.Text, "Meta_Text");
        }
        
        [TestMethod]
        public async Task Can_find_all_with_meta_object_async()
        {
            var terms = await TermService.FindAsync<TermMeta>("sdk-tax", null);
            
            Assert.AreEqual(terms.Items[0].Name, "Name_en");
            Assert.AreEqual(terms.Items[0].Description, "Desc_en");
            Assert.AreEqual(terms.Items[0].Order, 1);
            Assert.AreEqual(terms.Items[0].Meta.Text, "Meta_Text");
        }
    }
}