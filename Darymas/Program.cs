using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeMash.Net;
using CodeMash.Net.DataContracts;



namespace Darymas
{
    class Program
    {
        class Project : Entity
        {
            public string Name { get; set; }
        }

        static void Main(string[] args)
        {
            try
            {

                var project = new Project { Name = "My First Project" };
                var insertedProject = DB.InsertOne("Projects", project);
                    Console.WriteLine($"Project was inserted : {project.Id}");

            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine($"Project was not inserted !!! ");
            }
            Console.ReadLine();
        }
    }
}
