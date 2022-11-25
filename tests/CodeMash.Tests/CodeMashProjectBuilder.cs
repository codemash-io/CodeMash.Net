using System.Net;
using System.Net.Http.Headers;
using CodeMash.ServiceContracts.Api;
using CodeMash.Tests.Types.Hub;
using Newtonsoft.Json.Linq;
using ServiceStack.Text;
using UserRoleUpdateInput = CodeMash.ServiceContracts.Api.UserRoleUpdateInput;


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
        public List<string> TaxonomyRecordsIds { get; set; } = new ();
        
        // Cookies are not necessary to pass to each request,
        // because Hub doesn't have access by token yet.
        public CookieCollection Cookies { get; set; }
        
        public Guid PushNotificationTemplateId { get; set; }

    }
    
    public class SchemaResolveContext
    {
        public List<(string, string)> ResolveTokens { get; set; }

        public RequestContext RequestContext { get; set; }
        
        
    }
    
    public record TaxonomyTerms(string Id, List<Term> Terms);

    public static Builder New => new();


    public class Builder
    {
        public string UniqueEmail = $"{Guid.NewGuid()}@gmail.com";
        public ProjectOutput Output { get; set; }

        public CodeMashConfiguration AppSettings { get; set; }
        private readonly CancellationToken cancellationToken;
        private readonly Queue<Func<Task>> builderQueue;
        
        
        private void ReplaceToken(JToken? properties, string propertyType, string propertyValue, string replaceWith)
        {
            if (properties == null)
                return;
            
            foreach (var jToken in properties)
            {
                var prop = (JProperty) jToken;
                var sourceProperty = prop.Value["source"];

                // if (sourceProperty != null && sourceProperty.Value<string>("type") == propertyType && sourceProperty.Value<string>(propertyType) == propertyValue)
                if (sourceProperty != null && sourceProperty.Value<string>(propertyType) == propertyValue)
                {
                    sourceProperty[propertyType] = replaceWith;

                    if (sourceProperty.Value<string>("type") == "collections")
                    {
                        var url = sourceProperty.Value<string>("url");
                        sourceProperty["url"] = url?.Replace($"/db/{propertyValue}/find", $"/db/{replaceWith}/find");
                    }
                }
            }
        }

        private void GoThroughReferencesAndReplaceTokens(List<(string sourceType, string sourceValue)> references,
            Func<string, string?> findInTaxonomiesFunc, Func<string, string?> findInCollectionsFunc,
            ref Schema schema, Action<JToken?, string, string, string> replaceToken)
        {
            foreach (var reference in references)
            {
                var referenceType = reference.sourceType;
                var referencedSourceId = reference.sourceValue;
                var replaceWith = string.Empty;

                if (findInTaxonomiesFunc != null && referenceType == "taxonomy")
                {
                    replaceWith = findInTaxonomiesFunc(referencedSourceId);
                }

                if (findInCollectionsFunc != null && referenceType == "collection")
                {
                    replaceWith = findInCollectionsFunc(referencedSourceId);
                }

                var jsonParsed = JObject.Parse(schema.JsonSchema);
                var properties = jsonParsed["properties"];

                replaceToken(properties, referenceType, referencedSourceId, replaceWith ?? referencedSourceId);

                foreach (JProperty prop in properties)
                {
                    var nestedJsonProperty = prop.Value;
                    
                    var propType = nestedJsonProperty.Value<string>("type");

                    // nested
                    if (propType == "array")
                    {
                        var nestedFormProperties = nestedJsonProperty["items"]["properties"];
                        replaceToken(nestedFormProperties, referenceType, referencedSourceId, replaceWith ?? referencedSourceId);
                    }
                }

                schema.JsonSchema = jsonParsed.ToString();
            }
        }

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
        
        public Builder CreateNewRole(string roleName, List<string>? policies = null)
        {
            builderQueue.Enqueue(() => Task.Run(async () =>
            {
                var request = new CreateRole
                {
                    Name = roleName,
                    Policies = policies ?? new List<string>()
                };

                var response = await RestClient.Hub(Output.ToRequestContext())
                    .PostAsync("/membership/roles", request.Serialize(), cancellationToken);
                response.EnsureSuccessStatusCode();

                var responseDto = await response.Deserialize<CreateRoleResponse>(cancellationToken);

                if (responseDto == null)
                {
                    throw new Exception("Cannot create role for a project");
                }

                
                Thread.Sleep(200);
                
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
        
        public Builder SetSupportedLanguagesForProject(string[] languages)
        {
            builderQueue.Enqueue(() => Task.Run(async () =>
            {
                var request = new UpdateProjectLanguages
                {
                    Languages = languages.ToList(),
                    ProjectId = Output.ProjectId
                };

                var response = await RestClient.Hub(Output.ToRequestContext())
                    .PostAsync("/project/languages", request.Serialize(), cancellationToken);
                response.EnsureSuccessStatusCode();

                var responseDto = await response.Deserialize<UpdateProjectLanguagesResponse>(cancellationToken);


                if (responseDto == null)
                {
                    throw new Exception("Cannot create a project");
                }

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
                    RolesTree = new List<CodeMash.Tests.Types.Hub.UserRoleUpdateInput>
                        {new  CodeMash.Tests.Types.Hub.UserRoleUpdateInput {Role = "Administrator ", Policies = new List<Types.Hub.UserPolicyUpdateInput>()}}
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

                var firstCluster = responseDto.DatabaseClusters.First();

                Output.DatabaseClusterId = Guid.Parse(firstCluster.Id);
                Thread.Sleep(1000);

            }, cancellationToken));


            return this;
        }

        public Builder AddNewCollection(Schema schema, List<(string, string)>? references = null)
        {
            builderQueue.Enqueue(() => Task.Run(async () =>
            {
                if (references != null && references.Any())
                {
                    var taxonomiesResponse = await RestClient
                        .Hub(Output.ToRequestContext())
                        .GetAsync($"/db/taxonomies");
                    
                    taxonomiesResponse.EnsureSuccessStatusCode();
                    
                    var taxonomies = await taxonomiesResponse.Deserialize<GetTaxonomiesResponse>();
                    
                    
                    var schemasResponse = await RestClient
                        .Hub(Output.ToRequestContext())
                        .GetAsync($"/db/schemas");
                    
                    schemasResponse.EnsureSuccessStatusCode();
                    
                    var schemas = await schemasResponse.Deserialize<GetSchemasResponse>();

                    string FindInTaxonomies(string taxName)
                    {
                        var taxonomy = taxonomies.Result.Find(x => x.Name == taxName);
                        var taxonomyId = taxonomy?.Id;
                        return taxonomyId;
                    }

                    string FindInCollections(string collectionName)
                    {
                        var schemaDto = schemas.Result.Find(x => x.Name == collectionName);
                        var schemaId = schemaDto?.Id;
                        return schemaId;
                    }

                    GoThroughReferencesAndReplaceTokens(references, FindInTaxonomies, FindInCollections, ref schema, ReplaceToken);

                    schema.JsonSchema = schema.JsonSchema.Replace("\n", "");

                }

                var request = new CreateSchema
                {
                    CollectionName = schema.CollectionNameAsTitle,
                    JsonSchema = schema.JsonSchema,
                    UiSchema = schema.UiSchema,
                    
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
                        {"absence-types", "Absence Types"},
                        {"countries", "Countries"},
                        {"cities", "Cities"},
                        {"departments", "Departments"},
                        {"divisions", "Divisions"},
                        {"job-titles", "Job Titles"},
                        {"office-locations", "Office Locations"},
                    },
                    Created = new[] {"employees", "absence-types", "countries", "cities", "departments", "divisions", "job-titles", "office-locations"}.ToList()
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

        public Builder AddNewTaxonomy(CreateTaxonomy? request = null, bool takeParentFromLastInsertedTaxonomy = false, int[]? parentIndexes = null)
        {
            builderQueue.Enqueue(() => Task.Run(async () =>
            {
                request ??= new CreateTaxonomy
                {
                    Title = "My Test Taxonomy",
                    Description = "Taxonomy for testing purpose"
                };
                

                request.Parent = takeParentFromLastInsertedTaxonomy
                    ? Output.TaxonomyRecordsIds.Last()
                    : string.Empty;

                if (parentIndexes != null)
                {
                    request.Parent = null;
                    request.Dependencies ??= new List<string>();

                    foreach (var parentIndex in parentIndexes)
                    {
                        request.Dependencies.Add(Output.TaxonomyRecordsIds[parentIndex]);    
                    }
                }

                if (string.IsNullOrEmpty(request.JsonTermsMetaSchema) || request.JsonTermsMetaSchema == "{}")
                {
                    request.JsonTermsMetaSchema =
                        "{\"title\":\"\",\"type\":\"object\",\"required\":[],\"translatable\":[],\"properties\":{}}";
                }
                
                var response = await RestClient
                    .Hub(Output.ToRequestContext())
                    .PostAsync("/db/taxonomy",
                        request.Serialize(), cancellationToken);

                response.EnsureSuccessStatusCode();

                var responseDto =
                    await response.Deserialize<CreateTaxonomyResponse>(cancellationToken);

                if (responseDto == null)
                {
                    throw new Exception("Cannot create taxonomy");
                }

                Output.TaxonomyRecordsIds.Add(responseDto.RecordId);
                Thread.Sleep(3000);

            }, cancellationToken));
            return this;
        }

        public Builder InsertTermsIntoTaxonomy(List<CreateTerm> createTermRequests, string taxonomyName, string[]? parentTaxonomyNames = null)
        {
            builderQueue.Enqueue(() => Task.Run(async () =>
            {
                var parentTermsByTaxonomy = new Dictionary<string, TaxonomyTerms>();
                
                if (parentTaxonomyNames is {Length: > 0})
                {
                    foreach (var parentTaxonomyName in parentTaxonomyNames)
                    {
                        var findParentTaxonomyTermsResponse = await RestClient
                            .Api(Output.ToRequestContext())
                            .GetAsync($"/db/taxonomies/{parentTaxonomyName}/terms?IncludeTaxonomy=true");
                    
                        findParentTaxonomyTermsResponse.EnsureSuccessStatusCode();
                    
                        var parentTaxonomyTermsDto = await findParentTaxonomyTermsResponse.Deserialize<FindTermsResponse>();

                        if (parentTaxonomyTermsDto != null)
                        {
                            parentTermsByTaxonomy.Add(parentTaxonomyName, new TaxonomyTerms(parentTaxonomyTermsDto.Taxonomy.Id, parentTaxonomyTermsDto.Result));    
                        }
                    }
                }
                
                var parentTerms = new List<Term>();

                if (parentTaxonomyNames is {Length: 1})
                {
                    if (parentTermsByTaxonomy.ContainsKey(parentTaxonomyNames[0]))
                    {
                        var parentTaxonomyTerms = parentTermsByTaxonomy.First(x => x.Key == parentTaxonomyNames[0]);
                        parentTerms = parentTaxonomyTerms.Value.Terms;
                    }
                }


                foreach (var termRequest in createTermRequests)
                {
                    termRequest.CollectionName = taxonomyName;
                    
                    if (parentTaxonomyNames != null)
                    {
                        if (parentTaxonomyNames.Length == 1)
                        {
                            var parentName = termRequest.Parent;
                            
                            var parentTerm = parentTerms.Find(x => x.Name == parentName);

                            if (parentTerm != null)
                            {
                                termRequest.Parent = parentTerm.Id;    
                            }
                        }
                        
                        if (parentTaxonomyNames.Length > 1)
                        {
                            termRequest.Dependencies ??= new Dictionary<string, List<string>>();
                            
                            var parentResolverJsonArray = JsonArrayObjects.Parse(termRequest.Parent);
                            termRequest.Parent = null;

                            foreach (var parentResolverSettings in parentResolverJsonArray)
                            {
                                var parentTaxonomyName = parentResolverSettings["parentTaxonomy"];
                                var parentTermName = parentResolverSettings["parentTerm"];
                                
                                var parentTaxonomyTerms = parentTermsByTaxonomy.First(x => x.Key == parentTaxonomyName);
                                
                                var parentTerm = parentTaxonomyTerms.Value.Terms.Find(x => x.Name == parentTermName);

                                if (parentTerm != null)
                                {
                                    termRequest.Dependencies.Add(parentTaxonomyTerms.Value.Id, new List<string> { parentTerm.Id });     
                                }
                            }
                        }
                    }

                    var response =  await RestClient
                        .Hub(Output.ToRequestContext())
                        .PostAsync($"/db/taxonomies/{taxonomyName}",
                            termRequest.Serialize(), cancellationToken);

                    response.EnsureSuccessStatusCode();
                }                
                
                Thread.Sleep(3000);

            }, cancellationToken));
            return this;
        }
        
        
        public Builder AddPushNotificationTriggerToDatabaseInsert(bool includePreActionFilter = false)
        {
            builderQueue.Enqueue(() => Task.Run(async () =>
            {
                var preActionFilter =
                    "// You can use \"Old\" and \"New\" collection records passed into this function\n// Old - passed on delete and update.\n// New - passed on insert and update.\n\n// Write your custom C# code here\n// Your code has to return true of false\n// Trigger activates if this function returns true\nreturn true;";

                if (includePreActionFilter)
                {
                    var date = ((DateTimeOffset)new DateTime(1982, 9, 17)).ToUnixTimeSeconds();
                    preActionFilter = $"var age = Cm.GetProperty(New, \"birth_date\"); return age > {date};";
                }
                
                var request = new CreateCollectionTrigger
                {
                    
                    CollectionName = "employees",
                    Trigger = new SchemaTriggerCreateDto
                    {
                        Name = "Test Trigger",
                        Action = "{\"templateId\":\"" + Output.PushNotificationTemplateId + "\",\"tokens\":[{\"to\":\"Name\",\"from\":\"first_name\",\"provider\":\"NewRecord\"}],\"notificationType\":\"Create\",\"recipientType\":\"AllUsers\"}",
                        ActionType = "Notification",
                        ActivationCode = preActionFilter,
                        Types = new List<string> { "Insert" },
                        Clusters =  new List<string> { Output.DatabaseClusterId.ToString() }    
                    },
                    
                    
                };
                
                var response = await RestClient
                    .Hub(Output.ToRequestContext())
                    .PostAsync("/db/triggers",
                        request.Serialize(), cancellationToken);

                response.EnsureSuccessStatusCode();

                var responseDto =
                    await response.Deserialize<CreateCollectionTriggerResponse>(cancellationToken);

                if (responseDto == null)
                {
                    throw new Exception("Cannot add trigger to the collection");
                }

                Thread.Sleep(3000);

            }, cancellationToken));
            return this;
        }
        
        
        public Builder EnableExpoPushService()
        {
            builderQueue.Enqueue(() => Task.Run(async () =>
            {
                var request = new EnableNotification
                {
                    Model = new PushAccountProperties
                    {
                        DisplayName = "Expo",
                        PushProvider = PushProvider.Expo
                    }
                };
                
                var response = await RestClient
                    .Hub(Output.ToRequestContext())
                    .PostAsync("/notifications/push/enable",
                        request.Serialize(), cancellationToken);

                response.EnsureSuccessStatusCode();

                var responseDto =
                    await response.Deserialize<EnableNotificationResponse>(cancellationToken);

                if (responseDto == null)
                {
                    throw new Exception("Cannot enable expo push notification service");
                }

                Thread.Sleep(3000);

            }, cancellationToken));
            return this;
        }
        
        
        public Builder EnableLogsService()
        {
            builderQueue.Enqueue(() => Task.Run(async () =>
            {
                var request = new EnableLogging();
                
                var response = await RestClient
                    .Hub(Output.ToRequestContext())
                    .GetAsync("/logging/enable", cancellationToken);

                response.EnsureSuccessStatusCode();

                var responseDto =
                    await response.Deserialize<EnableLoggingResponse>(cancellationToken);

                if (responseDto == null)
                {
                    throw new Exception("Cannot enable logging service");
                }

                Thread.Sleep(3000);

            }, cancellationToken));
            return this;
        }
        
        public Builder AddNewPushTemplate()
        {
            builderQueue.Enqueue(() => Task.Run(async () =>
            {
                var request = new CreateNotificationTemplate
                {
                    TemplateName = "Test template",
                    Title = "Hello",
                    Body = "Hello @Model.Name"
                };
                
                var response = await RestClient
                    .Hub(Output.ToRequestContext())
                    .PostAsync("/notifications/push/templates",
                        request.Serialize(), cancellationToken);

                response.EnsureSuccessStatusCode();

                var responseDto =
                    await response.Deserialize<CreateNotificationTemplateResponse>(cancellationToken);

                if (responseDto == null || !responseDto.Result.HasValue)
                {
                    throw new Exception("Cannot create push notification template");
                }

                Output.PushNotificationTemplateId = responseDto.Result.Value;
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