namespace post_it_dotnet_fullstack.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumsController : ControllerBase
{
  public AlbumsController(AlbumsService albumsService, Auth0Provider auth0Provider)
  {
    _albumsService = albumsService;
    _auth0Provider = auth0Provider;
  }

  private readonly Auth0Provider _auth0Provider;
  private readonly AlbumsService _albumsService;

  [Authorize]
  [HttpPost]
  public async Task<ActionResult<Album>> CreateAlbum([FromBody] Album albumData)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      albumData.CreatorId = userInfo.Id;
      Album album = _albumsService.CreateAlbum(albumData);
      return Ok(album);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

}
