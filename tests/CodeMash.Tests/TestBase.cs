using System.Reflection;
using CodeMash.Client;
using CodeMash.Interfaces.Database.Repository;
using CodeMash.Repository;

namespace CodeMash.Tests;

public class TestBase
{
    public CodeMashConfiguration AppSettings { get; set; }
    
    public TestBase()
    {
        AppSettings = new CodeMashConfiguration();
    }
    
    // [OneTimeSetUp]
    // public async Task SetUp()
    // {
    //     var countries = "/utils/db/terms/countries".ToTermsList(true);
    //     var absenceTypes = "/utils/db/terms/absence-types".ToTermsList();
    //             
    //     var businessTripsSchema =
    //         "/utils/db/schemas/collections/business-trips".ToSchema(BusinessTripsCollectionName);
    //     var absenceRequestsSchema =
    //         "/utils/db/schemas/collections/absence-requests".ToSchema(AbsenceRequestsCollectionName);
    //             
    //     var output = await CodeMashProjectBuilder.New
    //         .CreateAccount()
    //         .SignInToHub()
    //         .CreateNewProject()
    //         .SetSupportedLanguagesForProject(new []{ "en", "lt"})
    //         .CreateNewRole("CEO")
    //         .CreateNewRole("HR")
    //         .CreateNewRole("Accountant")
    //         .EnableDatabase()
    //         .AddEmployeesTemplateSchema()
    //         .AddNewCollection(businessTripsSchema, new List<(string, string)>
    //         {
    //             new ("taxonomy", CountriesTaxonomyName),
    //             new ("collection", EmployeesCollectionName),
    //         })
    //         .AddNewCollection(absenceRequestsSchema, new List<(string, string)>
    //         {
    //             new ("taxonomy", AbsenceTypesTaxonomyName),
    //             new ("collection", EmployeesCollectionName),
    //         })
    //         .InsertTermsIntoTaxonomy(countries, CountriesTaxonomyName)
    //         .InsertTermsIntoTaxonomy(absenceTypes, AbsenceTypesTaxonomyName)
    //         .Build();
    //
    //     // Hub somehow returns it with Bearer.
    //     // This will be changed later.
    //     var apiKey = output.ApiAdminToken.Replace("Bearer: ", "");
    //     var settings = new CodeMashSettings(AppSettings.ApiBaseUri, apiKey, output.ProjectId);
    //     var client = new CodeMashClient(settings);
    //
    //     EmployeesRepo = new CodeMashRepository<Employee>(client);
        
        
    // }

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
            
            var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var testResultsPath = Path.Combine(buildDir ?? "", "../../../", "test_results");
            settings.UseDirectory(testResultsPath);
            
            settings.ScrubMembers<HttpResponseMessage>(_ => _.Headers); 
            settings.ScrubLines(line => line.Contains("Bearer"));
            settings.ScrubMember("Email");
            settings.ScrubMember("Cookies");
            settings.ScrubMember("ProjectId");
            return settings;
        }
    }
}


