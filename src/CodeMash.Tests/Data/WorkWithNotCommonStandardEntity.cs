using System;
using System.Threading.Tasks;
using CodeMash.Data;
using CodeMash.Interfaces.Data;
using CodeMash.Tests.Data;
using CodeMash.Tests.Data.Domain;
using MongoDB.Driver;
using NUnit.Framework;

namespace CodeMash.Tests
{
    [TestFixture]
    public partial class WorkWithNotCommonStandardEntity : TestBase
    {
        private NotCommonMongoEntity Entity { get; set; }
        public IRepository<NotCommonMongoEntity> UserAuthRepository { get; set; }
        

        protected override void Initialize()
        {
            base.Initialize();

            UserAuthRepository = Resolve<IRepository<NotCommonMongoEntity>>();

            // Arrange
            Entity = new NotCommonMongoEntity
            {
                Id = 11,
                Name = "Just Test"
            };
            
        }
        
        [Test]
        [Category("Data")]
        public void Can_insert_entity()
        {
            // Act
            UserAuthRepository.InsertOne(Entity);

            // Assert
            Entity.ShouldNotBeNull();
            Entity.Id.ShouldNotBeNull();
        }

        [Test]
        [Category("Data")]
        public async Task Can_insert_entity_async()
        {
            // Act
            await UserAuthRepository.InsertOneAsync(Entity);

            // Assert
            Entity.ShouldNotBeNull();
            Entity.Id.ShouldNotBeNull();
        }


        [Test]
        [Category("Data")]
        public void Can_insert_and_get_entity()
        {
            // Act
            UserAuthRepository.InsertOne(Entity);

            var entity = UserAuthRepository.FindOne(x => x.Id == 11);

            // Assert
            entity.ShouldNotBeNull();
            entity.Name.ShouldEqual("Just Test");
        }

        [Test]
        [Category("Data")]
        public async Task Can_insert_and_get_entity_async()
        {
            // Act
            await UserAuthRepository.InsertOneAsync(Entity);

            var entity = await UserAuthRepository.FindOneAsync(x => x.Id == 11);

            // Assert
            entity.ShouldNotBeNull();
            entity.Name.ShouldEqual("Just Test");
        }


        [Test]
        [Category("Data")]
        public async Task Throw_an_exception_when_use_findOneById_with_id_which_is_not_of_int_type()
        {
            // Act
            await UserAuthRepository.InsertOneAsync(Entity);

            var exception = typeof(FormatException).ShouldBeThrownByAsync(async () => await UserAuthRepository.FindOneByIdAsync("11"));
            exception.Message.ShouldEqual("'11' is not a valid 24 digit hex string.");
        }
        
        protected override void Dispose()
        {
            base.Dispose();

            // TODO : drop collections, instead of removing one by one items. 

            UserAuthRepository.DeleteMany(x => true);
        }
    }
}