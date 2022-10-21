using System.Net;
using System.Net.Http.Headers;
using CodeMash.Tests.Types.Api;
using CodeMash.Tests.Types.Hub;
using Isidos.CodeMash.Tests.ServiceLevel;
using UserRoleUpdateInput = CodeMash.Tests.Types.Hub.UserRoleUpdateInput;

namespace CodeMash.Tests;

public class CodeMashProjectBuilder
{

    public class ProjectOutput
    {

        public Guid AccountId { get; set; }
        public Guid ProjectId { get; set; }
        public Guid ApiUserId { get; set; }
        public string ApiAdminToken { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
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


                var response = await RestClient.Hub().PostAsync("/account", request.Serialize(), cancellationToken);
                response.EnsureSuccessStatusCode();

                var responseDto = await response.Deserialize<CreateAccountResponse>(cancellationToken);

                if (responseDto == null)
                {
                    throw new Exception("Cannot create an account");
                }

                Output.Password = request.Password;
                Output.Email = request.Email;
                Output.AccountId = responseDto.Result;

                Thread.Sleep(500);

            }, cancellationToken));
            return this;
        }

        public Builder SignInToHub()
        {
            builderQueue.Enqueue(() => Task.Run(async () =>
            {
                var request = new AuthenticateToAccount
                {
                    UserName = Output.Email,
                    Password = Output.Password,
                    AccountId = Output.AccountId
                };


                var cookies = new CookieContainer();
                var handler = new HttpClientHandler();
                handler.CookieContainer = cookies;
                var client = new HttpClient(handler)
                {
                    BaseAddress = new Uri(AppSettings.HubBaseUri),
                    DefaultRequestHeaders =
                    {
                        Accept = {{new MediaTypeWithQualityHeaderValue("application/json")}}
                    }
                };

                var response = await client.PostAsync("/account/auth", request.Serialize(), cancellationToken);
                response.EnsureSuccessStatusCode();

                var responseDto =
                    await response.Deserialize<AuthenticateToAccountResponse>(cancellationToken);

                if (responseDto == null)
                {
                    throw new Exception("Cannot authenticate");
                }

                Output.Cookies = cookies.GetCookies(new Uri(AppSettings.HubBaseUri));

                Thread.Sleep(500);

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


                // var response = await RestClient.Hub(new RequestContext {Cookies = Output.Cookies})
                var response = await RestClient.Hub(Output.ToRequestContext())
                    .PostAsync("/projects", request.Serialize(), cancellationToken);
                response.EnsureSuccessStatusCode();

                var responseDto = await response.Deserialize<CreateProjectResponse>(cancellationToken);

                if (responseDto == null)
                {
                    throw new Exception("Cannot create a project");
                }

                Output.ProjectId = responseDto.Result;
                Thread.Sleep(1000);

            }, cancellationToken));
            return this;
        }

        public Builder CreateAdminAsServiceUser()
        {
            builderQueue.Enqueue(() => Task.Run(async () =>
            {
                var request = new RegisterServiceUser
                {
                    DisplayName = "Project Administrator",
                    ProjectId = Output.ProjectId,
                    RolesTree = new List<UserRoleUpdateInput>
                        {new UserRoleUpdateInput {Role = "Administrator ", Policies = null}}
                };

                var response = await RestClient.Hub(Output.ToRequestContext())
                    .PostAsync("/membership/users/service/register", request.Serialize(), cancellationToken);

                response.EnsureSuccessStatusCode();

                var responseDto =
                    await response.Deserialize<RegisterServiceUserResponse>(cancellationToken);

                if (responseDto == null)
                {
                    throw new Exception("Cannot create a admin user as a service account");
                }



                Output.ApiUserId = Guid.Parse(responseDto.Result);

                var regenerateUserTokenRequest = new RegenerateServiceUserToken
                {
                    Id = Output.ApiUserId,
                    ProjectId = Output.ProjectId
                };


                var regenerateResponse = await RestClient.Hub(Output.ToRequestContext())
                    .PutAsync($"/membership/users/service/{responseDto.Result}/key/regenerate",
                        regenerateUserTokenRequest.Serialize(), cancellationToken);

                regenerateResponse.EnsureSuccessStatusCode();

                var regenerateResponseDto =
                    await regenerateResponse.Deserialize<RegenerateServiceUserTokenResponse>(cancellationToken);

                if (regenerateResponseDto == null)
                {
                    throw new Exception("Cannot regenerate token for newly created admin");
                }

                Output.ApiAdminToken = regenerateResponseDto.Result;

                Thread.Sleep(500);

            }, cancellationToken));
            return this;
        }

        public Builder EnableDatabase()
        {
            builderQueue.Enqueue(() => Task.Run(async () =>
            {
                var response = await RestClient
                    .Hub(Output.ToRequestContext())
                    .GetAsync("/db/enable?provider=CodeMash&freeRegion=eu-central-1", cancellationToken);
                response.EnsureSuccessStatusCode();

                var responseDto =
                    await response.Deserialize<EstablishDatabaseResponse>(cancellationToken);

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


                var response = await RestClient
                    .Hub(Output.ToRequestContext())
                    .PostAsync("/db/schema", request.Serialize(), cancellationToken);
                response.EnsureSuccessStatusCode();

                var responseDto =
                    await response.Deserialize<CreateSchemaResponse>(cancellationToken);

                if (responseDto == null)
                {
                    throw new Exception("Cannot create schema");
                }

                Thread.Sleep(500);

            }, cancellationToken));
            return this;
        }

        public Builder AddEmployeesTemplateSchema()
        {
            builderQueue.Enqueue(() => Task.Run(async () =>
            {
                var employeesSchemaTemplateId = "5e70ed82362de9480cc3598b";

                var request = new CreateSchemaFromTemplate
                {
                    Id = employeesSchemaTemplateId,
                    Names = new Dictionary<string, string>
                    {
                        {"employees", "Employees"},
                        {"companies", "Companies"},
                        {"addresses", "Addresses"},
                        {"countries", "Countries"},
                        {"cities", "Cities"},
                        {"areas", "Areas"},
                    },
                    Created = new[] {"employees", "companies", "addresses", "countries", "cities", "areas"}.ToList()
                };
                var response = await RestClient
                    .Hub(Output.ToRequestContext())
                    .PostAsync("/db/schemas/system-templates/" + employeesSchemaTemplateId,
                        request.Serialize(), cancellationToken);

                response.EnsureSuccessStatusCode();

                var responseDto =
                    await response.Deserialize<CreateSchemaFromTemplateResponse>(cancellationToken);

                if (responseDto == null)
                {
                    throw new Exception("Cannot create schemas from template" +
                                        employeesSchemaTemplateId);
                }

                Thread.Sleep(3000);
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