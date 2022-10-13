namespace CodeMash.Tests.v2;

[TestFixture]
public partial class DatabaseTests
{
    [Test]
    public async Task Cannot_Filter_Collection_When_Non_Authorized()
    {
        var response = await RestClient.Api().GetAsync($"/v2/db/employees/find");
        await Verify(response, VerifySettings);
    }
    
    [Test]
    public async Task Can_Establish_New_Database_Connection()
    {
        var output = await CodeMashProjectBuilder.New
            .CreateAccount()
            .SignInToHub()
            .CreateNewProject()
            .EnableDatabase()
            .Build();
      

        await Verify(output, VerifySettings);
    }
    
    [Test]
    public async Task Can_Add_New_Collection()
    {
        var output = await CodeMashProjectBuilder.New
            .CreateAccount()
            .SignInToHub()
            .CreateNewProject()
            .EnableDatabase()
            .AddNewCollection("/utils/db/schemas/employees".ToSchema("Employees"))
            .Build();
      

        await Verify(output, VerifySettings);
    }
}