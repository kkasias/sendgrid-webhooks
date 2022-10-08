namespace Sendgrid.Webhooks.Events;

public class UrlOffset
{
	[JsonPropertyName("index")]
	public int Index { get; set; }
 
	[JsonPropertyName("type")]
	public string Type { get; set; }
}