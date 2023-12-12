


namespace KeeprFinal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultKeepsController : ControllerBase
{
    private readonly Auth0Provider _auth0Provider;
    private readonly VaultKeepsService _vaultKeepsService;

    public VaultKeepsController(Auth0Provider auth0Provider, VaultKeepsService vaultKeepsService)
    {
        _auth0Provider = auth0Provider;
        _vaultKeepsService = vaultKeepsService;
    }


    [Authorize]
    [HttpPost]
    public async Task<ActionResult<VaultKeep>> CreateVaultKeep([FromBody] VaultKeep vaultKeepData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            vaultKeepData.CreatorId = userInfo.Id;
            VaultKeep vaultKeep = _vaultKeepsService.CreateVaultKeep(vaultKeepData);
            return Ok(vaultKeep);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpDelete("{vaultKeepId}")]
    public async Task<ActionResult<string>> RemoveVaultKeep(int vaultKeepId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            _vaultKeepsService.RemoveVaultKeep(vaultKeepId, userInfo.Id);
            string message = "The Vault Keep was deleted!";
            return Ok(message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
