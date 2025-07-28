

namespace post_it_dotnet_fullstack.Repositories;

public class WatchersRepository
{
  public WatchersRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal WatcherProfile CreateWatcher(Watcher watcherData)
  {
    string sql = @"
    INSERT INTO watchers(album_id, account_id)
    VALUES (@AlbumId, @AccountId);
    
    SELECT watchers.*, accounts.* FROM watchers
    INNER JOIN accounts ON accounts.id = watchers.account_id
    WHERE watchers.id = LAST_INSERT_ID();";

    WatcherProfile watcherProfile = _db.Query(sql, (Watcher watcher, WatcherProfile profile) =>
    {
      profile.AlbumId = watcher.AlbumId;
      profile.WatcherId = watcher.Id;
      return profile;
    }, watcherData).SingleOrDefault();
    return watcherProfile;
  }

  internal List<WatcherProfile> GetWatcherByAlbumId(int albumId)
  {
    string sql = @"
    SELECT watchers.*, accounts.* FROM watchers
    INNER JOIN accounts ON accounts.id = watchers.account_id
    WHERE watchers.album_id = @albumId;";

    List<WatcherProfile> watcherProfiles = _db.Query(sql, (Watcher watcher, WatcherProfile account) =>
    {
      account.AlbumId = watcher.AlbumId;
      account.Id = watcher.AccountId;
      return account;
    }, new { albumId }).ToList();
    return watcherProfiles;
  }
}