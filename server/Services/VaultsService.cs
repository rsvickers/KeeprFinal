


namespace KeeprFinal.Services;

public class VaultsService
{
    private readonly VaultsRepository _vaultsRepository;

    public VaultsService(VaultsRepository vaultsRepository)
    {
        _vaultsRepository = vaultsRepository;
    }

    internal Vault CreateVault(Vault vaultData)
    {
        Vault vault = _vaultsRepository.CreateVault(vaultData);
        return vault;
    }

    internal Vault GetVaultById(int vaultId, string userId)
    {
        Vault vault = _vaultsRepository.GetVaultById(vaultId);
        if (vault == null)
        {
            throw new Exception($"{vaultId} is not a valid ID");
        }

        if (vault.IsPrivate == true && vault.CreatorId != userId)
        {
            throw new Exception("NONE SHALL PASS 🧙‍♂️");
        }
        return vault;
    }

    internal Vault UpdateVault(int vaultId, string userId, Vault vaultData)
    {
        Vault vaultToUpdate = GetVaultById(vaultId, userId);

        if (vaultToUpdate.CreatorId != userId)
        {
            throw new Exception("NOT YOUR VAULT");
        }

        vaultToUpdate.Name = vaultData.Name ?? vaultToUpdate.Name;
        vaultToUpdate.Description = vaultData.Description ?? vaultToUpdate.Description;
        vaultToUpdate.Img = vaultData.Img ?? vaultToUpdate.Img;
        vaultToUpdate.IsPrivate = vaultData.IsPrivate ?? vaultToUpdate.IsPrivate;

        Vault vault = _vaultsRepository.UpdateVault(vaultToUpdate);
        return vault;
    }
}
