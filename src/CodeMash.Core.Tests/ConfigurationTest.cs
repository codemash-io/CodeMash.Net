using System;
using System.IO;
using CodeMash.Common;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;

namespace CodeMash.Core.Tests
{
    [TestFixture]
    public class ConfigurationTest
    {
        [Test]
        [Category("Common")]
        public void Can_read_configuration_from_the_config_file()
        {
            var settings = new CodeMashSettingsCore(null);
            
            Assert.AreEqual("ZzYkF0_S1oxUZgyJcxb2jivlmJenfjT_", settings.ApiKey);
            Assert.AreEqual("http://localhost:5001", settings.ApiUrl);
            Assert.AreEqual(Guid.Parse("5efa02af-1e6a-40c9-90e4-8e3a34cefa45"), settings.ProjectId);

        }
        
        [Test]
        [Category("Common")]
        public void Can_read_configuration_from_the_config_file_when_file_name_is_passed()
        {
            var settings = new CodeMashSettingsCore(null, "appsettings.Staging.json");
            
            Assert.AreEqual("ZzYkF0_S1oxUZgyJcxb2jivlmJenfjT_", settings.ApiKey);
            Assert.AreEqual("http://api.codemash.io", settings.ApiUrl);
            Assert.AreEqual(Guid.Parse("5efa02af-1e6a-40c9-90e4-8e3a34cefa45"), settings.ProjectId);

        }
        
       
        [Test]
        [Category("Common")]
        public void Can_read_configuration_from_with_passing_the_config()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Production.json");
            
            var config = builder.Build();
            
            var settings = new CodeMashSettingsCore(config);
            
            Assert.AreEqual("ZzYkF0_S1oxUZgyJcxb2jivlmJenfjT_", settings.ApiKey);
            Assert.AreEqual("https://api.codemash.io", settings.ApiUrl);
            Assert.AreEqual(Guid.Parse("5efa02af-1e6a-40c9-90e4-8e3a34cefa45"), settings.ProjectId);
            
        }
    }
}