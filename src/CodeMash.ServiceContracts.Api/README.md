# CodeMash .NET SDK Tests




# CodeMash Hub Types (Update the reference.)

Generate types, run script from root directory of this repo
Install x tool. See https://docs.servicestack.net/dotnet-tool


Get API

```bash
x cs http://localhost:5002 ./src/CodeMash.ServiceContracts.Api/api 
```

Get Events API

```bash
x cs http://localhost:5010 ./src/CodeMash.ServiceContracts.Api/events.api 
```

TO UPDATE RUN:

```bash
x cs 
```


## Run tests

Run tests and enjoy the ride.
https://github.com/VerifyTests/Verify

Runner - NUnit
Rider Plugin - https://plugins.jetbrains.com/plugin/17240-verify-support (Download add to Rider from Disk.)

Test results are in folder "test_results"