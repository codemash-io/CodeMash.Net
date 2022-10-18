using System.Net.Http.Headers;

namespace CodeMash.Tests.v2;

[TestFixture]
public class GeneralTests: TestBase
{
    [Test]
    public void Cannot_Call_Api_When_Bad_ApiUri_Is_Specified()
    {
        var client = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5019"),
            DefaultRequestHeaders =
            {
                Authorization = new AuthenticationHeaderValue("Bearer", "someFakeToken")
            }
        };
        Assert.ThrowsAsync<HttpRequestException>(async () => await client.GetAsync($"/v2/projects/{AppSettings.ProjectId}"));
    }
    
    
    [Test]
    public async Task Cannot_Call_Api_When_Endpoint_Authentication_Is_Not_Set()
    {
        var client = new HttpClient
        {
            BaseAddress = new Uri(AppSettings.ApiBaseUri),
        };
        var response = await client.GetAsync($"/v2/projects/{AppSettings.ProjectId}");
        await Verify(response, VerifySettings);
    }
    
    
    [Test]
    public async Task Cannot_Call_Api_When_Endpoint_Authentication_Is_Bad()
    {
        var client = new HttpClient
        {
            BaseAddress = new Uri(AppSettings.ApiBaseUri),
            DefaultRequestHeaders =
            {
                Authorization = new AuthenticationHeaderValue("Bearer", $"asdasd")
            }
        };
        var response = await client.GetAsync($"/v2/projects/{AppSettings.ProjectId}");
        await Verify(response, VerifySettings);
    }
}