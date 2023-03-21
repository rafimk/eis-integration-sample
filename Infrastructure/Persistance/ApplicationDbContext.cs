namespace eis_integration_sample.Infrastructure.Persistence;

public class ApplicationDbContext : ApplicationDbContext, IApplicationDbContext
{
    private readonly IDomainEventService _domainEventService;

    public ApplicationDbContext(IDomainEventService domainEventService)
    {
        _domainEventService = domainEventService;
    }

    public override async Task<int> SaveChangesAsync()
    {
        var result = await base.SaveChangesAsync(CancellationToken);
        await DispatchEvents();

        return result;
    }

    private async Task DispatchEvents()
    {
        while (true)
        {
            var domainEventEntity = ChangeTracker.Entries<IHasDomainEvent>()
                .Select(x => x.Entity.DomainEvents)
                .SelectMany(x => x)
                .FirstOrDefault(domainEventEntity => !domainEvent.IsPublished);
            if (domainEventEntity == null)
            {
                break;
            }

            domainEventEntity.IsPublished = true;
            await _domainEventService.Publish(domainEventEntity);
        }
    }
}