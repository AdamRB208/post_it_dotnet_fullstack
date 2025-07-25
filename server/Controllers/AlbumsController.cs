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

  

}
