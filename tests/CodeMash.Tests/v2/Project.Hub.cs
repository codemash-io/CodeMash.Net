
using CodeMash.Tests.Types.Hub;

namespace CodeMash.Tests.v2;

public partial class ProjectTests
{
    [Test]
    public void Cannot_Create_New_Project_With_Bad_Zone()
    {
        Assert.ThrowsAsync<HttpRequestException>(async () => 
            await CodeMashProjectBuilder.New
                .CreateAccount()
                .SignInToHub()
                .CreateNewProject(new CreateProject { ProjectName = "Test", ZoneName = "NonExisting"})
                .Build());
    }
    
    [Test]
    public async Task Can_Create_New_Project()
    {

        var project = new CreateProject { ProjectName = "Test", ZoneName = "central-europe-1" };
        
        var output = await CodeMashProjectBuilder.New
            .CreateAccount()
            .SignInToHub()
            .CreateNewProject(project)
            .Build();
      

        await Verify(output, VerifySettings);
    }
}