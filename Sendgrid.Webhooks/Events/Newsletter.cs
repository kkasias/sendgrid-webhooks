namespace Sendgrid.Webhooks.Events;

public class Newsletter
{
	[JsonPropertyName("newsletter_user_list_id")]
	public string NewsletterUserListId { get; set; }

	[JsonPropertyName("newsletter_id")]
	public string NewsletterId { get; set; }

	[JsonPropertyName("newsletter_send_id")]
	public string NewsletterSendId { get; set; }
}