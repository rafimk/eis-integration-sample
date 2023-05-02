namespace eis_integration_sample.Application.ItemMaster.Publish;

public ItemMasterCreatedEventDispatcher : INotification<DomainEventNotification<ItemCreatedEvent>>
{
    private readonly ILogger<ItemMasterCreatedEventDispatcher> _logger;
    private readonly IDomainEventDispatcher<ItemMaster> _domainEventDispatcher;

    public ItemMasterCreatedEventDispatcher(ILogger<ItemMasterCreatedEventDispatcher> logger, IDomainEventDispatcher<ItemMaster> domainEventDispatcher)
    {
        _logger = logger;
        _domainEventDispatcher = domainEventDispatcher;
    }

    public async Task Handle(DomainEventNotification<ItemCreatedEvent> notification, CancellationToken cancellationToken)
    {
        var domainEvent = notification.DomainEvent;
        await _domainEventDispatcher.DispatchEvent(domainEvent.Item, EISEventType.ItemMaster.Created);
        _logger.LogInformation("Item Created");
    }
}

public class ItemMasterDispatcher : IDomainEventDispatcher<ItemMaster>
{
    private readonly IEventPublisherService _eventDispacherService;

    public ItemCreatedEventDispacther(IEventPublisherService eventDispacherService)
    {
        _eventDispacherService = eventDispacherService;
    }

    public async Task DispatchEvent(ItemMaster entity, string eventType)
    {
        if (EISConstants.PublishStatus)
        {
            var itemCreatedContract = new ItemCreatedContract
            {
                ItemId = entity.Id,
                ItemName = entity.Name,
                CreatedBy = "ItemMasterModule"
            };

            Payload payload = new(itemCreatedContract, "ItemCreated", "ItemMaster");
            EisEventPayloadBehaviour payloadBehaviour = new(payload, eventType);
            await _eventDispacherService.publish(payloadBehaviour);
        }
    }
}