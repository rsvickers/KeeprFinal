

namespace KeeprFinal.Repositories;

public class ProfilesRepository
{
    private readonly IDbConnection _db;

    public ProfilesRepository(IDbConnection db)
    {
        _db = db;
    }

    internal Profile GetProfilesById(string profileId)
    {
        string sql = @"
        SELECT * FROM accounts WHERE id = @profileId;";

        return _db.Query<Profile>(sql, new { profileId }).FirstOrDefault();
    }
}
