



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

  internal Album GetAlbumById(int albumId)
  {
    Album album = _albumsRepository.GetAlbumById(albumId);
    if (album == null)
    {
      throw new Exception("No album found with the id of " + albumId);
    }
    return album;
  }

  internal Album ArchiveAlbum(int albumId, Account userInfo)
  {
    Album album = GetAlbumById(albumId);
    if (album.CreatorId != userInfo.Id)
    {
      throw new Exception($"YOU CANNOT ARCHIVE ANOTHER USER'S ALBUM, {userInfo.Name.ToUpper()}!");
    }
    album.Archived = !album.Archived;
    _albumsRepository.ArchiveAlbum(album);
    return album;
  }
}