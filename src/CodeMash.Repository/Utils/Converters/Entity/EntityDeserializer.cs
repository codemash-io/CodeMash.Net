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
        private readonly PropertyInfo[] _properties;

        private readonly string _cultureCode;
        
        public EntityDeserializer(PropertyInfo[] properties, string cultureCode = null)
        {
            _properties = properties;
            _cultureCode = cultureCode;
        }

        public void FormEntityForDeserialize(JObject entity)
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
                                var nestedPropAttrs = nestedProp.GetCustomAttribute<FieldAttribute>();
                                var nestedPropNameInitial = nestedPropAttrs?.ElementName ?? nestedProp.Name.ToLower();
                                
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
                                
                                RenameProperty(entityNestedObject, nestedProp, nestedPropNameInitial, jsonPropAttrIsSet);
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