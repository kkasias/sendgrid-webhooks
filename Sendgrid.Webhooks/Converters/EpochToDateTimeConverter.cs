namespace Sendgrid.Webhooks.Converters;

public class EpochToDateTimeConverter : System.Text.Json.Serialization.JsonConverter<DateTime>
{
	private static readonly DateTime EpochDate = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

	public override bool CanConvert(Type objectType)
	{
		return objectType == typeof(DateTime);
	}

	public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
	{
		var timestamp = reader.GetDouble();
		return EpochDate.AddSeconds(timestamp);
	}

	public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
	{
		if (value == default(DateTime))
			return;
            
		var date = (DateTime) value;
		var diff = date - EpochDate;

		var secondsSinceEpoch = (int) diff.TotalSeconds;
		writer.WriteNumberValue(secondsSinceEpoch);
	}
}