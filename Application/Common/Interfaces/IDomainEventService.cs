namespace eis_integration_sample.Application.Common.Interfaces;

public interface IDomainEventService
{
    Task Publish(DomainEvent domainEvent);
}