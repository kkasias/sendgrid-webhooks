using Sendgrid.Webhooks.Converters;
using Sendgrid.Webhooks.Events;

namespace Sendgrid.Webhooks.Service;

public class WebhookParser
{
    private readonly JsonSerializerOptions _options = new();

    public WebhookParser()
    {
        _options.Converters.Add(new WebhookJsonConverter());
    }

    public WebhookParser(JsonConverter converter)
    {
        if (converter == null) {
            throw new ArgumentNullException(nameof(converter));
        }
        
        _options.Converters.Add(converter);
    }

    public IList<WebhookEventBase> ParseEvents(String json)
    {
        return JsonSerializer.Deserialize<IList<WebhookEventBase>>(json, _options);
    }
}

