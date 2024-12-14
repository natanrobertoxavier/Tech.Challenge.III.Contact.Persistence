using Contact.Persistence.Application.Interfaces;
using Contact.Persistence.Domain.Messages.DomaiEvents;
using MediatR;
using Serilog;

namespace Contact.Persistence.Application.Messages.Handlers;
public class ContactEventHandler(
    ILogger logger,
    IEventAppService eventAppService) : INotificationHandler<ContactCreateDomainEvent>
{
    private readonly ILogger _logger = logger;
    private readonly IEventAppService _eventAppService = eventAppService;

    public async Task Handle(ContactCreateDomainEvent message, CancellationToken cancellationToken)
    {
        await _eventAppService.SendRegionCreateEvent(message);
    }
}
