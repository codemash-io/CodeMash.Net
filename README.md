# CodeMash.Net
CodeMash tools for .NET developers - <a target="_blank" href="http://codemash.io/documentation/api">http://codemash.io/documentation/api</a> 

CodeMash is a vital toolset for each developer which wants to achive daily development tasks in a rapid way : MondoDb as a service, one common payment gateway, central logging for whole your company applications, sending mails and sms messages easily, Identity provider, integrate your third party software vendors easily... More about that http://codemash.io

1. [Getting started](https://github.com/codemash-io/CodeMash.Net/blob/master/1.%20Getting%20started.md)
2. [Get ApiKey](https://github.com/codemash-io/CodeMash.Net/blob/master/2.%20Get%20ApiKey.md)
3. [Connecting to database](https://github.com/codemash-io/CodeMash.Net/blob/master/3.%20Connecting%20to%20database.md)  
	3.1. [Create](https://github.com/codemash-io/CodeMash.Net/blob/master/3.1.%20Create.md)  
  	3.2. [Read](https://github.com/codemash-io/CodeMash.Net/blob/master/3.2.%20Read.md)  
  	3.3. [Update](https://github.com/codemash-io/CodeMash.Net/blob/master/3.3.%20Update.md)  
  	3.4. [Delete](https://github.com/codemash-io/CodeMash.Net/blob/master/3.4.%20Delete.md)  

**Example**

```csharp

using System;
using CodeMash.Net;
using MongoDB.Driver;

namespace ConsoleApplication
{
    [CollectionName("Actors")]
    public class Person : EntityBase
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }

    class Program
    {
        
        static void Main(string[] args)
        {
            var person = new Person
            {
                Name = "Brad Pitt",
                Age = 55,
                Email = "guessbradsemailaddress@gmail.com"
            };

            Db.InsertOne(person);

            Mailer.SendMail(
				person.Email, 
				"CodeMash - it just works", 
				$"Hi Brad, yours id is - {person.Id}", 
				"support@codemash.io");

            var bradPittByName = Db.FindOne<Person>(x => x.Name == "Brad Pitt");
            var bradPittById = Db.FindOneById<Person>(person.Id.ToString());
            
			var filter = Builders<Person>.Filter.Eq(x => x.Name, "Brad Pitt");
			var bradPittByMongoDbNotation = Db.FindOne<Person>(filter);

            var allActorsCount = Db.Count<Person>(x => true);

            Console.WriteLine($"Actors on database: {allActorsCount}");
            Console.ReadLine();
        }
    }
}
```
