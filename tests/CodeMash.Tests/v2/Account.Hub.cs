namespace CodeMash.Tests.v2;

public class AccountTests: TestBase
{
    [Test]
    public async Task Can_Create_A_New_Account()
    {

        var output = await CodeMashProjectBuilder.New
            .CreateAccount()
            .Build();

        await Verify(output, VerifySettings);
    }
}