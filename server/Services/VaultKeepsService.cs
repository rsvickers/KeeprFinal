
namespace KeeprFinal.Services;

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
        // Keep keep = _keepsService.GetKeepsById
        VaultKeep vaultKeep = _vaultKeepsRepository.CreateVaultKeep(vaultKeepData);

        return vaultKeep;
    }

    internal VaultKeep GetById(int vaultKeepId)
    {
        VaultKeep vaultKeep = _vaultKeepsRepository.GetById(vaultKeepId);
        if (vaultKeep == null)
        {
            throw new Exception($"{vaultKeepId} is not a valid ID");
        }
        return vaultKeep;
    }

    internal string RemoveVaultKeep(int vaultKeepId, string userId)
    {
        VaultKeep vaultKeep = GetById(vaultKeepId);

        if (userId != vaultKeep.CreatorId)
        {
            throw new Exception("NOT YOURS TO DELETE");
        }
        _vaultKeepsRepository.RemoveVaultKeep(vaultKeepId);
        return $"{vaultKeepId} has been deleted";

        // Vault vault = _vaultsService.GetVaultById(vaultKeep.VaultId);

    }

    // internal VaultKeep GetKepts(VaultKeep vaultKeepData)
    // {
    //     VaultKeep vaultKeep = 
    // }

}
