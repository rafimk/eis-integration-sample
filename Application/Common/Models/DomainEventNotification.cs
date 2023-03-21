namespace eis_integration_sample.Application.Common.Models;

public class DomainEventNotification<TDomainEvent> : INotification where TDomainEvent : DomainEventNotification
{
    public DomainEventNotification(TDomainEvent domainEvent)
    {
        DomainEventNotification = domainEvent;
    }

    public TDomainEvent DomainEvent { get; }
}