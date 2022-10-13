using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using CodeMash.Tests.Types.Hub;

namespace CodeMash.Tests;

public record Schema
{
    public string UiSchema { get; set; }
    public string JsonSchema { get; set; }
    public string CollectionName { get; set; }
}


public class CodeMashProjectBuilder
{
    public class ProjectOutput
    {

        public Guid AccountId { get; set; }
        public Guid ProjectId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Guid UserId { get; set; }
        public Guid DatabaseClusterId { get; set; }

        // Cookies are not necessary to pass to each request,
        // because Hub doesn't have access by token yet.
        public CookieCollection Cookies { get; set; }

    }

    public static Builder New => new();


    public class Builder
    {
        public string UniqueEmail = $"{Guid.NewGuid()}@gmail.com";
        public ProjectOutput Output { get; set; }

        public CodeMashConfiguration AppSettings { get; set; }
        private readonly CancellationToken cancellationToken;
        private readonly Queue<Func<Task>> builderQueue;

        public Builder()
        {
            Output = new ProjectOutput();
            AppSettings = new CodeMashConfiguration();
            var cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;
            builderQueue = new Queue<Func<Task>>();
        }

        public Builder CreateAccount(CreateAccount? request = null)
        {
            builderQueue.Enqueue(() => Task.Run(async () =>
            {
                request ??= new CreateAccount
                {
                    FirstName = "Brad",
                    LastName = "Pitt",
                    Email = UniqueEmail,
                    Password = "aaa"
                };


                var postData = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

                var response = await RestClient.Hub().PostAsync("/account", postData, cancellationToken);
                response.EnsureSuccessStatusCode();

                var responseDto =
                    await response.Content.ReadFromJsonAsync<CreateAccountResponse>(
                        cancellationToken: cancellationToken);

                if (responseDto == null)
                {
                    throw new Exception("Cannot create an account");
                }

                var registrationSession =
                    new StringContent(
                        JsonSerializer.Serialize(
                            new CompleteRegisterAuthentication {SessionSecret = responseDto.Session}), Encoding.UTF8,
                        "application/json");
                await RestClient.Hub().PostAsync("/account/complete-register", registrationSession, cancellationToken);

                if (cancellationToken.IsCancellationRequested)
                {
                    cancellationToken.ThrowIfCancellationRequested();
                }

                Output.Password = request.Password;
                Output.Email = request.Email;
                Output.AccountId = responseDto.Result;

            }, cancellationToken));
            return this;
        }

        public Builder SignInToHub()
        {
            builderQueue.Enqueue(() => Task.Run(async () =>
            {
                var request = new StringContent(
                    JsonSerializer.Serialize(
                        new AuthenticateToAccount
                        {
                            UserName = Output.Email,
                            Password = Output.Password,
                            AccountId = Output.AccountId
                        }), Encoding.UTF8, "application/json");


                var cookies = new CookieContainer();
                var handler = new HttpClientHandler();
                handler.CookieContainer = cookies;
                var client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(AppSettings.HubBaseUri),
                    DefaultRequestHeaders =
                    {
                        Accept = {{new MediaTypeWithQualityHeaderValue("application/json")}},
                        // Authorization = new AuthenticationHeaderValue("Bearer", AppSettings.SysAdminToken)
                    }
                };

                var response = await client.PostAsync("/account/auth", request, cancellationToken);
                response.EnsureSuccessStatusCode();

                var responseDto =
                    await response.Content.ReadFromJsonAsync<AuthenticateToAccountResponse>(
                        cancellationToken: cancellationToken);

                if (responseDto == null)
                {
                    throw new Exception("Cannot authenticate");
                }

                Output.Cookies = cookies.GetCookies(new Uri(AppSettings.HubBaseUri));

            }, cancellationToken));
            return this;
        }

        public Builder CreateNewProject(CreateProject? request = null)
        {
            builderQueue.Enqueue(() => Task.Run(async () =>
            {
                request ??= new CreateProject
                {
                    ProjectName = "Test Project",
                    Description = "Some test project",
                    ZoneName = "central-europe-1",
                };


                var postData = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

                var response = await RestClient.Hub(new RequestContext {Cookies = Output.Cookies})
                    .PostAsync("/projects", postData, cancellationToken);
                response.EnsureSuccessStatusCode();

                var responseDto =
                    await response.Content.ReadFromJsonAsync<CreateProjectResponse>(
                        cancellationToken: cancellationToken);

                if (responseDto == null)
                {
                    throw new Exception("Cannot create a project");
                }

                Output.ProjectId = responseDto.Result;
                Thread.Sleep(1000);

            }, cancellationToken));
            return this;
        }

        public Builder EnableDatabase()
        {
            builderQueue.Enqueue(() => Task.Run(async () =>
            {
                var response = await RestClient
                    .Hub(new RequestContext {Cookies = Output.Cookies, ProjectId = Output.ProjectId})
                    .GetAsync("/db/enable?provider=CodeMash&freeRegion=eu-central-1", cancellationToken);
                response.EnsureSuccessStatusCode();

                var responseDto =
                    await response.Content.ReadFromJsonAsync<EstablishDatabaseResponse>(
                        cancellationToken: cancellationToken);

                if (responseDto == null)
                {
                    throw new Exception("Cannot enable db");
                }

                Output.DatabaseClusterId = responseDto.Result;
                Thread.Sleep(1000);

            }, cancellationToken));


            return this;
        }

        public Builder AddNewCollection(Schema schema)
        {
            builderQueue.Enqueue(() => Task.Run(async () =>
            {
                var request = new CreateSchema
                {
                    CollectionName = schema.CollectionName,
                    JsonSchema = schema.JsonSchema,
                    UiSchema = schema.JsonSchema
                };
                var postData = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
                var response = await RestClient
                    .Hub(new RequestContext {Cookies = Output.Cookies, ProjectId = Output.ProjectId})
                    .PostAsync("/db/schema", postData, cancellationToken);
                response.EnsureSuccessStatusCode();

                var responseDto =
                    await response.Content.ReadFromJsonAsync<CreateSchemaResponse>(
                        cancellationToken: cancellationToken);

                if (responseDto == null)
                {
                    throw new Exception("Cannot create schema");
                }

            }, cancellationToken));
            return this;
        }

        public async Task<ProjectOutput> Build()
        {
            Console.WriteLine(">>> Building CodeMash project...");
            while (builderQueue.TryDequeue(out var startTaskExecution))
            {
                var currentTask = startTaskExecution.Invoke();
                await currentTask.ConfigureAwait(false);
            }

            Console.WriteLine(">>> Done...");
            return Output;
        }
    }




}