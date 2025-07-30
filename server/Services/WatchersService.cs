using Microsoft.IdentityModel.Tokens;

namespace post_it_dotnet_fullstack.Services;

public class WatchersService
{
  public WatchersService(WatchersRepository watchersRepository)
  {
    _watchersRepository = watchersRepository;
  }
  private readonly WatchersRepository _watchersRepository;

  internal Watcher CreateWatcher(Watcher watcherData)
  {
    Watcher watcher = _watchersRepository.CreateWatcher(watcherData);
    return watcher;
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

  private Watcher GetWatcherById(int watcherId)
  {
    Watcher watcher = _watchersRepository.GetWatcherById(watcherId);
    if (watcher == null)
    {
      throw new Exception("Invalid watcher id: " + watcherId);
    }
    return watcher;
  }

  internal void DeleteWatcher(int watcherId, Account userInfo)
  {
    Watcher watcher = GetWatcherById(watcherId);
    if (watcher.AccountId != userInfo.Id)
    {
      throw new Exception($"YOU CANNOT DELETE ANOTHER USER'S WATCHER, {userInfo.Name.ToUpper()}!!!");
    }
    _watchersRepository.DeleteWatcher(watcherId);
  }
}