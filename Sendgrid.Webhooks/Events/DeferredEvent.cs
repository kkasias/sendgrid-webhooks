namespace Sendgrid.Webhooks.Events;

public class DeferredEvent : DeliveryEventBase
{
	[JsonPropertyName("response")]
	public string Response { get; set; }

	[JsonPropertyName("attempt")]
	public string Attempts { get; set; }
}