namespace Sendgrid.Webhooks.Events;

public class DroppedEvent : DeliveryEventBase
{
	[JsonPropertyName("reason")]
	public string Reason { get; set; }
}