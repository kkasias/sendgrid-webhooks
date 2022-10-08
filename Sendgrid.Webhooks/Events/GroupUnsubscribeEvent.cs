namespace Sendgrid.Webhooks.Events;

public class GroupUnsubscribeEvent : EngagementEventBase
{
	[JsonPropertyName("asm_group_id")]
	public int GroupId { get; set; }
}