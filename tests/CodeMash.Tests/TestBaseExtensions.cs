using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using System.Text.Json;
using CodeMash.Tests;
using CodeMash.Tests.Types.Api;
using File = System.IO.File;

namespace Isidos.CodeMash.Tests.ServiceLevel;

public static class TestBaseExtensions
{
    public static RequestContext ToRequestContext(this CodeMashProjectBuilder.ProjectOutput output,
        bool useApiToken = false)
    {
        return useApiToken
            ? new RequestContext {ApiKey = output.ApiAdminToken, ProjectId = output.ProjectId}
            : new RequestContext {Cookies = output.Cookies, ProjectId = output.ProjectId};
    }
    
    
    public static StringContent Serialize(this object request)
    {
        return new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
    }

    public static async Task<T?> Deserialize<T>(this HttpResponseMessage response, CancellationToken cancellationToken = default) where T : new()
    {
        return await response.Content.ReadFromJsonAsync<T>(
            cancellationToken: cancellationToken);
    }
       
    
    public static Schema ToSchema(this string source, string collectionName)
    {
        var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        
        var jsonSchema = File.ReadAllText($"{buildDir}{source}/schema.json");
        var uiSchema = File.ReadAllText($"{buildDir}{source}/ui.json");
        
        return new Schema
        {
            JsonSchema = jsonSchema,
            UiSchema = uiSchema,
            CollectionName = collectionName.ToLower()
        };
    }
}