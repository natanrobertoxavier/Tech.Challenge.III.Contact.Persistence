namespace Contact.Persistence.Infrastructure.Queue;
public static class RabbitMqConstants
{
    public const string ContactPersistenceExchange = $"contact.persistence.exchange";

    public const string RegisterContactQueueName = "contact.persistence.on-register-contact";
    public const string RegisterContactRoutingKey = "on-register-contact";
}
