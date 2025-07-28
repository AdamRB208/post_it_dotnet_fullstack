using Microsoft.IdentityModel.Tokens;

namespace post_it_dotnet_fullstack.Services;

public class WatchersService
{
  public WatchersService(WatchersRepository watchersRepository)
  {
    _watchersRepository = watchersRepository;
  }
  private readonly WatchersRepository _watchersRepository;

  internal WatcherProfile CreateWatcher(Watcher watcherData)
  {
    WatcherProfile watcherProfile = _watchersRepository.CreateWatcher(watcherData);
    return watcherProfile;
  }

  internal List<WatcherProfile> GetWatcherByAlbumId(int albumId)
  {
    List<WatcherProfile> watcherProfiles = _watchersRepository.GetWatcherByAlbumId(albumId);
    return watcherProfiles;
  }

  internal List<WatcherAlbum> GetWatcherAlbums(string accountId)
  {
    List<WatcherAlbum> watcherAlbums = _watchersRepository.GetWatcherAlbums(accountId);
    return watcherAlbums;
  }
}