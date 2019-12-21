# Step 3: Set up API keys

To make calls to our servers and access your resources you will need 2 keys - project ID and a secret key. 

1. Login to <a target="_blank" href="https://cloud.codemash.io">CodeMash dashboard</a>.
2. Inside your CodeMash dashboard select or create a project.
3. Inside your project settings find settings called **Project ID** and **Secret key**.
    * For **Project ID** simply copy it.
    * For **Secret Key** press **_Regenerate_** and then copy generated token. The token will not be accessible after you close it, you will need to regenerate it.

## Storing tokens
Your **Project ID** is not secret and can be visibly shown. Your **Secret key** should not be visible in public places as it would allow anyone to access your resources.

### Environment variables (Recommended)

To store your tokens in private use environment variables. This way you won't expose your secret key to public places.

### App Settings (Not recommended)

You can store your tokens in **appsettings.json** (name can be different) file which is in your base project folder. CodeMash sdk expects the following format:

```csharp
  {
    "CodeMash" : {
        "ApiKey" : "{YOUR_SECRET_KEY}",
        "ProjectId" : "{YOUR_PROJECT_ID}"
    }
  }
```

Set up is complete! Back to main page - [Main page](https://github.com/codemash-io/CodeMash.Net/blob/master/README.md).