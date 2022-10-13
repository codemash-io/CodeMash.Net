namespace CodeMash.Tests.v2;

public class MembershipTests: TestBase
{
    [Test]
    public async Task Can_Login_To_Hub_When_New_Account_Has_Been_Created()
    {
        var output = await CodeMashProjectBuilder.New
            .CreateAccount()
            .SignInToHub()
            .Build();


        await Verify(output, VerifySettings);

    }
}