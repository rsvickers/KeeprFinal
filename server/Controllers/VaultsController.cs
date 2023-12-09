
using server.Models;
using server.Services;

namespace KeeprFinal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VaultsController : ControllerBase
{
    private readonly VaultsService _vaultsService;
    private readonly Auth0Provider _auth0Provider;
    private readonly VaultKeepsService _vaultKeepsService;

    public VaultsController(VaultsService vaultsService, Auth0Provider auth0Provider, VaultKeepsService vaultKeepsService)
    {
        _vaultsService = vaultsService;
        _auth0Provider = auth0Provider;
        _vaultKeepsService = vaultKeepsService;
    }

    [Authorize]
    [HttpPost]
    public async Task<ActionResult<Vault>> CreateVault([FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            vaultData.CreatorId = userInfo.Id;
            Vault vault = _vaultsService.CreateVault(vaultData);
            return Ok(vault);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{vaultId}")]
    public async Task<ActionResult<Vault>> GetVaultById(int vaultId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _vaultsService.GetVaultById(vaultId, userInfo?.Id);
            return Ok(vault);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [HttpGet("{vaultId}/keeps")]
    public async Task<ActionResult<List<KeepVaultModel>>> GetKeepsInVault(int vaultId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            List<KeepVaultModel> keepVaultModel = _vaultsService.GetKeepsInVault(vaultId, userInfo?.Id);
            return Ok(keepVaultModel);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }


    [Authorize]
    [HttpPut("{vaultId}")]
    public async Task<ActionResult<Vault>> UpdateVault(int vaultId, [FromBody] Vault vaultData)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            Vault vault = _vaultsService.UpdateVault(vaultId, userInfo?.Id, vaultData);
            return Ok(vault);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }

    [Authorize]
    [HttpDelete("{vaultId}")]
    public async Task<ActionResult<string>> RemoveVault(int vaultId)
    {
        try
        {
            Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
            string message = _vaultsService.RemoveVault(vaultId, userInfo?.Id);
            return Ok(message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}
