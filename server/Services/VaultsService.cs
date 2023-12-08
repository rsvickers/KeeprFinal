


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
}
