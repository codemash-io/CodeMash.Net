using System.Collections.Generic;
using System.Linq;
using CodeMash.Interfaces.Database.Repository;

namespace CodeMash.Core.Tests
{
    public abstract class DatabaseTestBase : TestBase
    {
        protected IRepository<SdkEntity> Repository { get; set; }
        
        protected Dictionary<string, string> InsertedRecords { get; set; } = new Dictionary<string, string>();
        
        protected SdkEntity entity;
        
        protected SdkEntity InsertRecord(int index = 0)
        {
            entity = new SdkEntity
            {
                Notes = "Notes",
                Number = 54 + index,
            };
            
            var response = Repository.InsertOne(entity);
            InsertedRecords[TestContext.TestName + index] = response.Result.Id;

            return response.Result;
        }

        public override void TearDown()
        {
            if (InsertedRecords != null && InsertedRecords.Any())
            {
                var testRecords = InsertedRecords.Where(x => x.Key.StartsWith(TestContext.TestName)).Select(x => x.Value).ToList();
                if (testRecords.Any())
                {
                    foreach (var testRecord in testRecords)
                    {
                        Repository.DeleteOne(testRecord);
                    }
                }
            }
        }
    }
}