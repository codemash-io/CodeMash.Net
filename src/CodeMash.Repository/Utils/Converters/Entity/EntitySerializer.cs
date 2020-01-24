using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeMash.Models;
using Isidos.CodeMash.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace CodeMash.Repository
{
    public class EntitySerializer
    {
        private readonly PropertyInfo[] _properties;
        
        public EntitySerializer(PropertyInfo[] properties)
        {
            _properties = properties;
        }

        public void FormEntityForSerialize(JObject entity)
        {
            foreach (var property in _properties)
            {
                var propName = property.Name;
                if (property.PropertyType == typeof(DateTime))
                {
                    if (entity.ContainsKey(propName) && entity[propName].Type == JTokenType.Date)
                    {
                        entity[propName].Replace(new JValue(DateTimeHelpers.DateTimeToUnixTimestamp(entity[propName].ToObject<DateTime>())));
                    }
                }
                // Nested
                else if (property.PropertyType.GetInterfaces().Contains(typeof(ICollection)))
                {
                    var listItemType = property.PropertyType.GetGenericArguments().FirstOrDefault();
                    var propertiesOfNestedItem = listItemType?.GetProperties();

                    if (propertiesOfNestedItem == null) continue;
                    
                    // Nested array props
                    if (entity.ContainsKey(propName) && entity[propName].Type == JTokenType.Array)
                    {
                        var entityNestedArray = (JArray) entity[propName];

                        foreach (var entityNestedItem in entityNestedArray)
                        {
                            if (entityNestedItem.Type != JTokenType.Object) continue;
                            var entityNestedObject = (JObject) entityNestedItem;
                            
                            foreach (var nestedProp in propertiesOfNestedItem)
                            {
                                var nestedPropName = nestedProp.Name;
                                
                                // Nested date time
                                if (nestedProp.PropertyType == typeof(DateTime))
                                {
                                    if (entityNestedObject.ContainsKey(nestedPropName) && entityNestedObject[nestedPropName].Type == JTokenType.Date)
                                    {
                                        entityNestedObject[nestedPropName].Replace(new JValue(DateTimeHelpers.DateTimeToUnixTimestamp(entityNestedObject[nestedPropName].ToObject<DateTime>())));
                                    }
                                }
                                
                                RenameProperty(entityNestedObject, nestedProp);
                                
                            }
                        }
                    }
                }

                RenameProperty(entity, property);
            }
        }

        private void RenameProperty(JObject entity, PropertyInfo prop)
        {
            var propName = prop.Name;
            if (!entity.ContainsKey(propName)) return;
            
            var propAttr = prop.GetCustomAttribute<FieldAttribute>();
            var lowerNestedPropName = propName.ToLower();
                                    
            if (!string.IsNullOrEmpty(propAttr?.ElementName))
            {
                if (propAttr.ElementName == propName) return;
                
                entity.Add(new JProperty(propAttr.ElementName, entity[propName]));
                entity.Remove(propName);
            }
            else
            {
                if (propName == lowerNestedPropName) return;
                
                entity.Add(new JProperty(lowerNestedPropName, entity[propName]));
                entity.Remove(propName);
            }
        }
        
    }
}