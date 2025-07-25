
namespace post_it_dotnet_fullstack.Services;

public class AlbumsService
{
  public AlbumsService(AlbumsRepository albumsRepository)
  {
    _albumsRepository = albumsRepository;
  }
  private readonly AlbumsRepository _albumsRepository;

  internal Album CreateAlbum(Album albumData)
  {
    Album album = _albumsRepository.CreateAlbum(albumData);
    return album;
  }
}