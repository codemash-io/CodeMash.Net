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
                var listItemType = typeof(T).GetGenericArguments().FirstOrDefault();
                var properties = listItemType?.GetProperties();
                
                var entitySerializer = new EntitySerializer(properties);
                foreach (var entityArrayItem in entityToken)
                {
                    var entity = (JObject) entityArrayItem;
                    entitySerializer.FormEntityForSerialize(entity);
                }
                
                entityToken.WriteTo(writer);
            }
            else if (entityToken.Type == JTokenType.Object)
            {
                var properties = typeof(T).GetProperties();
                var entity = (JObject) entityToken;

                var entitySerializer = new EntitySerializer(properties);
                entitySerializer.FormEntityForSerialize(entity);
                
                entity.WriteTo(writer);
            }
            else
            {
                entityToken.WriteTo(writer);
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
                
                var entityDeserializer = new EntityDeserializer(/*properties,*/ CultureCode);
                foreach (var entityArrayItem in entityToken)
                {
                    var entity = (JObject) entityArrayItem;
                    entityDeserializer.FormEntityForDeserialize(entity, properties);
                }
            }
            else if (entityToken.Type == JTokenType.Object)
            {
                var properties = typeof(T).GetProperties();
                var entity = (JObject) entityToken;
                var entityDeserializer = new EntityDeserializer(/*properties,*/ CultureCode);

                entityDeserializer.FormEntityForDeserialize(entity, properties);
            }
            
            return JsonConvert.DeserializeObject<T>(entityToken.ToString());
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