

namespace KeeprFinal.Repositories;

public class KeepsRepository
{
    private readonly IDbConnection _db;

    public KeepsRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Keep CreateKeep(Keep keepData)
    {
        string sql = @"
        INSERT INTO
        keeps (name, description, img, views,creatorId)
        VALUES (@Name, @Description, @Img, @Views, @CreatorId);

        SELECT 
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creatorId = accounts.id
        WHERE keeps.id = LAST_INSERT_ID();";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder, keepData).FirstOrDefault();

        return keep;
    }

    private Keep KeepBuilder(Keep keep, Profile profile)
    {
        keep.Creator = profile;
        return keep;
    }
}
