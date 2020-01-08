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
    public static class JsonConverterHelper
    {
        public static string SerializeWithLowercase<T>(T serializableObject)
        {
            return serializableObject == null ? null : JsonConvert.SerializeObject(serializableObject, new EntityConverter<T>(null));
        }
        
        public static T DeserializeWithLowercase<T>(string json, string cultureCode)
        {
            if (string.IsNullOrEmpty(json))
            {
                return default(T);
            }
            
            return JsonConvert.DeserializeObject<T>(json, new EntityConverter<T>(cultureCode));
        }
        
        public class EntityConverter<T> : JsonConverter
        {
            private string CultureCode { get; set; }

            public EntityConverter(string cultureCode)
            {
                CultureCode = cultureCode;
            }
            
            public override void WriteJson(JsonWriter writer, object value, 
                JsonSerializer serializer)
            {
                var entityToken = JToken.FromObject(value);
                if (entityToken.Type == JTokenType.Array)
                {
                    // var properties = typeof(T).GetProperties();
                    var listItemType = typeof(T).GetGenericArguments().FirstOrDefault();
                    var properties = listItemType?.GetProperties();
                    foreach (var entityArrayItem in entityToken)
                    {
                        var entity = (JObject) entityArrayItem;
                        FormEntityForSerialize(properties, entity);
                    }
                    
                    entityToken.WriteTo(writer);
                }
                else if (entityToken.Type == JTokenType.Object)
                {
                    var properties = typeof(T).GetProperties();
                    var entity = (JObject) entityToken;

                    FormEntityForSerialize(properties, entity);
                    
                    entity.WriteTo(writer);
                }
                else
                {
                    entityToken.WriteTo(writer);
                }
            }

            private void FormEntityForSerialize(PropertyInfo[] properties, JObject entity)
            {
                foreach (var property in properties)
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
                                            // entityNestedObject[nestedPropName].Replace(new JValue(DateTimeHelpers.DateTimeToUnixTimestamp(new DateTime(entityNestedObject[nestedPropName].ToString()))));
                                        }
                                    }

                                    if (entityNestedObject.ContainsKey(nestedPropName))
                                    {
                                        var nestedPropAttrs = nestedProp.GetCustomAttribute<FieldNameAttribute>();
                                        if (!string.IsNullOrEmpty(nestedPropAttrs?.ElementName) && nestedPropAttrs.ElementName != nestedPropName)
                                        {
                                            entityNestedObject.Add(new JProperty(nestedPropAttrs.ElementName ?? nestedPropName, entityNestedObject[nestedPropName]));
                                            entityNestedObject.Remove(nestedPropName);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    
                    if (entity.ContainsKey(propName))
                    {
                        var propAttrs = property.GetCustomAttribute<FieldNameAttribute>();
                        if (!string.IsNullOrEmpty(propAttrs?.ElementName) && propAttrs.ElementName != propName)
                        {
                            entity.Add(new JProperty(propAttrs.ElementName ?? propName, entity[propName]));
                            entity.Remove(propName);
                        }
                    }
                }
            }

            public override object ReadJson(JsonReader reader, Type objectType, 
                object existingValue, JsonSerializer serializer)
            {
                var entityToken = JToken.Load(reader);
                if (entityToken.Type == JTokenType.Array)
                {
                    var listItemType = typeof(T).GetGenericArguments().FirstOrDefault();
                    var properties = listItemType?.GetProperties();
                    foreach (var entityArrayItem in entityToken)
                    {
                        var entity = (JObject) entityArrayItem;
                        FormEntityForDeserialize(properties, entity);
                    }
                }
                else if (entityToken.Type == JTokenType.Object)
                {
                    var properties = typeof(T).GetProperties();
                    var entity = (JObject) entityToken;

                    FormEntityForDeserialize(properties, entity);
                }
                
                
                return JsonConvert.DeserializeObject<T>(entityToken.ToString(), new JsonSerializerSettings 
                { 
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                });
                
                //return DeserializeWithLowercase<T>(entity.ToString(), null);
            }

            private void FormEntityForDeserialize(PropertyInfo[] properties, JObject entity)
            {
                var cultureCodeSet = !string.IsNullOrEmpty(CultureCode);
                foreach (var property in properties)
                {
                    var propAttrs = property.GetCustomAttribute<FieldNameAttribute>();
                    var propNameInitial = propAttrs?.ElementName ?? property.Name;
                    
                    // Non nested translatable
                    if (property.PropertyType == typeof(Dictionary<string, string>) && cultureCodeSet)
                    {
                        if (entity.ContainsKey(propNameInitial) && entity[propNameInitial].Type == JTokenType.String)
                        {
                            entity[propNameInitial].Replace(new JObject(new JProperty(CultureCode, entity[propNameInitial].ToString())));
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

                            foreach (var entityNestedItem in entityNestedArray)
                            {
                                if (entityNestedItem.Type != JTokenType.Object) continue;
                                var entityNestedObject = (JObject) entityNestedItem;
                                
                                foreach (var nestedProp in propertiesOfNestedItem)
                                {
                                    var nestedPropAttrs = nestedProp.GetCustomAttribute<FieldNameAttribute>();
                                    var nestedPropNameInitial = nestedPropAttrs?.ElementName ?? nestedProp.Name;
                                    
                                    // Nested translatable
                                    if (nestedProp.PropertyType == typeof(Dictionary<string, string>) && cultureCodeSet)
                                    {
                                        if (entityNestedObject.ContainsKey(nestedPropNameInitial) && entityNestedObject[nestedPropNameInitial].Type == JTokenType.String)
                                        {
                                            entityNestedObject[nestedPropNameInitial].Replace(new JObject(new JProperty(CultureCode, entityNestedObject[nestedPropNameInitial].ToString())));
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
                                    
                                    if (entityNestedObject.ContainsKey(nestedPropNameInitial) && nestedPropNameInitial != nestedProp.Name)
                                    {
                                        entityNestedObject.Add(new JProperty(nestedProp.Name, entityNestedObject[nestedPropNameInitial]));
                                        entityNestedObject.Remove(nestedPropNameInitial);
                                    }
                                }
                            }
                        }
                    }
                    
                    if (entity.ContainsKey(propNameInitial) && property.Name != propNameInitial)
                    {
                        entity.Add(new JProperty(property.Name, entity[propNameInitial]));
                        entity.Remove(propNameInitial);
                    }
                }
            }

            public override bool CanRead
            {
                get { return true; }
            }

            public override bool CanConvert(Type objectType)
            {
                return true;
            }
        }
    }
}