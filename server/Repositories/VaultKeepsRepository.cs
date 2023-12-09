


using server.Models;

namespace KeeprFinal.Repositories;

public class VaultKeepsRepository
{
    private readonly IDbConnection _db;

    public VaultKeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal VaultKeep CreateVaultKeep(VaultKeep vaultKeepData)
    {
        string sql = @"
        INSERT INTO
        vaultKeeps (creatorId, vaultId, keepId)
        VALUES (@CreatorId, @VaultId, @KeepId);

        SELECT 
        vaultKeeps.*,
        accounts.*  
        FROM vaultKeeps
        JOIN accounts ON accounts.id = vaultKeeps.creatorId
        WHERE vaultKeeps.id = LAST_INSERT_ID();";

        VaultKeep vaultKeep = _db.Query<VaultKeep, Profile, VaultKeep>(sql, VaultKeepBuilder, vaultKeepData).FirstOrDefault();
        return vaultKeep;
    }

    internal VaultKeep GetById(int vaultKeepId)
    {
        string sql = @"
        SELECT * FROM vaultKeeps
        WHERE id = @vaultKeepId;";

        VaultKeep vaultKeep = _db.Query<VaultKeep>(sql, new { vaultKeepId }).FirstOrDefault();
        return vaultKeep;
    }

    internal List<KeepVaultModel> GetKeepsInVault(int vaultId, string userId)
    {
        string sql = @"
        SELECT
        keeps.*,
        vaultKeeps.*,
        vaults.*,
        accounts.*
        FROM keeps
        JOIN vaultKeeps ON vaultKeeps.keepId = keeps.id
        JOIN vaults ON vaults.id = vaultKeeps.vaultId
        JOIN accounts ON accounts.id = vaultKeeps.creatorId
        WHERE vaultKeeps.vaultId = @vaultId
        ;";

        List<KeepVaultModel> foundKeeps = _db.Query<KeepVaultModel, VaultKeep, Vault, Profile, KeepVaultModel>(sql, (keepVaultModel, vaultKeep, vault, profile) =>
        {
            keepVaultModel.VaultKeepId = vaultKeep.Id;
            vault.Creator = profile;
            keepVaultModel.Creator = profile;
            return keepVaultModel;
        }, new { vaultId }).ToList();
        return foundKeeps;
    }

    internal void RemoveVaultKeep(int vaultKeepId)
    {
        string sql = @"DELETE FROM vaultKeeps WHERE id = @vaultKeepId LIMIT 1;";

        _db.Execute(sql, new { vaultKeepId });
    }

    private VaultKeep VaultKeepBuilder(VaultKeep vaultKeep, Profile profile)
    {
        vaultKeep.Creator = profile;
        return vaultKeep;
    }
}
