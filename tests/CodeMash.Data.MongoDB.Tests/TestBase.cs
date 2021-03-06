using System;
using System.Configuration;
using System.Diagnostics;
using CodeMash.Interfaces.Data;
using CodeMash.Interfaces.Notifications;
using CodeMash.Notifications;
using Microsoft.Practices.Unity;
using MongoDB.Driver;
using NUnit.Framework;

namespace CodeMash.Data.MongoDB.Tests
{
    [TestFixture]
    public class TestBase
    {
        //private ServiceStackHost appHost;
        public static IUnityContainer Container;


        private static IUnityContainer BuildUnityContainer()
        {
            var connectionString = ConfigurationManager.AppSettings["MyConnectionString"];

            Debug.WriteLine($"Connection string of database : {connectionString}");

            var container = new UnityContainer();

            
            container.RegisterInstance("Collection", "CollectionName", new ContainerControlledLifetimeManager());

            if (!string.IsNullOrEmpty(connectionString))
            {
                container.RegisterInstance("MyConnectionString", new MongoUrl(connectionString),
                   new ContainerControlledLifetimeManager());

                container.RegisterType(typeof(IRepository<>), typeof(Repository<>),
                        new InjectionConstructor(new ResolvedParameter<MongoUrl>("MyConnectionString")));
            }
            else
            {
                container.RegisterType(typeof(IRepository<>), typeof(Repository<>), new InjectionConstructor());
            }
            container.RegisterType(typeof(IEmailService), typeof(EmailService));
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
            Dispose();
            Container.Dispose();
            
            
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