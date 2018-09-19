using System;
using CodeMash.Interfaces;

namespace CodeMash.Configuration
{
    public class CodeMashConfiguration
    {
        protected ICodeMashSettings CodeMashSettings { get; set; }
        
        protected CodeMashConfiguration(ICodeMashSettings codeMashSettings)
        {
            CodeMashSettings = codeMashSettings;
        }

        public string ApiKey => CodeMashSettings.ApiKey;
        public string ApiUrl => CodeMashSettings.ApiUrl;
        public Guid ProjectId => CodeMashSettings.ProjectId;

        public virtual void AssertConfigurationIsSetProperly()
        {
            if (string.IsNullOrWhiteSpace(ApiKey))
            {
                throw new Exception("Please specify apiKey in CodeMash configuration. More about configuration of CodeMash at http://codemash.io/documentation/api/net/configuration");
            }

            if (string.IsNullOrWhiteSpace(ApiUrl))
            {
                throw new Exception("Please specify endpoint address of CodeMash in CodeMash configuration. More about configuration of CodeMash at http://codemash.io/documentation/api/net/configuration");
            }
            
            if (ProjectId == null || ProjectId == Guid.Empty)
            {
                throw new Exception("Please specify project id. More about configuration of CodeMash at http://codemash.io/documentation/api/net/configuration");
            }
        }
    }
}