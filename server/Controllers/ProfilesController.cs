
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
}
