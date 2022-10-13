using System.Net.Http.Json;
using System.Text;
using Argon;
using CodeMash.Tests.Types.Api;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CodeMash.Tests.v2;

[TestFixture]
public partial class DatabaseTests: TestBase
{
    const string EmployeesCollectionName = "employees";
    public CodeMashProjectBuilder.ProjectOutput Output { get; set; } = new ();
    
    [OneTimeSetUp]
    public async Task Start()
    {
        Output = await CodeMashProjectBuilder.Newgit 
            .CreateAccount()
            .SignInToHub()
            .CreateNewProject()
            .EnableDatabase()
            .AddNewCollection("/utils/db/schemas/employees".ToSchema(EmployeesCollectionName))
            .Build();
        
    }
    
    
    [Test]
    public async Task Can_Add_A_New_Record_As_Json_In_Collection()
    {
        var request = new InsertOneRequest
        {
            Document = "{ 'first_name': 'Domantas', 'last_name': 'Jovaisas'}"
        };

        var postData = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8,"application/json");
        var response = await RestClient.Api(new RequestContext { Cookies = Output.Cookies, ProjectId = Output.ProjectId}).PostAsync($"/v2/db/{EmployeesCollectionName}", postData);
        response.EnsureSuccessStatusCode();
                    
        var responseDto = await response.Content.ReadFromJsonAsync<InsertOneResponse>();
        var target = JToken.Parse(responseDto.Result);
        
        await Verify(target, VerifySettings)
            .ScrubMember("_id")
            .ScrubMember("_meta");
    }
}