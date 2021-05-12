using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CodeMash.Repository
{
    public class AggregateConverter<T> : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, 
            JsonSerializer serializer)
        {
            var entityToken = JToken.FromObject(value);
            entityToken.WriteTo(writer);
        }

        public override object ReadJson(JsonReader reader, Type objectType, 
            object existingValue, JsonSerializer serializer)
        {
            var entityToken = JToken.Load(reader);
            if (entityToken.Type == JTokenType.Array)
            {
                var listItemType = typeof(T).GetGenericArguments().FirstOrDefault();
                var properties = listItemType?.GetProperties();
                
                var entityDeserializer = new AggregateDeserializer();
                foreach (var entityArrayItem in entityToken)
                {
                    var entity = (JObject) entityArrayItem;
                    entityDeserializer.FormAggregateForDeserialize(entity, properties);
                }
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