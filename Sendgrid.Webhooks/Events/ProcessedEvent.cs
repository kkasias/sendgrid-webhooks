using Sendgrid.Webhooks.Converters;

namespace Sendgrid.Webhooks.Events;

public class ProcessedEvent : DeliveryEventBase
{
	[JsonPropertyName("send_at"), JsonConverter(typeof(EpochToDateTimeConverter))]
	public DateTime SendAt { get; set; }
}