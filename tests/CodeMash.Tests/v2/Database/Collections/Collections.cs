using CodeMash.Client;
using CodeMash.Interfaces.Database.Repository;
using CodeMash.Repository;

namespace CodeMash.Tests.v2;

[TestFixture]
public partial class Database
{
    public partial class Collections
    {
        public static readonly string EmployeesCollectionName = "employees";
        public static readonly string BusinessTripsCollectionName = "business-trips";
        public static readonly string AbsenceRequestsCollectionName = "absence-requests";
    
        public static readonly string SimpleTaxonomyDisplayName = "My Test Taxonomy";
        public static readonly string SimpleTaxonomyName = "my-test-taxonomy";
        public static readonly string CountriesTaxonomyName = "countries";
        public static readonly string AbsenceTypesTaxonomyName = "absence-types";
        public static readonly string StatesTaxonomyName = "states";
        public static readonly string CitiesTaxonomyName = "cities";
        
        public CodeMashConfiguration AppSettings { get; set; }
        protected IRepository<Employee> EmployeesRepo { get; set; }
        
        public CodeMashProjectBuilder.ProjectOutput Output { get; set; } = new();
        
        public Collections()
        {
            AppSettings = new CodeMashConfiguration();
        }


        [OneTimeSetUp]
        public async Task Start()
        {
            var countries = "/utils/db/terms/countries".ToTermsList(true);
            var absenceTypes = "/utils/db/terms/absence-types".ToTermsList();

            var businessTripsSchema =
                "/utils/db/schemas/collections/business-trips".ToSchema(BusinessTripsCollectionName);
            var absenceRequestsSchema =
                "/utils/db/schemas/collections/absence-requests".ToSchema(AbsenceRequestsCollectionName);

            Output = await CodeMashProjectBuilder.New
                .CreateAccount()
                .SignInToHub()
                .CreateNewProject()
                .CreateAdminAsServiceUser()
                .SetSupportedLanguagesForProject(new[] {"en", "lt"})
                .CreateNewRole("CEO")
                .CreateNewRole("HR")
                .CreateNewRole("Accountant")
                .EnableDatabase()
                .AddEmployeesTemplateSchema()
                .AddNewCollection(businessTripsSchema, new List<(string, string)>
                {
                    new("taxonomy", CountriesTaxonomyName),
                    new("collection", EmployeesCollectionName),
                })
                .AddNewCollection(absenceRequestsSchema, new List<(string, string)>
                {
                    new("taxonomy", AbsenceTypesTaxonomyName),
                    new("collection", EmployeesCollectionName),
                })
                .InsertTermsIntoTaxonomy(countries, CountriesTaxonomyName)
                .InsertTermsIntoTaxonomy(absenceTypes, AbsenceTypesTaxonomyName)
                .Build();
            
            var apiKey = Output.ApiAdminToken.Replace("Bearer: ", "");
            var settings = new CodeMashSettings(AppSettings.ApiBaseUri, apiKey, Output.ProjectId);
            var client = new CodeMashClient(settings);

            EmployeesRepo = new CodeMashRepository<Employee>(client);

        }
    }
}