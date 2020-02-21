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
    public class AggregateDeserializer
    {
        public void FormAggregateForDeserialize(JObject entity, PropertyInfo[] _properties)
        {
            foreach (var property in _properties)
            {
                var propAttrs = property.GetCustomAttribute<FieldAttribute>();
                var jsonPropAttrIsSet = property.GetCustomAttribute<JsonPropertyAttribute>() != null; // Don't rename if this is set
                var propNameInitial = propAttrs?.ElementName ?? property.Name.ToLower();
                
                // Non nested date time
                if (property.PropertyType == typeof(DateTime))
                {
                    if (entity.ContainsKey(propNameInitial) && entity[propNameInitial].Type == JTokenType.Integer)
                    {
                        entity[propNameInitial].Replace(new JValue(DateTimeHelpers.DateTimeFromUnixTimestamp((long)entity[propNameInitial])));
                    }
                }
                // Reference when not referencing, set ID even if using object
                else if (entity[propNameInitial] is JObject entityObject)
                {
                    var properties = property.PropertyType.GetProperties();
                    FormAggregateForDeserialize(entityObject, properties);
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

                        for (var i = 0; i < entityNestedArray.Count; i += 1)
                        {
                            if (entityNestedArray[i] is JObject entityNestedItemObject)
                            {
                                FormAggregateForDeserialize(entityNestedItemObject, propertiesOfNestedItem);
                            }
                            else if (listItemType == typeof(DateTime))
                            {
                                if (entityNestedArray[i].Type == JTokenType.Integer)
                                {
                                    entityNestedArray[i].Replace(new JValue(DateTimeHelpers.DateTimeFromUnixTimestamp((long)entityNestedArray[i])));
                                }
                            }
                        }
                    }
                }

                RenameProperty(entity, property, propNameInitial, jsonPropAttrIsSet);
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