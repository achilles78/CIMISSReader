namespace QCimiss
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;

    public class JsonHelper
    {
        public static T DeserializeAnonymousType<T>(string json, T anonymousTypeObject) => 
            JsonConvert.DeserializeAnonymousType<T>(json, anonymousTypeObject);

        public static List<T> DeserializeJsonToList<T>(string json) where T: class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader reader = new StringReader(json);
            return (serializer.Deserialize(new JsonTextReader(reader), typeof(List<T>)) as List<T>);
        }

        public static T DeserializeJsonToObject<T>(string json) where T: class
        {
            JsonSerializer serializer = new JsonSerializer();
            StringReader reader = new StringReader(json);
            return (serializer.Deserialize(new JsonTextReader(reader), typeof(T)) as T);
        }

        public static string SerializeObject(object o) => 
            JsonConvert.SerializeObject(o);
    }
}

