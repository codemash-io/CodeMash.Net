# CodeMash - Get ApiKey
1. Register an account or login to <a target="_blank" href="http://cloud.codemash.io">CodeMash dashboard</a> 
2. On CodeMash dashboard go to <a target="_blank" href="http://cloud.codemash.io/connections/db">Connections</a> and find connection called "Api" 
3. Copy your Api key and address of CodeMash api to your configuration file as follows

```
<?xml version="1.0" encoding="utf-8" ?>  
<configuration>
  <configSections>
    <section name="CodeMash" type="CodeMash.Net.CodeMashConfigurationSection, CodeMash.Net" requirePermission="false" />
  </configSections>
  <CodeMash>
    <client name="CodeMashClient" apiKey="your api token" address="http://api.codemash.io/1.0/" />
  </CodeMash>  
</configuration>
```
