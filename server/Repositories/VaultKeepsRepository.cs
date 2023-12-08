

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


    private VaultKeep VaultKeepBuilder(VaultKeep vaultKeep, Profile profile)
    {
        vaultKeep.Creator = profile;
        return vaultKeep;
    }
}
