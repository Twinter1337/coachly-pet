namespace CoachlyBackEnd.Config;

public class StripeSettings
{
    public string SecretKey { get; set; }
    public string PublishableKey { get; set; }
    public string WebhookSecret { get; set; }
}