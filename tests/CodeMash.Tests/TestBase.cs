using System.Reflection;

namespace CodeMash.Tests;

public class TestBase
{
    public CodeMashConfiguration AppSettings { get; set; }
    
    public TestBase()
    {
        AppSettings = new CodeMashConfiguration();
    }
    public static VerifySettings VerifySettings
    {
        get
        {
            var settings = new VerifySettings();
            
            settings.UseDirectory("test_results");
            settings.ScrubMembers<HttpResponseMessage>(_ => _.Headers); 
            settings.ScrubLines(line => line.Contains("Bearer"));
            settings.ScrubMember("Email");
            settings.ScrubMember("Cookies");
            settings.ScrubMember("ProjectId");
            return settings;
        }
    }
}


public static class TestBaseExtensions
{
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