namespace post_it_dotnet_fullstack.Services;

public class PicturesService
{
  public PicturesService(PicturesRepository picturesRepository)
  {
    _picturesRepository = picturesRepository;
  }
  private readonly PicturesRepository _picturesRepository;

  
}