using System;
using CodeMash.Interfaces.Database.Repository;
using CodeMash.Models;

namespace CodeMash.Repository
{
    public partial class CodeMashRepository<T> where T : IEntity
    {
        private static string GetCollectionNameFromInterface()
        {
            // Check to see if the object (inherited from Entity) has a CollectionName attribute
            var att = Attribute.GetCustomAttribute(typeof(T), typeof(CollectionAttribute));
            var collectionName = att != null ? ((CollectionAttribute) att).Name : typeof(T).Name;

            return collectionName;
        }

        private static string GetCollectionNameFromType(Type entityType)
        {
            var collectionName = string.Empty;

            // Check to see if the object (inherited from Entity) has a CollectionName attribute
            var customAttribute = Attribute.GetCustomAttribute(entityType, typeof(CollectionAttribute));
            if (customAttribute != null)
            {
                // It does! Return the value specified by the CollectionName attribute
                collectionName = ((CollectionAttribute) customAttribute).Name;
            }
            else
            {
                if (typeof(Entity).IsAssignableFrom(entityType))
                {
                    while (entityType != null && entityType.BaseType != typeof(Entity))
                    {
                        entityType = entityType.BaseType;
                    }
                }

                if (entityType != null) collectionName = entityType.Name;
            }

            return collectionName;
        }

        private static string GetCollectionName()
        {
            var collectionName = typeof(T).BaseType == typeof(object)
                ? GetCollectionNameFromInterface()
                : GetCollectionNameFromType(typeof(T));

            if (string.IsNullOrEmpty(collectionName))
            {
                throw new ArgumentException("Collection name cannot be empty for this entity");
            }

            return collectionName;
        }
    }
}