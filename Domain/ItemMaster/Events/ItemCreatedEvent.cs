namespace eis_integration_sample.Domain.ItemMaster.Events;

public class ItemCreatedEvent : DomainEvent
{
    public ItemMaster Item { get; }

    public ItemCreatedEvent(ItemMaster item)
    {
        Item = item;
    }
}