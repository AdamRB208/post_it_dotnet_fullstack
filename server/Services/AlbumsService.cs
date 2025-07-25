


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

  internal List<Album> GetAlbums()
  {
    List<Album> albums = _albumsRepository.GetAlbums();
    return albums;
  }

  internal List<Album> GetAlbums(string category)
  {
    List<Album> albums = _albumsRepository.GetAlbumsByCategory(category);
    return albums;
  }
}