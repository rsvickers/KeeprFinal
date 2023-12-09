



namespace KeeprFinal.Services;

public class ProfilesService
{
    private readonly ProfilesRepository _profilesRepository;
    private readonly KeepsRepository _keepsRepository;
    private readonly VaultsRepository _vaultsRepository;

    public ProfilesService(ProfilesRepository profilesRepository, KeepsRepository keepsRepository, VaultsRepository vaultsRepository)
    {
        _profilesRepository = profilesRepository;
        _keepsRepository = keepsRepository;
        _vaultsRepository = vaultsRepository;
    }


    internal Profile GetProfileById(string profileId)
    {
        Profile profile = _profilesRepository.GetProfilesById(profileId);
        return profile;
    }

    internal List<Keep> GetKeepsByProfileId(string profileId)
    {
        List<Keep> keeps = _keepsRepository.GetKeepsByProfileId(profileId);
        return keeps;
    }

    internal List<Vault> GetVaultsByProfileId(string profileId)
    {
        List<Vault> vaults = _vaultsRepository.GetVaultsByProfileId(profileId);
        vaults = vaults.FindAll(vault => vault.IsPrivate == false);
        return vaults;
    }
}
