namespace eis_integration_sample.Infrastructure.Services;

public class EisEventProcessorService : IMessageProcessor
{
    private readonly ILogger<EisEventProcessorService> _logger;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public EisEventProcessorService(ILogger<EisEventProcessorService> logger, IServiceScopeFactory serviceScopeFactory)
    {
        _logger = logger;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task Process(Payload payload, string eventType)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var payloadContent = payload.Contenet;

        var sourceSystemName = payload?.sourceSystemName;
        object payloadContractCommand = null;

        if (sourceSystemName.Equals("MDM"))
        {
            if (eventType.Equals(EisEventTypes.MDM.Created))
            {
                payloadContractCommand = EisCore.Application.Util.JsonSerializerUtil.DeserializedObject<MDMCreateDto>(payloadContent.ToString());
            }
        }

        if (payloadContractCommand != null)
        {
            await mediator.Send(payloadContractCommand);
        }
        else
        {
            _logger.LogDebug($"<< {eventType} - received - not used for xxx");
        }
    }
}