
namespace KeeprFinal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class KeepsController : ControllerBase
{

    private readonly KeepsService _keepsService;
    private readonly Auth0Provider _auth0Provider;

    public KeepsController(Auth0Provider auth0Provider, KeepsService keepsService)
    {
        _auth0Provider = auth0Provider;
        _keepsService = keepsService;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Keep>> CreateKeep([FromBody] Keep keepData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            keepData.CreatorId = userInfo.Id;
            Keep keep = _keepsService.CreateKeep(keepData);
            return Ok(keep);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet]
    public ActionResult<List<Keep>> GetKeeps()
    {
        try
        {
            List<Keep> keeps = _keepsService.GetKeeps();
            return Ok(keeps);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{keepId}")]
    public async Task<ActionResult<Keep>> GetKeepById(int keepId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            // TODO after update change this so it counts up after visit
            Keep keep = _keepsService.GetKeepsByIdAndAddViews(keepId, userInfo?.Id);
            return Ok(keep);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpPut("{keepId}")]
    public async Task<ActionResult<Keep>> UpdateKeep(int keepId, [FromBody] Keep keepData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Keep keep = _keepsService.UpdateKeep(keepId, userInfo?.Id, keepData);
            return Ok(keep);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

}


