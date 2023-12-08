

namespace server.Services;

public class VaultKeepsService
{
    private readonly VaultKeepsRepository _vaultKeepsRepository;
    private readonly VaultsService _vaultsService;
    private readonly KeepsService _keepsService;

    public VaultKeepsService(VaultKeepsRepository vaultKeepsRepository, VaultsService vaultsService, KeepsService keepsService)
    {
        _vaultKeepsRepository = vaultKeepsRepository;
        _vaultsService = vaultsService;
        _keepsService = keepsService;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        Vault vault = _vaultsService.GetVaultById(vaultKeepData.VaultId, vaultKeepData.CreatorId);
        if (vault.CreatorId != vaultKeepData.CreatorId)
        {
            throw new Exception("NOT YOUR VAULT");
        }

        VaultKeep vaultKeep = _vaultKeepsRepository.CreateVaultKeep(vaultKeepData);
        return vaultKeep;
    }
}
