using System;
using CodeMash.Interfaces;
using CodeMash.Repository;
using MongoDB.Bson.Serialization.Attributes;

namespace TestApp
{
    class Program
    {
        [CollectionName("recipes")]
        public class Recipe : Entity
        {
            [BsonElement("title")]
            public string Name { get; set; }
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            IRepository<Recipe> recipesRepository = CodeMashRepositoryFactory.Create<Recipe>();

            var recipes = recipesRepository.Find(x => true);

            Console.ReadLine();

        }
    }
}