
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

    // NOTE may have to add an counter to go up after other things are built out
    internal List<Keep> GetKeeps()
    {
        string sql = @"
        SELECT 
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creatorId = accounts.id;";

        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder).ToList();
        return keeps;
    }

    internal Keep GetKeepsById(int keepId)
    {
        string sql = @"
        SELECT 
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creatorId = accounts.id
        WHERE keeps.id = @keepId;";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder, new { keepId }).FirstOrDefault();
        return keep;
    }

    internal List<Keep> GetKeepsByProfileId(string profileId)
    {
        string sql = @"
        SELECT 
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creatorId = accounts.id
        WHERE keeps.creatorId = @profileId;";

        List<Keep> keeps = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder, new { profileId }).ToList();
        return keeps;
    }

    internal void RemoveKeep(int keepId)
    {
        string sql = "DELETE FROM keeps WHERE id = @keepId LIMIT 1;";
        _db.Execute(sql, new { keepId });
    }

    internal Keep UpdateKeep(Keep keepData)
    {
        string sql = @"
        UPDATE keeps
        SET
        name = @Name,
        description = @Description,
        img = @Img,
        views = @Views
        WHERE id = @Id LIMIT 1;

        SELECT 
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON keeps.creatorId = accounts.id
        WHERE keeps.id = @Id;";

        Keep keep = _db.Query<Keep, Profile, Keep>(sql, KeepBuilder, keepData).FirstOrDefault();
        return keep;
    }

    private Keep KeepBuilder(Keep keep, Profile profile)
    {
        keep.Creator = profile;
        return keep;
    }
}
