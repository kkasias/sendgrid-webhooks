namespace Sendgrid.Webhooks.Events;

public class BounceEvent : DeliveryEventBase
{
    [JsonPropertyName("reason")]
    public string Reason { get; set; }

    [JsonPropertyName("type")]
    public string BounceType { get; set; }

    [JsonPropertyName("status")]
    public string BounceStatus { get; set; }
}
