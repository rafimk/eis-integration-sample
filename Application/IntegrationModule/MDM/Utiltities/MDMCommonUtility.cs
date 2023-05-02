namespace eis_integration_sample.Application.IntegrationModule.MDM.Utilties;

public class MDMCommonUtility
{
    public static MasterDataBase UpdateCommonAttributes(MasterDataBase entity, MasterDataBaseContract contract)
    {
        if (contract.Description != null && !contract.Description.Equals(entity.Description))
        {
            entity.Description = contract.Description;
        }

        if (contract.IsDelete != null && !contract.IsDelete.Equals(entity.IsDelete))
        {
            entity.IsDelete = contract.IsDelete.Equals("Y");
        }

        return entity;
    }
}