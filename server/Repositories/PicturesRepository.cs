namespace post_it_dotnet_fullstack.Repositories;

public class PicturesRepository
{
  public PicturesRepository(IDbConnection db)
  {
    _db = db;
  }
  private readonly IDbConnection _db;


  
}