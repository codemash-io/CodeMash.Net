using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;

namespace CodeMash.Repository
{
    public static class JsonConverterHelper
    {
        public static T DeserializeWithLowercase<T>(string json)
        {
            return string.IsNullOrEmpty(json) ? default(T) : JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings 
            { 
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
            });
        }
        
        public static string SerializeWithLowercase<T>(T serializableObject)
        {
            return serializableObject == null ? null : JsonConvert.SerializeObject(serializableObject, new JsonSerializerSettings 
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });
        }
        
        
        public static T DeserializeWithLowercase<T>(string json, string cultureCode)
        {
            if (string.IsNullOrEmpty(json))
            {
                return default(T);
            }
            
            if (string.IsNullOrEmpty(cultureCode))
            {
                return JsonConvert.DeserializeObject<T>(json, new JsonSerializerSettings 
                { 
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                });
            }
            else
            {
                return JsonConvert.DeserializeObject<T>(json, new EntityConverter<T>(cultureCode));
            }
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
                throw new NotImplementedException();
            }

            public override object ReadJson(JsonReader reader, Type objectType, 
                object existingValue, JsonSerializer serializer)
            {
                var entity = JObject.Load(reader);

                var properties = typeof(T).GetProperties();
                foreach (var property in properties)
                {
                    if (property.PropertyType == typeof(Dictionary<string, string>))
                    {
                        var propName = property.Name.ToLowerCaseFirstLetter();
                        if (entity.ContainsKey(propName) && entity[propName].Type == JTokenType.String)
                        {
                            entity[propName].Replace(new JObject(new JProperty(CultureCode, entity[propName].ToString())));
                        }
                    }
                    else if (property.PropertyType.GetInterfaces().Contains(typeof(ICollection)))
                    {
                        var listItemType = property.PropertyType.GetGenericArguments().FirstOrDefault();
                        var propertiesOfNestedItem = listItemType?.GetProperties();

                        if (propertiesOfNestedItem == null) continue;
                        
                        var propName = property.Name.ToLowerCaseFirstLetter();
                        if (entity.ContainsKey(propName) && entity[propName].Type == JTokenType.Array)
                        {
                            var entityNestedArray = (JArray) entity[propName];

                            foreach (var entityNestedItem in entityNestedArray)
                            {
                                if (entityNestedItem.Type != JTokenType.Object) continue;
                                var entityNestedObject = (JObject) entityNestedItem;
                                
                                foreach (var nestedProp in propertiesOfNestedItem)
                                {
                                    if (nestedProp.PropertyType == typeof(Dictionary<string, string>))
                                    {
                                        var nestedPropName = nestedProp.Name.ToLowerCaseFirstLetter();
                                        if (entityNestedObject.ContainsKey(nestedPropName) && entityNestedObject[nestedPropName].Type == JTokenType.String)
                                        {
                                            entityNestedObject[nestedPropName].Replace(new JObject(new JProperty(CultureCode, entityNestedObject[nestedPropName].ToString())));
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                
                return DeserializeWithLowercase<T>(entity.ToString(), null);
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