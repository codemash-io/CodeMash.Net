namespace CodeMash.Tests.v2;

[TestFixture]
public partial class Database
{
    [TestFixture]
    public partial class Collections
    {
        [TestFixture]
        public class InsertOne : TestBase
        {
            [Test]
            public async Task Can_Insert_A_New_Record()
            {
                var employee = new Employee
                {
                    FirstName = "Domantas",
                    LastName = "Jovaisas"
                };
        
                var target = await EmployeesRepo.InsertOneAsync(employee);
                await Verify(target, VerifySettings);
            }        
        }
    }
    
    
}