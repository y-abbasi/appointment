using System;
using Newtonsoft.Json;

namespace Suzianna.Rest.Serialization
{
    internal class JsonSerializer : ISerializer
    {
        public string Serialize(object objectToSerialize)
        {
            var st = new JsonSerializerSettings();
            st.Converters.Add(new DateOnlyConverter());
            return JsonConvert.SerializeObject(objectToSerialize, st);
        }
    }

    public class DateOnlyConverter : JsonConverter<DateOnly>
    {
        public override void WriteJson(JsonWriter writer, DateOnly value, Newtonsoft.Json.JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString("O"));
        }

        public override DateOnly ReadJson(JsonReader reader, Type objectType, DateOnly existingValue,
            bool hasExistingValue,
            Newtonsoft.Json.JsonSerializer serializer)
        {
            return DateOnly.Parse(reader.ReadAsString());
        }
    }
}