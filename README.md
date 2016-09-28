# CodeMash.Net
SDK of CodeMash API - http://codemash.io/documentation/api

CodeMash.Net is an easy C# client for using common task - CRUD operations, emails, notifications, payments, logging.

1. [Getting started](https://github.com/codemash-io/CodeMash.Net/blob/master/1.%20Getting%20started.md)
2. [Connecting to database](https://github.com/codemash-io/CodeMash.Net/blob/master/2.%20Connecting%20to%20database.md)
3. CRUD operations  
3.1. [Create](https://github.com/codemash-io/CodeMash.Net/blob/master/3.1.%20Create.md)  
3.2. [Read](https://github.com/codemash-io/CodeMash.Net/blob/master/3.2.%20Read.md)  
3.3. [Update](https://github.com/codemash-io/CodeMash.Net/blob/master/3.3.%20Update.md)  
3.4. [Delete](https://github.com/codemash-io/CodeMash.Net/blob/master/3.4.%20Delete.md)  

**Example**

```csharp

using CodeMash.MongoDB.Repository;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB
{
    class Program
    {
        static void Main(string[] args)
        {
            var repo = new MongoRepository<Person>();

            // adding new entity
            var person = new Person
            {
                Name = "Vardenis",
                Age = 25
            };
            repo.InsertOne(person);

            // searching
            var result = repo.Find(x => x.Age == 25);

            // updating
            var filter = Builders<Person>.Filter.Eq("Age", "25");
            person.Age = 30;
            var update = Builders<Person>.Update.Set<Person>("Age", person);
            repo.UpdateOne(filter, update, null);
        }
    }

    // The Entity base-class is provided by MongoRepository
    // for all entities you want to use in MongoDb
    public class Person : Entity
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
```
