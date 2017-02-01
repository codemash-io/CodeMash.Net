using System;
using CodeMash.Data;
using CodeMash.Interfaces.Data;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using NUnit.Framework;

namespace CodeMash.Tests
{
    [TestFixture]
    public class TestBase
    {
        //private ServiceStackHost appHost;
        public static IUnityContainer Container;


        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterInstance("MyConnectionString", new MongoUrl("mongodb://localhost"), new ContainerControlledLifetimeManager());
            container.RegisterInstance("Collection", "CollectionName", new ContainerControlledLifetimeManager());

            container.RegisterType(typeof(IMongoRepository<>), typeof(MongoRepository<>), new InjectionConstructor(new ResolvedParameter<MongoUrl>("MyConnectionString")));//, new ResolvedParameter<string>("Collection")));
            return container;
        }

        protected virtual void Initialize()
        {
        }

        protected virtual void Dispose()
        {
            // Drop collections
        }

        [SetUp]
        public void SetUp()
        {
            Container = BuildUnityContainer();
            Initialize();
        }

        [TearDown]
        public void TearDown()
        {
            Container.Dispose();
            Dispose();
            
        }

        public static T Resolve<T>()
        {

            T resolved;

            //if (Container.IsRegistered<T>()) 
            try
            {
                resolved = Container.Resolve<T>();
            }
            catch (Exception e)
            {
                resolved = default(T);
            }
            return resolved;
        }



    }
}