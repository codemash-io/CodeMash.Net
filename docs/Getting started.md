# CodeMash instalation

1. Using your preferred editor (Visual Studio, Rider, Visual Studio Code, or any other) open your .NET project.   
2. Add NuGet Package `CodeMash.Core`. For more information see: https://www.nuget.org/packages/CodeMash.Core
3. Add appsettings.json file if it does not already exist and enter CodeMash section defined below: 

**Example.**
```csharp
  {
    "CodeMash" : {
        "ApiKey" : "",
        "ApiUrl" : "https://api.codemash.io",
        "ProjectId" : ""
    }
  }
```

To get ApiKey and ProjectId values, you need to go to the settings of the CodeMash Project.
