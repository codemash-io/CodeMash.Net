using System;
using CodeMash.Client;
using CodeMash.Models;
using CodeMash.Repository;

namespace CodeMash.App
{
    [CollectionName("collection-1")]
    public class Collection1 : Entity
    {
        [FieldName("name")]
        public string Name { get; set; }
        
        public DateTime Lunchtime2 { get; set; }
    }
    
    class Program
    {
        static void Main(string[] args)
        {
            var client = new CodeMashClient("H_948rchw9JR_1vY1RlbiddPFd1kkqyb", Guid.Parse("ffa8f642-3c6b-4c69-9cba-433afb382036"));
            var repo = new CodeMashRepository<Collection1>(client);

            repo.InsertOne(new Collection1 {Name = "aa", Lunchtime2 = DateTime.UtcNow });
        }
    }
}