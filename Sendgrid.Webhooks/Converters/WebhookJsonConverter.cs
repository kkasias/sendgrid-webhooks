using System.Collections.Immutable;
using System.Linq;
using System.Text.Json.Nodes;
using Sendgrid.Webhooks.Events;

namespace Sendgrid.Webhooks.Converters;

public class WebhookJsonConverter : JsonConverter<object>
{
    //these will be filtered out from the UniqueParams
    private static readonly string[] KnownProperties = new string[] {"event", "email", "category", "timestamp", "ip", "useragent", "type", 
                                                           "reason", "status", "url", "url_offset", "send_at", "tls", "cert_err" };

    private static readonly IDictionary<string, Type> TypeMapping = new Dictionary<string, Type>()
    {
        {"processed", typeof(ProcessedEvent)},
        {"bounce", typeof(BounceEvent)},
        {"click", typeof(ClickEvent)},
        {"deferred", typeof(DeferredEvent)},
        {"delivered", typeof(DeliveryEvent)},
        {"dropped", typeof(DroppedEvent)},
        {"open", typeof(OpenEvent)},
        {"spamreport", typeof(SpamReportEvent)},
        {"unsubscribe", typeof(UnsubscribeEvent)},
        {"group_resubscribe", typeof(GroupResubscribeEvent)},
        {"group_unsubscribe", typeof(GroupUnsubscribeEvent)}
    };
    
    public override void Write(Utf8JsonWriter writer, object value, JsonSerializerOptions serializer)
    {
        throw new NotImplementedException("The webhook JSON converter does not support write operations.");
    }

    public override object Read(ref Utf8JsonReader reader, Type objectType, JsonSerializerOptions options)
    {
	    if (reader.TokenType != JsonTokenType.StartArray)
		    throw new JsonException("Expected StartArray token");

        List<WebhookEventBase> events = new();
        JsonElement element = new JsonElement();
        
        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
	        var jsonObject = JsonDocument.ParseValue(ref reader);
            
	        //serialise based on the event type
	        jsonObject.RootElement.TryGetProperty("event", out var eventName); 

	        if (!TypeMapping.ContainsKey(eventName.ToString()))
		        throw new NotImplementedException(string.Format("Event {0} is not implemented yet.", eventName.ToString()));

	        Type type = TypeMapping[eventName.ToString()];
	        WebhookEventBase webhookItem = (WebhookEventBase) jsonObject.Deserialize(type, options);

	        AddUnmappedPropertiesAsUnique(webhookItem, jsonObject);

	        events.Add(webhookItem);
        }
        return events;
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof (WebhookEventBase) || objectType == typeof (IList<WebhookEventBase>);
    }

    private void AddUnmappedPropertiesAsUnique(WebhookEventBase webhookEvent, JsonDocument jObject)
    {
        var dict = jObject.RootElement.EnumerateObject();

        foreach (var o in dict)
        {
            if (KnownProperties.Contains(o.Name))
                continue;

            if (o.Value.ValueKind is not (JsonValueKind.Object or JsonValueKind.Number))
            {
                webhookEvent.UniqueParameters.Add(o.Name, o.Value.GetString());
            }
            else
            {
                webhookEvent.UniqueParameters.Add(o.Name, o.Value.GetRawText());
            }
        }
    }
}

