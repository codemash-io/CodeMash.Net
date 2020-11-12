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
    public class EntityDeserializer
    {
        private readonly string _cultureCode;
        
        public EntityDeserializer(string cultureCode = null)
        {
            _cultureCode = cultureCode;
        }

        public void FormEntityForDeserialize(JObject entity, PropertyInfo[] _properties)
        {
            var cultureCodeSet = !string.IsNullOrEmpty(_cultureCode);
            foreach (var property in _properties)
            {
                var propAttrs = property.GetCustomAttribute<FieldAttribute>();
                var jsonPropAttrIsSet = property.GetCustomAttribute<JsonPropertyAttribute>() != null; // Don't rename if this is set
                var propNameInitial = propAttrs?.ElementName ?? property.Name.ToLower();
                
                // Non nested translatable
                if (property.PropertyType == typeof(Dictionary<string, string>) && cultureCodeSet)
                {
                    if (entity.ContainsKey(propNameInitial) && entity[propNameInitial].Type == JTokenType.String)
                    {
                        entity[propNameInitial].Replace(new JObject(new JProperty(_cultureCode, entity[propNameInitial].ToString())));
                    }
                }
                // Non nested date time
                else if (property.PropertyType == typeof(DateTime))
                {
                    if (entity.ContainsKey(propNameInitial) && entity[propNameInitial].Type == JTokenType.Integer)
                    {
                        entity[propNameInitial].Replace(new JValue(DateTimeHelpers.DateTimeFromUnixTimestamp((long)entity[propNameInitial])));
                    }
                }
                // For terms, if Meta is got as object but it should be string
                else if (property.PropertyType == typeof(string) && entity.ContainsKey(propNameInitial) && entity[propNameInitial].Type == JTokenType.Object)
                {
                    entity[propNameInitial].Replace(new JValue(entity[propNameInitial].ToString()));
                }
                // Reference when not referencing, set ID even if using object
                else if (property.PropertyType.GetInterfaces().Contains(typeof(IEntity)))
                {
                    if (entity.ContainsKey(propNameInitial) && entity[propNameInitial].Type == JTokenType.String)
                    {
                        entity[propNameInitial].Replace(new JObject(new JProperty("_id", entity[propNameInitial].ToString())));
                    }
                    else if (entity[propNameInitial] is JObject referencedNestedObject)
                    {
                        var properties = property.PropertyType.GetProperties();
                        ReplaceIdWithUnderscore(referencedNestedObject);
                        FormEntityForDeserialize(referencedNestedObject, properties);
                    }
                }
                // If some random object
                else if (entity.ContainsKey(propNameInitial) && entity[propNameInitial].Type == JTokenType.Object)
                {
                    var properties = property.PropertyType.GetProperties();
                    FormEntityForDeserialize((JObject) entity[propNameInitial], properties);
                }
                // Nested
                else if (property.PropertyType.GetInterfaces().Contains(typeof(ICollection)))
                {
                    var listItemType = property.PropertyType.GetGenericArguments().FirstOrDefault();
                    var propertiesOfNestedItem = listItemType?.GetProperties();

                    if (propertiesOfNestedItem == null) continue;
                    
                    // Nested array props
                    if (entity.ContainsKey(propNameInitial) && entity[propNameInitial].Type == JTokenType.Array)
                    {
                        var entityNestedArray = (JArray) entity[propNameInitial];

                        // If it's a reference, not nested
                        if (listItemType.GetInterfaces().Contains(typeof(IEntity)) && entityNestedArray.Count > 0)
                        {
                            // Multi reference when not referencing, set ID even if using object
                            if (entityNestedArray[0].Type == JTokenType.String)
                            {
                                var arrayLength = entityNestedArray.Count;
                                for (var i = 0; i < arrayLength; i++)
                                {
                                    entityNestedArray[i].Replace(new JObject(new JProperty("_id", entityNestedArray[i].ToString())));
                                }
                            }
                            // Multi when referencing
                            else if (entityNestedArray[0].Type == JTokenType.Object)
                            {
                                var arrayLength = entityNestedArray.Count;
                                for (var i = 0; i < arrayLength; i++)
                                {
                                    var entityNestedArrayObject = (JObject) entityNestedArray[i];
                                    ReplaceIdWithUnderscore(entityNestedArrayObject);
                                    FormEntityForDeserialize(entityNestedArrayObject, propertiesOfNestedItem);
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
                                    var nestedPropAttrs = nestedProp.GetCustomAttribute<FieldAttribute>();
                                    var nestedPropNameInitial = nestedPropAttrs?.ElementName ?? nestedProp.Name.ToLower();
                                    
                                    // If this property is a single reference
                                    if (nestedProp.PropertyType.GetInterfaces().Contains(typeof(IEntity)))
                                    {
                                        // When not referencing
                                        if (entityNestedObject.ContainsKey(nestedPropNameInitial) && entityNestedObject[nestedPropNameInitial].Type == JTokenType.String)
                                        {
                                            entityNestedObject[nestedPropNameInitial].Replace(new JObject(new JProperty("_id", entityNestedObject[nestedPropNameInitial].ToString())));
                                        }
                                        // When referencing
                                        else if (entityNestedObject.ContainsKey(nestedPropNameInitial) && entityNestedObject[nestedPropNameInitial] is JObject referencedNestedObject)
                                        {
                                            var properties = nestedProp.PropertyType.GetProperties();
                                            ReplaceIdWithUnderscore(referencedNestedObject);
                                            FormEntityForDeserialize(referencedNestedObject, properties);
                                        }
                                        
                                        RenameProperty(entityNestedObject, nestedProp, nestedPropNameInitial, jsonPropAttrIsSet);
                                        continue;
                                    }
                                    // If this property is a multi reference
                                    if (nestedProp.PropertyType.GetInterfaces().Contains(typeof(ICollection)))
                                    {
                                        var multiRefListItemType = nestedProp.PropertyType.GetGenericArguments().FirstOrDefault();

                                        if (multiRefListItemType != null && multiRefListItemType.GetInterfaces().Contains(typeof(IEntity)) && 
                                            entityNestedObject.ContainsKey(nestedPropNameInitial) && entityNestedObject[nestedPropNameInitial].Type == JTokenType.Array)
                                        {
                                            var multiRefPropertiesOfNestedItem = multiRefListItemType.GetProperties();
                                            var multiRefEntityNestedArray = (JArray) entityNestedObject[nestedPropNameInitial];
                                            
                                            if (multiRefEntityNestedArray.Count > 0)
                                            {
                                                // Multi reference when not referencing, set ID even if using object
                                                if (multiRefEntityNestedArray[0].Type == JTokenType.String)
                                                {
                                                    var arrayLength = multiRefEntityNestedArray.Count;
                                                    for (var i = 0; i < arrayLength; i++)
                                                    {
                                                        multiRefEntityNestedArray[i].Replace(new JObject(new JProperty("_id", multiRefEntityNestedArray[i].ToString())));
                                                    }
                                                }
                                                // Multi when referencing
                                                else if (multiRefEntityNestedArray[0].Type == JTokenType.Object)
                                                {
                                                    var arrayLength = multiRefEntityNestedArray.Count;
                                                    for (var i = 0; i < arrayLength; i++)
                                                    {
                                                        var multiRefEntityNestedArrayObject = (JObject) multiRefEntityNestedArray[i];
                                                        ReplaceIdWithUnderscore(multiRefEntityNestedArrayObject);
                                                        FormEntityForDeserialize(multiRefEntityNestedArrayObject, multiRefPropertiesOfNestedItem);
                                                    }
                                                }
                                            }
                                            
                                            RenameProperty(entityNestedObject, nestedProp, nestedPropNameInitial, jsonPropAttrIsSet);
                                            continue;
                                        }
                                    }
                                    
                                    // Nested translatable
                                    if (nestedProp.PropertyType == typeof(Dictionary<string, string>) && cultureCodeSet)
                                    {
                                        if (entityNestedObject.ContainsKey(nestedPropNameInitial) && entityNestedObject[nestedPropNameInitial].Type == JTokenType.String)
                                        {
                                            entityNestedObject[nestedPropNameInitial].Replace(new JObject(new JProperty(_cultureCode, entityNestedObject[nestedPropNameInitial].ToString())));
                                        }
                                    }
                                    // Nested date time
                                    else if (nestedProp.PropertyType == typeof(DateTime))
                                    {
                                        if (entityNestedObject.ContainsKey(nestedPropNameInitial) && entityNestedObject[nestedPropNameInitial].Type == JTokenType.Integer)
                                        {
                                            entityNestedObject[nestedPropNameInitial].Replace(new JValue(DateTimeHelpers.DateTimeFromUnixTimestamp((long)entityNestedObject[nestedPropNameInitial])));
                                        }
                                    }
                                    // For terms, if Meta is got as object but it should be string
                                    else if (property.PropertyType == typeof(string) && entityNestedObject.ContainsKey(nestedPropNameInitial) && entityNestedObject[nestedPropNameInitial].Type == JTokenType.Object)
                                    {
                                        entityNestedObject[nestedPropNameInitial].Replace(new JValue(entityNestedObject[nestedPropNameInitial].ToString()));
                                    }
                                    // If some random object
                                    else if (entityNestedObject.ContainsKey(nestedPropNameInitial) && entityNestedObject[nestedPropNameInitial].Type == JTokenType.Object)
                                    {
                                        var properties = nestedProp.PropertyType.GetProperties();
                                        FormEntityForDeserialize((JObject) entityNestedObject[nestedPropNameInitial], properties);
                                    }
                                    
                                    RenameProperty(entityNestedObject, nestedProp, nestedPropNameInitial, jsonPropAttrIsSet);
                                }
                            }
                        }
                        
                    }
                }

                RenameProperty(entity, property, propNameInitial, jsonPropAttrIsSet);
            }
        }

        private void ReplaceIdWithUnderscore(JObject entity)
        {                  
            if (entity.ContainsKey("id") && !entity.ContainsKey("_id"))
            {
                entity.Add(new JProperty("_id", entity["id"].ToString()));
                entity.Remove("id");
            }
        }
        
        private void RenameProperty(JObject entity, PropertyInfo prop, string propNameInitial, bool jsonPropAttrIsSet)
        {
            if (jsonPropAttrIsSet) return;
            
            var propName = prop.Name;
            if (!entity.ContainsKey(propNameInitial)) return;
            
            if (prop.Name != propNameInitial)
            {
                entity.Add(new JProperty(propName, entity[propNameInitial]));
                entity.Remove(propNameInitial);
            }
        }
        
    }
}