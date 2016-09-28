CodeMash.MongoDb.Repository is an easy to use library to use MongoDB with .NET.
It implements a Repository pattern on top of Official MongoDB C# driver. Meaning, CodeMash.MongoDb.Repository serves as an intermidiate layer between Mongo DB and business layer of your .NET aplication.

1. [Getting started](https://bitbucket.org/justinas_jasiunas/test-for-wiki-documentation/wiki/Getting%20started%20)
2. [Connecting to database](https://bitbucket.org/justinas_jasiunas/test-for-wiki-documentation/wiki/Connecting%20to%20DB)
3. CRUD operations  
3.1. [Create](https://bitbucket.org/justinas_jasiunas/test-for-wiki-documentation/wiki/Create)  
3.2. [Read](https://bitbucket.org/justinas_jasiunas/test-for-wiki-documentation/wiki/Read)  
3.3. [Update](https://bitbucket.org/justinas_jasiunas/test-for-wiki-documentation/wiki/Update)  
3.4. [Delete](https://bitbucket.org/justinas_jasiunas/test-for-wiki-documentation/wiki/Delete)  

**Example**

```
#!C#

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
