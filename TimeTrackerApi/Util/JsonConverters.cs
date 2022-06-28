namespace TimeTrackerApi.Util;

using System.Text.Json;
using System.Text.Json.Serialization;

public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string DateFormat = "dd.MM.yyyy";

    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var dateString = reader.GetString() ?? string.Empty;

        return DateOnly.ParseExact(dateString, DateFormat);
    }

    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(DateFormat));
    }
}

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
