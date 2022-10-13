# CodeMash .NET SDK Tests

## Prerequisites

Install Docker. 
Install Docker Compose. 
Run docker-compose.yaml file
Rebuild project. 


# CodeMash Hub Types (Update the reference.)

Generate types, run script from root directory of this repo
Install x tool. See https://docs.servicestack.net/dotnet-tool

```bash
x cs http://localhost:5001 ./tests/CodeMash.Tests/utils/types/v2/hub 
x cs http://localhost:5002 ./tests/CodeMash.Tests/utils/types/v2/api 
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