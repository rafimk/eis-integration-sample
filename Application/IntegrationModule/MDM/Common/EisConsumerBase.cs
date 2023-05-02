namespace eis_integration_sample.Application.IntegrationModule.MDM.Common;

public abstract class EisConsumerBase : MasterDataBaseContract
{
    public string ContentType { get; set; }
    public string SourceSystemName { get; set; }
}