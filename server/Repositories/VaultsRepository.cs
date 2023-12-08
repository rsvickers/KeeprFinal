



namespace KeeprFinal.Repositories;

public class VaultsRepository
{
    private readonly IDbConnection _db;

    public VaultsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Vault CreateVault(Vault vaultData)
    {
        string sql = @"
        INSERT INTO
        vaults (name, description, img, isPrivate, creatorId)
        VALUES (@Name, @Description, @Img, @IsPrivate, @CreatorId);

        SELECT 
        vaults.*,
        accounts.*  
        FROM vaults
        JOIN accounts ON accounts.id = vaults.creatorId
        WHERE vaults.id = LAST_INSERT_ID();";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, VaultBuilder, vaultData).FirstOrDefault();
        return vault;
    }

    internal Vault GetVaultById(int vaultId)
    {
        string sql = @"
        SELECT 
        vaults.*,
        accounts.*  
        FROM vaults
        JOIN accounts ON accounts.id = vaults.creatorId
        WHERE vaults.id = @vaultId;";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, VaultBuilder, new { vaultId }).FirstOrDefault();
        return vault;
    }

    internal Vault UpdateVault(Vault vaultData)
    {
        string sql = @"
        UPDATE vaults
        SET 
        name = @Name,
        description = @Description,
        img = @Img,
        isPrivate = @IsPrivate
        WHERE id = @Id LIMIT 1;
        
        SELECT 
        vaults.*,
        accounts.*  
        FROM vaults
        JOIN accounts ON accounts.id = vaults.creatorId
        WHERE vaults.id = @Id;";

        Vault vault = _db.Query<Vault, Profile, Vault>(sql, VaultBuilder, vaultData).FirstOrDefault();
        return vault;
    }

    private Vault VaultBuilder(Vault vault, Profile profile)
    {
        vault.Creator = profile;
        return vault;
    }
}
