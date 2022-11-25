using System.Net.Http.Json;
using System.Reflection;
using System.Text;
using CodeMash.ServiceContracts.Api;
using CodeMash.Tests.Types.Hub;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using ServiceStack.Text;
using File = System.IO.File;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CodeMash.Tests;

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

        var schemaParsedAsJson = JObject.Parse(jsonSchema);
        

        return new Schema
        {
            JsonSchema = jsonSchema,
            UiSchema = uiSchema,
            CollectionName = collectionName.ToLower(),
            CollectionNameAsTitle = schemaParsedAsJson["title"].ToString()
            
        };
    }
    
    
    // This hack is due to bad API behaviour. It expects string as document, 
    // but don't work properly with stringified JSON.
    private static string CleanUp(string serializedObject) =>
        serializedObject.Replace("\"{", "{")
            .Replace("}\"", "}")
            .Replace("\\\"", "\"");
    
    
    public static List<CreateTerm> ToTermsList(this string source, bool includeMeta = false, bool includeParentResolver = false)
    {
        var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        
        var documentsAsJson = File.ReadAllText($"{buildDir}{source}.json");

        var jsonItems = JsonArrayObjects.Parse(documentsAsJson);

        var terms = new List<CreateTerm>();

        foreach (var jsonItem in jsonItems)
        {
            if (includeMeta || includeParentResolver)
            {
                var all = jsonItem.Values.ToList();
                
                if (includeMeta)
                {
                    terms.Add(new CreateTerm
                    {
                        Document = all[0],
                        Meta = all[1],
                        Parent = includeParentResolver ? all[2]: null
                    });
                }
                else
                {
                    terms.Add(new CreateTerm
                    {
                        Document = all[0],
                        Parent = all[1]
                    });
                }   
            }
            else
            {
                var jsonAsString = JsonConvert.SerializeObject(jsonItem);

                terms.Add(new CreateTerm
                {
                    Document = CleanUp(jsonAsString)
                });
            }
        }
        return terms;

        
    }
}