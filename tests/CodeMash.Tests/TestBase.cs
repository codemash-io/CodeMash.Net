using CodeMash.Client;
using CodeMash.Interfaces.Database.Repository;
using CodeMash.Repository;
using Isidos.CodeMash.Tests.ServiceLevel;

namespace CodeMash.Tests;

public class TestBase
{
    public CodeMashConfiguration AppSettings { get; set; }
    protected IRepository<Employee> EmployeesRepo { get; set; }
    
    public TestBase()
    {
        AppSettings = new CodeMashConfiguration();
    }
    
    [SetUp]
    public async Task SetUp()
    {
        var output = await CodeMashProjectBuilder.New 
            .CreateAccount()
            .SignInToHub()
            .CreateNewProject()
            .EnableDatabase()
            .AddNewCollection("/utils/db/schemas/employees".ToSchema("employees"))
            .CreateAdminAsServiceUser()
            .Build();

        // Hub somehow returns it with Bearer.
        // This will be changed later.
        var apiKey = output.ApiAdminToken.Replace("Bearer: ", "");
        var settings = new CodeMashSettings(AppSettings.ApiBaseUri, apiKey, output.ProjectId);
        var client = new CodeMashClient(settings);

        EmployeesRepo = new CodeMashRepository<Employee>(client);
        
        
    }

    [TearDown]
    public void TearDown()
    {
        // Container.Dispose();
    }

    
    public static VerifySettings VerifySettings
    {
        get
        {
            var settings = new VerifySettings();
            
            settings.UseDirectory("test_results");
            settings.ScrubMembers<HttpResponseMessage>(_ => _.Headers); 
            settings.ScrubLines(line => line.Contains("Bearer"));
            settings.ScrubMember("Id");
            settings.ScrubMember("ProjectId");
            return settings;
        }
    }
}