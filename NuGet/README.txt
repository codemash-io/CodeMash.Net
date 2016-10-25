[Get Started]
-----------------------------------------------------------------------------------------------------------------------------------
add codemash configuration to either web.config or app.config file as follows :


<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <configSections>
    <section name="CodeMash" type="CodeMash.Net.CodeMashConfigurationSection, CodeMash.Net" requirePermission="false" />
  </configSections>
  <CodeMash>
    <client apiKey="HP7qoWW**************LctB7IkU" address="http://api.codemash.io/1.0/" />
  </CodeMash>  
</configuration>

if you don't have apiKey please create an account on http://api.codemash.io and got to http://api.codemash.io/connections/api 
Copy ApiKey and address to your configuration and that's it, you can start using CodeMash


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

            Mailer.SendMail(person.Email, "CodeMash - it just works", $"Hi Brad, yours id is - {person.Id}", "support@codemash.io");

            var bradPittByName = Db.FindOne<Person>(x => x.Name == "Brad Pitt");
            var bradPittById = Db.FindOneById<Person>(person.Id.ToString());
            var bradPittByMongoDbNotation = Db.FindOne<Person>(Builders<Person>.Filter.Eq(x => x.Name, "Brad Pitt"));

            var allActorsCount = Db.Count<Person>(x => true);

            Console.WriteLine($"Actors on database: {allActorsCount}");
            Console.ReadLine();
        }
    }
}


-----------------------------------------------------------------------------------------------------------------------------------

[Get Code]

CodeMash.Net code you can find at : https://github.com/codemash-io/CodeMash.Net it's open source

-----------------------------------------------------------------------------------------------------------------------------------
[Documentation]

Full, comprehensive documentation you can find at https://codemash.io/documentation/api/net

-----------------------------------------------------------------------------------------------------------------------------------
[RoadMap]


CodeMash.Net 1.0.1.0
----------------------------

ApiKey authentication 
CRUD support with MongoDB driver
SMTP send mails.
----------------------------


-----------------------------------------------------------------------------------------------------------------------------------
- Dependencies

MongoDB.Driver" version="2.3.0" 
MongoDB.Driver.Core" version="2.3.0"
Newtonsoft.Json" version="9.0.1"
ServiceStack.Client" version="4.5.4"
ServiceStack.Interfaces" version="4.5.4"
ServiceStack.Text" version="4.5.4"