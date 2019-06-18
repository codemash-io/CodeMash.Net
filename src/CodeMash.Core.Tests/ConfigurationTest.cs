using System;
using System.IO;
using CodeMash.Common;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeMash.Core.Tests
{
    [TestClass]
    public class ConfigurationTest
    {
        [TestMethod]
        public void Can_read_configuration_from_the_config_file()
        {
            var settings = new CodeMashSettingsCore(null);

            "ZzYkF0_S1oxUZgyJcxb2jivlmJenfjT_".ShouldEqual(settings.ApiKey);
            "http://localhost:5001".ShouldEqual(settings.ApiUrl);
            Guid.Parse("5efa02af-1e6a-40c9-90e4-8e3a34cefa45").ShouldEqual(settings.ProjectId);

        }
        
        [TestMethod]
        public void Can_read_configuration_from_the_config_file_when_file_name_is_passed()
        {
            var settings = new CodeMashSettingsCore(null, "appsettings.Production.json");
            
            "caZkSuuuYwCDG8sVOYjBx3arg1VJ9S7l".ShouldEqual(settings.ApiKey);
            "https://api.codemash.io".ShouldEqual(settings.ApiUrl);
            Guid.Parse("046e7b3c-5945-4869-b0ad-531f73f6aeeb").ShouldEqual(settings.ProjectId);

        }
        
       
        [TestMethod]
        public void Can_read_configuration_from_with_passing_the_config()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.Production.json");
            
            var config = builder.Build();
            
            var settings = new CodeMashSettingsCore(config);
            
            "caZkSuuuYwCDG8sVOYjBx3arg1VJ9S7l".ShouldEqual(settings.ApiKey);
            "https://api.codemash.io".ShouldEqual(settings.ApiUrl);
            Guid.Parse("046e7b3c-5945-4869-b0ad-531f73f6aeeb").ShouldEqual(settings.ProjectId);
            
        }
    }
}