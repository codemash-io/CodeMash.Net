using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using CodeMash.Models;
using Isidos.CodeMash.Utils;
using Newtonsoft.Json.Linq;

namespace CodeMash.Repository
{
    public class EntitySerializer
    {
        public void FormEntityForSerialize(JObject entity, PropertyInfo[] _properties)
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
                // Reference when not referencing, set ID even if using object
                else if (property.PropertyType.GetInterfaces().Contains(typeof(IEntity)))
                {
                    if (entity.ContainsKey(propName) && entity[propName].Type == JTokenType.Object)
                    {
                        entity[propName].Replace(new JValue(entity[propName]["_id"]?.ToString() ?? entity[propName]["id"]?.ToString()));
                    }
                }
                // If some random object
                else if (entity.ContainsKey(propName) && entity[propName].Type == JTokenType.Object)
                {
                    var properties = property.PropertyType.GetProperties();
                    FormEntityForSerialize((JObject) entity[propName], properties);
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

                        if (listItemType.GetInterfaces().Contains(typeof(IEntity)) && entityNestedArray.Count > 0)
                        {
                            if (entityNestedArray[0].Type == JTokenType.Object)
                            {
                                var arrayLength = entityNestedArray.Count;
                                for (var i = 0; i < arrayLength; i++)
                                {
                                    entityNestedArray[i].Replace(new JValue(entityNestedArray[i]["_id"]?.ToString() ?? entityNestedArray[i]["id"]?.ToString()));
                                }
                            }
                        }
                        else
                        {
                            foreach (var entityNestedItem in entityNestedArray)
                            {
                                if (entityNestedItem.Type != JTokenType.Object) continue;
                                var entityNestedObject = (JObject) entityNestedItem;
                            
                                foreach (var nestedProp in propertiesOfNestedItem)
                                {
                                    var nestedPropName = nestedProp.Name;
                                
                                    // If this property is a single reference
                                    if (nestedProp.PropertyType.GetInterfaces().Contains(typeof(IEntity)))
                                    {
                                        // When not referencing
                                        if (entityNestedObject.ContainsKey(nestedPropName) && entityNestedObject[nestedPropName].Type == JTokenType.Object)
                                        {
                                            entityNestedObject[nestedPropName].Replace(new JValue(entityNestedObject[nestedPropName]["_id"]?.ToString() ?? entityNestedObject[nestedPropName]["id"]?.ToString()));
                                        }
                                        
                                        RenameProperty(entityNestedObject, nestedProp);
                                        continue;
                                    }
                                    // If this property is a multi reference
                                    if (nestedProp.PropertyType.GetInterfaces().Contains(typeof(ICollection)))
                                    {
                                        var multiRefListItemType = nestedProp.PropertyType.GetGenericArguments().FirstOrDefault();

                                        if (multiRefListItemType != null && multiRefListItemType.GetInterfaces().Contains(typeof(IEntity)) && 
                                            entityNestedObject.ContainsKey(nestedPropName) && entityNestedObject[nestedPropName].Type == JTokenType.Array)
                                        {
                                            var multiRefEntityNestedArray = (JArray) entityNestedObject[nestedPropName];
                                            if (multiRefEntityNestedArray.Count > 0)
                                            {
                                                if (multiRefEntityNestedArray[0].Type == JTokenType.Object)
                                                {
                                                    var arrayLength = multiRefEntityNestedArray.Count;
                                                    for (var i = 0; i < arrayLength; i++)
                                                    {
                                                        multiRefEntityNestedArray[i].Replace(new JValue(multiRefEntityNestedArray[i]["_id"].ToString() ?? multiRefEntityNestedArray[i]["id"].ToString()));
                                                    }
                                                }
                                            }
                                            
                                            RenameProperty(entityNestedObject, nestedProp);
                                            continue;
                                        }
                                    }
                                    
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