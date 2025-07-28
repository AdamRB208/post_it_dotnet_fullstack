namespace post_it_dotnet_fullstack.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WatchersController : ControllerBase
{
  public WatchersController(WatchersService watchersService, Auth0Provider auth0Provider)
  {
    _watchersService = watchersService;
    _auth0Provider = auth0Provider;
  }

  private readonly WatchersService _watchersService;
  private readonly Auth0Provider _auth0Provider;


  [Authorize]
  [HttpPost]
  public async Task<ActionResult<WatcherProfile>> CreateWatcher([FromBody] Watcher watcherData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      watcherData.AccountId = userInfo.Id;
      WatcherProfile watcherProfile = _watchersService.CreateWatcher(watcherData);
      return Ok(watcherProfile);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }


}