


namespace post_it_dotnet_fullstack.Repositories;

public class WatchersRepository
{
  public WatchersRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal Watcher CreateWatcher(Watcher watcherData)
  {
    string sql = @"
    INSERT INTO watchers(album_id, account_id)
    VALUES (@AlbumId, @AccountId);
    
    SELECT watchers.* FROM watchers
    WHERE watchers.id = LAST_INSERT_ID();";

    Watcher watcher = _db.Query<Watcher>(sql, watcherData).SingleOrDefault();
    return watcher;
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

  internal List<WatcherAlbum> GetWatcherAlbums(string accountId)
  {
    string sql = @"
    SELECT watchers.*, albums.*, accounts.* FROM watchers
    INNER JOIN albums ON albums.id = watchers.album_id
    INNER JOIN accounts ON accounts.id = albums.creator_id
    WHERE watchers.account_id = @accountId;";

    List<WatcherAlbum> watcherAlbums = _db.Query(sql, (Watcher watcher, WatcherAlbum album, Profile account) =>
    {
      album.AccountId = watcher.AccountId;
      album.WatcherId = watcher.Id;
      album.Creator = account;
      return album;
    }, new { accountId }).ToList();
    return watcherAlbums;
  }
}