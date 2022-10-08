namespace Sendgrid.Webhooks.Converters;

public class BooleanConverter : System.Text.Json.Serialization.JsonConverter<bool>
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Boolean);
    }

    public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
	    if (reader.TokenType == JsonTokenType.String)
	    {
		    return !reader.GetString().Equals("0");
	    }
        return !reader.GetInt32().Equals(0);
    }

    public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options)
    {
	    writer.WriteNumberValue(value ? 1 : 0);
    }
}
