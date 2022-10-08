namespace Sendgrid.Webhooks.Converters;

public abstract class GenericListCreationJsonConverter<T> : JsonConverter<List<T>> where T : class
{

	public override bool CanConvert(Type objectType)
	{
		return true;
	}
	
	public override List<T> Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions options)
	{
		if (reader.TokenType == JsonTokenType.StartArray)
		{
			var list = new List<T>();
			var jsonObject = JsonDocument.ParseValue(ref reader);
			list = JsonSerializer.Deserialize<List<T>>(jsonObject);
			return list; 
		}
		
		T t = JsonSerializer.Deserialize<T>(JsonDocument.ParseValue(ref reader));
		return new List<T>(new[] {t});
	}

	public override void Write(Utf8JsonWriter writer, List<T> value, JsonSerializerOptions options)
	{
		throw new NotImplementedException();
	}
}