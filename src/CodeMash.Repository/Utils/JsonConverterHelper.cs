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
        public static string SerializeEntity<T>(T serializableObject)
        {
            return serializableObject == null ? null : JsonConvert.SerializeObject(serializableObject, new EntityConverter<T>(null));
        }
        
        public static T DeserializeEntity<T>(string json, string cultureCode)
        {
            if (string.IsNullOrEmpty(json))
            {
                return default(T);
            }
            
            return JsonConvert.DeserializeObject<T>(json, new EntityConverter<T>(cultureCode));
        }
    }
}