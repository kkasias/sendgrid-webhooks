namespace Sendgrid.Webhooks.Events;

public class OpenEvent : EngagementEventBase
{
	[JsonPropertyName("sg_machine_open")]
	public bool SgMachineOpen { get; set; }
	
	[JsonPropertyName("sg_content_type")]
	public string SgContentType { get; set; }

	[JsonPropertyName("sg_template_id")]
	public string SgTemplateId { get; set; }

	[JsonPropertyName("sg_template_name")]
	public string SgTemplateName { get; set; }
}