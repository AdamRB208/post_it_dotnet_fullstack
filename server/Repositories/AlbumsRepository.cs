

using System.Security.AccessControl;

namespace post_it_dotnet_fullstack.Repositories;

public class AlbumsRepository
{
  public AlbumsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  internal Album CreateAlbum(Album albumData)
  {
    string sql = @"
    INSERT INTO 
    albums(title, description, cover_img, creator_id, category)
    VALUES(@Title, @Description, @CoverImg, @CreatorId, @Category);
    
    SELECT albums.*, accounts.*
    FROM albums
    INNER JOIN accounts ON accounts.id = albums.creator_id
    WHERE albums.id = LAST_INSERT_ID();";

    Album createdAlbum = _db.Query(sql, (Album album, Profile account) =>
    {
      album.Creator = account;
      return album;
    }, albumData).SingleOrDefault();
    return createdAlbum;
  }

  internal List<Album> GetAlbums()
  {
    string sql = @"
    SELECT albums.*, accounts.*,
      (SELECT COUNT(*) FROM watchers WHERE watchers.album_id = albums.id) as watcher_count
    FROM albums INNER JOIN accounts ON accounts.id = albums.creator_id;";

    List<Album> albums = _db.Query(sql, (Album album, Profile account) =>
    {
      album.Creator = account;
      return album;
    }).ToList();
    return albums;
  }

  internal List<Album> GetAlbumsByCategory(string category)
  {
    string sql = @"
    SELECT albums.*, accounts.*
    FROM albums
    INNER JOIN accounts ON accounts.id = albums.creator_id
    WHERE albums.category LIKE @category;";

    List<Album> albums = _db.Query(sql, (Album album, Profile account) =>
    {
      album.Creator = account;
      return album;
    }, new { category = $"%{category}%" }).ToList();
    return albums;
  }

  // internal Album GetAlbumById(int albumId)
  // {
  //   string sql = @"
  //   SELECT albums.*, accounts.*,
  //     (SELECT COUNT(*) FROM watchers WHERE watchers.album_id = albums.id) as WatcherCount
  //   FROM albums
  //   INNER JOIN accounts ON accounts.id = albums.creator_id
  //   LEFT JOIN watchers ON watchers.album_id = albums.id
  //   WHERE albums.id = @albumId;";

  //   Album foundAlbum = _db.Query(sql, (Album album, Profile account) =>
  //   {
  //     album.Creator = account;
  //     return album;
  //   }, new { albumId }, splitOn: "id").SingleOrDefault();
  //   return foundAlbum;
  // }

  internal Album GetAlbumById(int albumId)
  {
    string sql = @"
  SELECT albums.*, accounts.*
  FROM albums
  INNER JOIN accounts ON accounts.id = albums.creator_id
  WHERE albums.id = @albumId;";

    Album foundAlbum = _db.Query(sql, (Album album, Profile account) =>
    {
      album.Creator = account;
      return album;
    }, new { albumId }, splitOn: "id").SingleOrDefault();

    if (foundAlbum != null)
    {
      string countSql = "SELECT COUNT(*) FROM watchers WHERE album_id = @albumId;";
      var count = _db.QuerySingle<long>(countSql, new { albumId });
      foundAlbum.WatcherCount = (int)count;
    }

    return foundAlbum;
  }

  internal void ArchiveAlbum(Album album)
  {
    string sql = @"UPDATE albums SET archived = @Archived WHERE id = @Id LIMIT 1;";

    int rowsAffected = _db.Execute(sql, album);
    switch (rowsAffected)
    {
      case 1:
        return;
      case 0:
        throw new Exception("Update was not Successful!");
      default:
        throw new Exception("Update was too Successful!");
    }
  }

  internal void DeleteAlbum(int albumId)
  {
    string sql = @"
    DELETE FROM albums WHERE id = @albumId LIMIT 1;";

    int rowsAffected = _db.Execute(sql, new { albumId });
    if (rowsAffected == 0)
    {
      throw new Exception("NO ROWS WERE DELETED!");
    }
    if (rowsAffected > 1)
    {
      throw new Exception(rowsAffected + "ROWS WERE DELETED!");
    }
  }
}