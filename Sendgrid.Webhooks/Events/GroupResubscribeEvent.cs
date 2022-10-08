namespace Sendgrid.Webhooks.Events;

public class GroupResubscribeEvent : EngagementEventBase
{
	[JsonPropertyName("asm_group_id")]
	public int GroupId { get; set; }
}