# Step 2: Install CodeMash via NuGet

To make easier use of our services we suggest downloading our provided SDK.

1. Using your preferred editor (Visual Studio, Rider, Visual Studio Code or any other) open your .NET project.
2. Add NuGet Package using one of the following ways
   * Search for `CodeMash.Core` in package manager,
   * or run the command ` Install-Package CodeMash.Core`.

For more information about CodeMash NuGet package click [here](https://www.nuget.org/packages/CodeMash.Core).

Package `CodeMash.Core` includes all of SDK services.

#### Dependencies
* Servicestack

Continue the set up process - **Step 3**: [Set up API keys](https://github.com/codemash-io/CodeMash.Net/blob/master/docs/Set%20up%20API%20keys.md).


## Other packages

If you don't want to install all of the SDK but just some of the services - read below.

### `Install-Package CodeMash.Client`

This package only provides the API client to call CodeMash servers.

#### Dependencies
* Servicestack

### `Install-Package CodeMash.Interfaces`

This package contains all of the interfaces used by other packages.

#### Dependencies
* Servicestack

### `Install-Package CodeMash.Repository`

This package contains all of the methods for **Database** service.

#### Dependencies
* Servicestack