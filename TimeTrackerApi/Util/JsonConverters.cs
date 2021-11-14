//using Newtonsoft.Json;
//using System.Globalization;

using System.Text.Json;
using System.Text.Json.Serialization;

namespace TimeTrackerApi.Util;

//public class DateOnlyJsonConverter : JsonConverter<DateOnly>
//{
//    private const string DateFormat = "yyyy-MM-dd";

//    public override DateOnly ReadJson(JsonReader reader, Type objectType, DateOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
//    {
//        return DateOnly.ParseExact((string)reader.Value, DateFormat, CultureInfo.InvariantCulture);
//    }

//    public override void WriteJson(JsonWriter writer, DateOnly value, JsonSerializer serializer)
//    {
//        writer.WriteValue(value.ToString(DateFormat, CultureInfo.InvariantCulture));
//    }
//}

//public class TimeOnlyJsonConverter : JsonConverter<TimeOnly>
//{
//    private const string TimeFormat = "HH:mm:ss.FFFFFFF";

//    public override TimeOnly ReadJson(JsonReader reader, Type objectType, TimeOnly existingValue, bool hasExistingValue, JsonSerializer serializer)
//    {
//        return TimeOnly.ParseExact((string)reader.Value, TimeFormat, CultureInfo.InvariantCulture);
//    }

//    public override void WriteJson(JsonWriter writer, TimeOnly value, JsonSerializer serializer)
//    {
//        writer.WriteValue(value.ToString(TimeFormat, CultureInfo.InvariantCulture));
//    }
//}

//public class TimeOnylyJsonConverter : JsonConverter<TimeOnly>
//{
//    public override DateTimeOffset Read(
//        ref Utf8JsonReader reader,
//        Type typeToConvert,
//        JsonSerializerOptions options) =>
//            DateTimeOffset.ParseExact(reader.GetString(),
//                "MM/dd/yyyy", CultureInfo.InvariantCulture);

//    public override void Write(
//        Utf8JsonWriter writer,
//        DateTimeOffset dateTimeValue,
//        JsonSerializerOptions options) =>
//            writer.WriteStringValue(dateTimeValue.ToString(
//                "MM/dd/yyyy", CultureInfo.InvariantCulture));
//}
