[Get Started]
-----------------------------------------------------------------------------------------------------------------------------------
add codemash configuration to either web.config or app.config file as follows :


<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <configSections>
    <section name="CodeMash" type="CodeMash.CodeMashConfigurationSection, CodeMash" requirePermission="false" />
  </configSections>
  <CodeMash>
    <client name="Sdk" apiKey="HP7qoWW**************LctB7IkU" address="http://api.codemash.io/1.0/" />
  </CodeMash>  
</configuration>

if you don't have apiKey please create an account on http://cloud.codemash.io and got to http://cloud.codemash.io/connections/api 
Copy ApiKey and address to your configuration and that's it, you can start using CodeMash


using System;
using CodeMash;
using CodeMash.Data;
using CodeMash.Interfaces;
using CodeMash.Notifications;
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

            var personsRepository = CodemashRepositoryFactory.Create<Person>();
            var notificationsService = CodemashNotificationFactory.Create<Email>();

            personsRepository.InsertOne(person);

            notificationsService.Send(
                person.Email, 
                "CodeMash - it just works", 
                $"Hi Brad, yours id is - {person.Id}", 
                "support@codemash.io");

            // simple way using lambda expressions
            var bradPittByName = personsRepository.FindOne(x => x.Name == "Brad Pitt");
            var bradPittById = personsRepository.FindOneById(person.Id);

            // mongo db way
            var filter = Builders<Person>.Filter.Eq(x => x.Name, "Brad Pitt");
            var bradPittByMongoDbNotation = personsRepository.FindOne(filter);

            var allActorsCount = personsRepository.Count(x => true);

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
