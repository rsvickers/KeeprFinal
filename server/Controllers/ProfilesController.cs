
namespace KeeprFinal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProfilesController : ControllerBase
{
    private readonly Auth0Provider _auth0Provider;
    private readonly ProfilesService _profilesService;

    public ProfilesController(Auth0Provider auth0Provider, ProfilesService profilesService)
    {
        _auth0Provider = auth0Provider;
        _profilesService = profilesService;
    }

    [HttpGet("{profileId}")]
    public async Task<ActionResult<Profile>> GetProfileById(string profileId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);

            Profile profile = _profilesService.GetProfileById(profileId);
            return profile;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{profileId}/keeps")]
    public ActionResult<List<Keep>> GetKeepsByProfileId(string profileId)
    {
        try
        {
            List<Keep> keeps = _profilesService.GetKeepsByProfileId(profileId);
            return keeps;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{profileId}/vaults")]
    public ActionResult<List<Vault>> GetVaultsByProfileId(string profileId)
    {
        try
        {
            List<Vault> vaults = _profilesService.GetVaultsByProfileId(profileId);
            return vaults;
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
