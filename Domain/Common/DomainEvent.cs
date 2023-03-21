using System;
namespace eis_integration_sample.Domain.Common;

public abstract class DomainEvent
{
    public Guid Id { get; set; }
    public bool IsPublished { get; set;}
    public DateTimeOffset DateOccured { get; protected set; } = DateTime.UtcNow;

    protected DomainEvent()
    {
        DateOccured = DateTimeOffset.UtcNow;
    }
}