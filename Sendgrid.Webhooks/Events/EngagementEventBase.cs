using Sendgrid.Webhooks.Converters;

namespace Sendgrid.Webhooks.Events;

public abstract class EngagementEventBase : WebhookEventBase
{
	[JsonPropertyName("useragent")]
	public string UserAgent { get; set; }

	[JsonPropertyName("ip")]
	public string Ip { get; set; }

	[JsonConverter(typeof(BooleanConverter))]
	[JsonPropertyName("tls")]
	public bool Tls { get; set; }

	[JsonConverter(typeof(BooleanConverter))]
	[JsonPropertyName("cert_err")]
	public bool CertificateError { get; set; }
}