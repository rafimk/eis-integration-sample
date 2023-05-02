namespace eis_integration_sample.Application.IntegrationModule.MDM.ItemFamily.Subscribers;

public class ItemFamilySubscriber : IRequestHandler<ItemFamilyContract>
{
    private readonly IApplicationDbContext _context;
    private readonly ILogger<ItemFamilySubscriber> _logger;

    public ItemFamilySubscriber(IApplicationDbContext context, ILogger<ItemFamilySubscriber> logger)
    {
        _context = context;
        _logger = logger;        
    }

    public async Task Handle(ItemFamilyContract request, CancellationToken cancellationToken)
    {
        await PerformPayloadOperation(request);
        return Unit.Value;
    }

    private async Task PerformPayloadOperation(ItemFamilyContract contract)
    {
        _logger.LogDebug($"{this.GetType}: {contract.GetType{}} received");
        _logger.LogDebug($"{contract}");

        CancellationToken token = default;
        var entity = await _context.ItemFamily.FirstOrDefault(x => x.Code.Equals(contract.code));
        var listingOrder = await _context.ItemFamily.OrderBy(x => x.ListingOrder).AsNoTracking().LastOrDefault();

        if (entity == null)
        {
            var itemFamily = new ItemFamily
            {
                Id = Guid.NewGuid(),
                Code = contract.Code,
                IsActive = true,
                IsDelete = contract.IsDelete == null && contract.IsDelete.Equals("Y"),
                listingOrder = (listingOrder?.ListingOrder + 1) ?? 0;
            };

            await _context.ItemFamily.Add(itemFamily);
            _logger.LogDebug($"{this.GetType}: {contract.GetType{}} added");
        }
        else if (entity is not null)
        {
            entity = (ItemFamily)MDMCommonUtility.UpdateCommonAttributes(entity, contract);
        }
        else 
        {

        }

        await _context.SaveChangesAsync(token);

    }


}