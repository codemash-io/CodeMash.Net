[Get Code]

CodeMash.Sdk you can find at : https://djovaisas@bitbucket.org/djovaisas/CodeMash.sdk.git

[Run Tests]

Ensure Test project is built (CodeMash.Net.Tests)
Change CodeMash section into app.config file regarding your account settings. http://developers.CodeMash.com/connections/api

Run test using either Resharper or DotCover test runer (see installation into packages folder)

or you can run from command line (terminal)

specify full path like : mspec-clr4 C:\projects\CodeMash\Sdk\Net\CodeMash.Net.Tests\bin\Debug\CodeMash.Net.Tests.dll
or from MachineSpacifications\tools folder : mspec-clr4 ..\..\..\CodeMash.Net.Tests\bin\Debug\CodeMash.Net.Tests.dll

[RoadMap]
CodeMash.Net 2.2.1.3
----------------------------

Removed Basic Auth added, ApiKey authentication 


CodeMash.Net 2.2.1.0
----------------------------

Basic Auth added


CodeMash.Net 2.2.0.9
----------------------------

Sending Mail support
You can send mail either using CodeMash build in smtp or use your own.


CodeMash.Net 2.2.0.4
----------------------------

Removed responsibility from ServiceStack
updated MongoDB.Driver to 2.3
Full stack of basic functions - CRUD 


CodeMash.Net 2.2.0.3
----------------------------

FindAsync Aggregation has been added


CodeMash.Net 2.2.0.2
----------------------------

FindAsync projections has been added


CodeMash.Net 2.2
----------------------------

Changed package name to CodeMash.Net 
This package is usefull for Microsoft .NET applications. It can be used from applications like : Console Application, Microsoft ASP.NET MVC, Windows Forms, WPF, Windows Phone


Namespace was changed regarding new package name

Updated MongoDB Driver to v2.2 (Works with MongoDB v3.2)


CodeMash.Sdk.Net 2.1
----------------------------

.NET framework 4.5.2

Using standard MongoDB.Driver v2.1

Changed naming convention 

	CodeMash.Sdk.Net 
	CodeMash.Sdk.Net.DataContracts
	CodeMash.Sdk.Net.Tests	
	
All CRUD actions are async now
Changed HubClient to FsClient (Naming convention - FsClient represents CodeMash client)
CRUD actions removed, renamed and added new. Now CRUD actions are called identically to MongoDB.Driver : InsertOneAsync, UpdateManyAsync, DeleteManyAsync, FindAsync, FindAndReplaceAsync .....
Mail support. - If you setup your mail correctly in CodeMash.com Calling SendMail will send email to your recipients.


CodeMash.Sdk.Net.Web 1.0.1.6
----------------------------
File Upload

CodeMash.Sdk.Net.Web 1.0.1.5
----------------------------
Custom configuration 

CodeMash.Sdk.Net.Web 1.0.1.4
----------------------------
Paging and sorting 

CodeMash.Sdk.Net.Web 1.0.1.2
----------------------------
GetEntities method fixed 

CodeMash.Sdk.Net.Web 1.0.1.1
----------------------------

Support only for .NET 4.0 version. TODO : make it available for all .NET versions beginning from 3.5 later


CodeMash.Sdk.Net.Web 1.0.1.0
----------------------------

Removed ServiceStack as a web request client. Now we are using 
native .NET WebRequest instance for getting data out of web service.

CodeMash.Sdk.Net.Web 1.0.0.9
----------------------------

Fixed issue when editing document

CodeMash.Sdk.Net.Web 1.0.0.8
----------------------------

- Supported .NET version from 4.0

- Functionality 

1) CRUD
2) Upsert(Insert/Update) and Delete for embedded documents
3) Create User for CodeMash

- Tests run through 

Machine.Specifications			0.8.0
Machine.Specifications.Should	0.7.1

- Dependencies

mongocsharpdriver		1.8.3
Newtonsoft.Json			5.0.8
ServiceStack.Client		4.0.8
ServiceStack.Interfaces	4.0.8
ServiceStack.Text		4.0.8
