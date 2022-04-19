using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace TableBookingSystem.Web.Util.Converters
{
    public class TimeSpanConvertor : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            Regex hhmmssRegex = new Regex(@"(?:[01]\d|2[0-3]):(?:[0-5]\d):(?:[0-5]\d)");
            Regex hhmmRegex = new Regex(@"^([0-9]|0[0-9]|1[0-9]|2[0-3]):[0-5][0-9]");

            if (reader.TokenType == JsonTokenType.String && (hhmmssRegex.IsMatch(reader.GetString()) || hhmmRegex.IsMatch(reader.GetString())))
            {
                TimeSpan parsedTimeSpan = TimeSpan.Parse(reader.GetString());
                return parsedTimeSpan;
            }
            else
            {
                throw new Exception("Invalid JSON Value " + reader.TokenStartIndex);
            }
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            string newValue = value.ToString();
            writer.WriteStringValue(newValue);
        }
    }
}
