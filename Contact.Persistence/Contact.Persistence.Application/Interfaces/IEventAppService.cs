using Contact.Persistence.Domain.Messages.DomaiEvents;

namespace Contact.Persistence.Application.Interfaces;
public interface IEventAppService
{
    Task SendRegionCreateEvent(ContactCreateDomainEvent message);
}
