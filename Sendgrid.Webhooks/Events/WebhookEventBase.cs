using Sendgrid.Webhooks.Converters;
using Sendgrid.Webhooks.Service;

namespace Sendgrid.Webhooks.Events;

public abstract class WebhookEventBase
{
	public WebhookEventBase()
	{
		UniqueParameters = new Dictionary<string, string>();
	}

	[JsonPropertyName("sg_message_id")]
	public string SgMessageId { get; set; }

	[JsonPropertyName("sg_event_id")]
	public string SgEventId { get; set; }

	[JsonPropertyName("event"), JsonConverter(typeof(JsonStringEnumConverter))]
	public WebhookEventType EventType { get; set; }

	[JsonPropertyName("email")]
	public string Email { get; set; }

	[JsonPropertyName("category"), JsonConverter(typeof(WebhookCategoryConverter))]
	public IList<string> Category { get; set; }

	[JsonPropertyName("timestamp"), JsonConverter(typeof(EpochToDateTimeConverter))]
	public DateTime Timestamp { get; set; }

	public IDictionary<string, string> UniqueParameters { get; set; }
}