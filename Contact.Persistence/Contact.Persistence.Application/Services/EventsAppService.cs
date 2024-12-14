using Contact.Persistence.Application.Events;
using Contact.Persistence.Application.Interfaces;
using Contact.Persistence.Domain.Messages.DomaiEvents;
using Contact.Persistence.Infrastructure.Queue;

namespace Contact.Persistence.Application.Services;
public class EventsAppService(
    IRabbitMqEventsDispatcher rabbitMqEventsDispatcher) : IEventAppService
{
    private readonly IRabbitMqEventsDispatcher _rabbitMqEventsDispatcher = rabbitMqEventsDispatcher;

    public async Task SendRegionCreateEvent(ContactCreateDomainEvent message)
    {
        await _rabbitMqEventsDispatcher.SendEvent(new OnContactCreateEvent()
        {
            Payload = new OnContactCreateMessage(
                message.Id,
                message.FirstName,
                message.LastName,
                message.DDDId,
                message.PhoneNumber,
                message.Email,
                message.UserId
            )
        });
    }
}
