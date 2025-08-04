using System.Threading.Tasks;

namespace post_it_dotnet_fullstack.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlbumsController : ControllerBase
{
  public AlbumsController(AlbumsService albumsService, Auth0Provider auth0Provider, PicturesService picturesService, WatchersService watchersService)
  {
    _albumsService = albumsService;
    _auth0Provider = auth0Provider;
    _picturesService = picturesService;
    _watchersService = watchersService;
  }

  private readonly Auth0Provider _auth0Provider;
  private readonly AlbumsService _albumsService;
  private readonly PicturesService _picturesService;
  private readonly WatchersService _watchersService;

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

  [HttpGet]
  public ActionResult<List<Album>> GetAlbums([FromQuery] string category)
  {
    try
    {
      List<Album> albums;
      if (category == null)
      {
        albums = _albumsService.GetAlbums();
      }
      else
      {
        albums = _albumsService.GetAlbums(category);
      }
      return Ok(albums);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

  [HttpGet("{albumId}")]
  public ActionResult<Album> GetAlbumById(int albumId)
  {
    try
    {
      Album album = _albumsService.GetAlbumById(albumId);
      return Ok(album);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

  [Authorize]
  [HttpPut("{albumId}")]
  public async Task<ActionResult<Album>> ArchiveAlbum(int albumId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      Album album = _albumsService.ArchiveAlbum(albumId, userInfo);
      return Ok(album);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

  [HttpGet("{albumId}/pictures")]
  public ActionResult<List<Picture>> GetPicturesByAlbumId(int albumId)
  {
    try
    {
      List<Picture> pictures = _picturesService.GetPicturesByAlbumId(albumId);
      return Ok(pictures);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

  [HttpGet("{albumId}/watchers")]
  public ActionResult<List<WatcherProfile>> GetWatcherByAlbumId(int albumId)
  {
    try
    {
      List<WatcherProfile> watcherProfiles = _watchersService.GetWatcherByAlbumId(albumId);
      return Ok(watcherProfiles);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

  [Authorize]
  [HttpDelete("{albumId}")]
  public async Task<ActionResult<string>> DeleteAlbum(int albumId)
  {
    try
    {
      Account userInfo = await _auth0Provider.GetUserInfoAsync<Account>(HttpContext);
      string message = _albumsService.DeleteAlbum(albumId, userInfo);
      return Ok(message);
    }
    catch (Exception error)
    {
      return BadRequest(error.Message);
    }
  }

}
