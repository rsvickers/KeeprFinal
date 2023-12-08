

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
}
