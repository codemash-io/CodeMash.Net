namespace CodeMash.Tests.v2;

[TestFixture]
public class ProjectTests: TestBase
{
    [Test]
    public async Task Cannot_Get_Project_Info_When_Bad_Format_ProjectId_Is_Provided()
    {
        var response = await RestClient.Api().GetAsync($"/v2/projects/asd");
        await Verify(response, VerifySettings);
    }
    
    [Test]
    // TODO: this gives forbidden because that is not implemented correctly on CM side.
    public async Task Cannot_Get_Project_Because_No_Authorization_Provided()
    {
        var response = await RestClient.Api().GetAsync($"/v2/projects/{AppSettings.ProjectId}");
        await Verify(response, VerifySettings);
    }
    
}