[Get Code]

CodeMash.Sdk you can find at : https://github.com/codemash-io/CodeMash.Net

[Run Tests]

Ensure Test project is built (CodeMash.Net.Tests)
Change CodeMash section into app.config file regarding your account settings. http://api.codemash.io/

Run test using either Resharper or DotCover test runer (see installation into packages folder)

or you can run from command line (terminal)

specify full path like : mspec-clr4 C:\projects\CodeMash\Sdk\Net\CodeMash.Net.Tests\bin\Debug\CodeMash.Net.Tests.dll
or from MachineSpacifications\tools folder : mspec-clr4 ..\..\..\CodeMash.Net.Tests\bin\Debug\CodeMash.Net.Tests.dll

[RoadMap]
CodeMash.Net 1.0.0.1
----------------------------

ApiKey authentication 
CRUD support with MongoDB driver
SMTP send mails.
----------------------------


- Dependencies

MongoDB.Driver			2.3.0
Newtonsoft.Json			9.1.0
