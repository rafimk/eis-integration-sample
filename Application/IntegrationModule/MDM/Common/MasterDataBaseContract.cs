namespace eis_integration_sample.Application.IntegrationModule.MDM.Common;

public abstract class MasterDataBaseContract
{
    public Guid Id { get; set; }
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string IsDeleted { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
}