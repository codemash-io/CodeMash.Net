using System;
using CodeMash.Interfaces;
using CodeMash.Repository;
using MongoDB.Bson.Serialization.Attributes;

namespace TestApp
{
    class Program
    {
        [CollectionName("recipes")]
        public class Recipe : Entity, IEntity
        {
            [BsonElement("title")]
            public string Name { get; set; }
        }
        
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            CodeMash.Repository.IRepository<Recipe> recipesRepository = CodeMashRepositoryFactory.Create<Recipe>();

            var recipes = recipesRepository.Find<Recipe>(x => true);

            Console.ReadLine();

        }
    }
}