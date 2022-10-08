namespace Sendgrid.Webhooks.Events;

public class ClickEvent : EngagementEventBase
{
	[JsonPropertyName("url")]
	public string Url { get; set; }

	[JsonPropertyName("url_offset")]
	public UrlOffset UrlOffset { get; set; }

	[JsonPropertyName("newsletter")]
	public Newsletter Newsletter { get; set; }
}