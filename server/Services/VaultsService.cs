


using server.Models;

namespace KeeprFinal.Services;

public class VaultsService
{
    private readonly VaultsRepository _vaultsRepository;
    private readonly VaultKeepsRepository _vaultKeepsRepository;

    public VaultsService(VaultsRepository vaultsRepository, VaultKeepsRepository vaultKeepsRepository)
    {
        _vaultsRepository = vaultsRepository;
        _vaultKeepsRepository = vaultKeepsRepository;
    }

    internal Vault CreateVault(Vault vaultData)
    {
        Vault vault = _vaultsRepository.CreateVault(vaultData);
        return vault;
    }

    internal List<KeepVaultModel> GetKeepsInVault(int vaultId, string userId)
    {
        Vault vault = GetVaultById(vaultId, userId);
        if (vault.IsPrivate && (vault.CreatorId != userId || userId == null))
        {
            throw new Exception("NONE SHALL PASS 🧙‍♂️");
        }
        List<KeepVaultModel> keepVaultModels = _vaultKeepsRepository.GetKeepsInVault(vaultId, userId);
        return keepVaultModels;
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


    internal string RemoveVault(int vaultId, string userId)
    {
        Vault vault = this.GetVaultById(vaultId, userId);
        if (vault.CreatorId != userId)
        {
            throw new Exception("NOT YOUR VAULT");
        }

        _vaultsRepository.RemoveVault(vaultId);
        return $"{vault.Name} has been deleted.";
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
        // vaultToUpdate.IsPrivate = vaultData.IsPrivate ?? vaultToUpdate.IsPrivate;

        Vault vault = _vaultsRepository.UpdateVault(vaultToUpdate);
        return vault;
    }
    internal List<Vault> GetVaultsByAccount(string accountId)
    {
        List<Vault> vaults = _vaultsRepository.GetVaultsByAccount(accountId);
        return vaults;
    }
}
