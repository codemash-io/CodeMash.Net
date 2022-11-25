namespace CodeMash.Tests.v2;

[TestFixture]
public partial class Database : TestBase
{
    [TestFixture]
    public class InsertOne: Collections
    {
        // TODO: rich insert - linked collections, taxonomies, roles, nested forms. 
        // TODO: validation
        // TODO: bypass validation
        // TODO: wait for upload
        // TODO: ignore triggers
        // TODO: set responsible
        // TODO: ResolveProviderFiles
                
        [Test]
        public async Task Can_Insert_New_Record()
        {
            var employee = new Employee
            {
                FirstName = "Domantas",
                LastName = "Jovaisas"
            };
                
            var target = await EmployeesRepo.InsertOneAsync(employee);

            await Verify(target, VerifySettings)
                .ScrubMember("Id");
        }
    }
}