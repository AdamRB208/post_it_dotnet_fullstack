namespace post_it_dotnet_fullstack.Repositories;

public class AlbumsRepository
{
  public AlbumsRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;

  
}